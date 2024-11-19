// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AspNetCore.Swagger.Themes;
using CodeOfChaos.Extensions.AspNetCore;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using InfiniLore.Server.API;
using InfiniLore.Server.Components;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Services;
using InfiniLore.Server.Services.CQRS;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Testcontainers.MsSql;

namespace InfiniLore.Server;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    public static async Task Main(string[] args) {
        // -------------------------------------------------------------------------------------------------------------
        // Builder
        // -------------------------------------------------------------------------------------------------------------
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.OverrideLoggingAsSeriLog();

        // TODO: Add Kestrel SLL

        #region Database
        MsSqlContainer? container = new MsSqlBuilder()
            // .WithLogger(Log.Logger)
            .WithImage("mcr.microsoft.com/mssql/server:2022-CU10-ubuntu-22.04")
            .WithPassword("AnnaIsTrans4Ever!")
            .WithName("infinilore-production-db")
            .WithReuse(true)
            .Build();

        if (container is null) throw new Exception("Could not create MsSqlContainer");

        await container.StartAsync();

        builder.Services.AddDbContextFactory<InfiniLoreDbContext>(options =>
            options.UseSqlServer(container.GetConnectionString())
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
        );
        #endregion

        #region Authentication
        builder.Services.AddAuthenticationJwtBearer(
            signingOptions: options => {
                options.SigningKey = builder.Configuration["JWT:Key"];
            },
            bearerOptions: bearerOptions => {
                bearerOptions.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
                bearerOptions.TokenValidationParameters.NameClaimType = ClaimTypes.NameIdentifier;

                bearerOptions.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:Issuer"];
                bearerOptions.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:Audience"];

                bearerOptions.MapInboundClaims = true;
            });

        builder.Services.AddAuthentication(o => {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        // TODO Add google oauth login

        builder.Services.AddIdentityCore<InfiniLoreUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<InfiniLoreDbContext>()
            .AddSignInManager();

        builder.Services.ConfigureApplicationCookie(
            cookieOptions => {
                // ReSharper disable once RedundantLambdaParameterType
                cookieOptions.Events.OnRedirectToLogin = (RedirectContext<CookieAuthenticationOptions> context) => {
                    if (IsApiRequest(context)) {
                        context.Response.StatusCode = 401;
                    }
                    else {
                        context.Response.Redirect(context.RedirectUri);
                    }

                    return Task.CompletedTask;
                };

                // ReSharper disable once RedundantLambdaParameterType
                cookieOptions.Events.OnRedirectToAccessDenied = (RedirectContext<CookieAuthenticationOptions> context) => {
                    if (IsApiRequest(context)) {
                        context.Response.StatusCode = 403;
                    }
                    else {
                        context.Response.Redirect(context.RedirectUri);
                    }

                    return Task.CompletedTask;
                };
            });
        #endregion

        #region Authorization
        builder.Services.AddAuthorization();
        #endregion

        #region Razor Components
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        #endregion

        #region API
        builder.Services
            .AddFastEndpoints(options => {
                // options.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All;
                options.Assemblies = [
                    typeof(API.IAssemblyEntry).Assembly
                ];
            })
            .SwaggerDocument(options => {
                options.DocumentSettings = settings => {
                    settings.Version = "v1";
                    settings.Title = "InfiniLore API";
                    settings.Description = "An ASP.NET Core Web API for managing InfiniLore";
                };
            });

        builder.Services.AddIdentityApiEndpoints<InfiniLoreUser>();
        #endregion

        #region MediatR
        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<Services.IAssemblyEntry>();
        });

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        #endregion

        builder.Services.RegisterServicesFromInfiniLoreServerData();
        builder.Services.RegisterServicesFromInfiniLoreServerServices();

        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        WebApplication app = builder.Build();
        await MigrateDatabaseAsync(app);

        if (app.Environment.IsDevelopment()) {
            app.UseWebAssemblyDebugging();
        }
        else {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.MapStaticAssets();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(WasmClient.IAssemblyEntry).Assembly);

        app.UseFastEndpoints(ctx => {
            ctx.Endpoints.RoutePrefix = "api";
            ctx.Binding.ReflectionCache
                .AddFromInfiniLoreServerAPI()
                .AddFromInfiniLoreServerData()
                .AddFromInfiniLoreServerServices()
                ;

            ctx.Errors.UseProblemDetails();
        });

        app.UseOpenApi();
        app.UseSwaggerUI(ModernStyle.Dark, setupAction: ctx => {
            ctx.SwaggerEndpoint("v1/swagger.json", "InfiniLore API v1");
            ctx.RoutePrefix = "swagger";
        });

        await app.RunAsync();
    }

    private async static ValueTask MigrateDatabaseAsync(WebApplication app) {
        // Create a localised scope so we can get the DbContextFactory correctly.
        await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
        await using InfiniLoreDbContext db = await app.Services.GetRequiredService<IDbContextFactory<InfiniLoreDbContext>>().CreateDbContextAsync();
        await db.Database.MigrateAsync();
        await db.SaveChangesAsync();
    }

    private static bool IsApiRequest(RedirectContext<CookieAuthenticationOptions> context) => context is { Request.Path.Value: "/api", Response.StatusCode: 200 };
}
