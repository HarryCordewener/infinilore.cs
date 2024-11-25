﻿// <auto-generated />
using System;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfiniLore.Server.Data.SqlServer.Migrations
{
    [DbContext(typeof(InfiniLoreDbContext))]
    partial class InfiniLoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "d957c0f8-e90e-4068-a968-4f4b49fc165c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bcb90b69-a743-4c2e-b9b6-6aadeaeaba38",
                            Email = "testuser@example.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "TESTUSER@EXAMPLE.COM",
                            NormalizedUserName = "TESTUSER",
                            PasswordHash = "AQAAAAIAAYagAAAAEF9DotKZHyyCk7hsoitWWz1XPso6L+kNOyu4PIZy8FNNF8usuG4EwuvcaLmHt5/Sow==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d957c0f8-e90e-4068-a968-4f4b49fc165b",
                            TwoFactorEnabled = false,
                            UserName = "testuser"
                        });
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.Account.JwtRefreshTokenModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExpiresInDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.PrimitiveCollection<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Roles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TokenHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TokenHash")
                        .IsUnique();

                    b.ToTable("JwtRefreshTokens");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.LoreScopeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("IsDiscoverable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPubliclyReadable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.HasIndex("Name", "OwnerId")
                        .IsUnique();

                    b.HasIndex("Id", "OwnerId", "IsPubliclyReadable");

                    b.ToTable("LoreScopes");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.MultiverseModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("IsDiscoverable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPubliclyReadable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LoreScopeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LoreScopeId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("Name", "LoreScopeId")
                        .IsUnique();

                    b.HasIndex("Id", "OwnerId", "IsPubliclyReadable");

                    b.ToTable("Multiverses");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.UniverseModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("IsDiscoverable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPubliclyReadable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MultiverseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MultiverseId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("Name", "MultiverseId")
                        .IsUnique();

                    b.HasIndex("Id", "OwnerId", "IsPubliclyReadable");

                    b.ToTable("Universes");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserContentAccessModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AccessKind")
                        .HasColumnType("decimal(20,0)");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LoreScopeModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MultiverseModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UniverseModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ContentId")
                        .IsUnique();

                    b.HasIndex("LoreScopeModelId");

                    b.HasIndex("MultiverseModelId");

                    b.HasIndex("UniverseModelId");

                    b.HasIndex("UserId");

                    b.HasIndex("ContentId", "UserId")
                        .IsUnique();

                    b.ToTable("UserContentAccesses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "2995026b-5735-497a-91e5-503e0a5db3d9",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "f41634bf-d6e8-4d25-9096-bfc99ed7bd82",
                            Name = "user",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.Account.JwtRefreshTokenModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", "Owner")
                        .WithMany("JwtRefreshTokens")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.LoreScopeModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", "Owner")
                        .WithMany("LoreScopes")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.MultiverseModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.UserData.LoreScopeModel", "LoreScope")
                        .WithMany("Multiverses")
                        .HasForeignKey("LoreScopeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", "Owner")
                        .WithMany("Multiverses")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoreScope");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.UniverseModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.UserData.MultiverseModel", "Multiverse")
                        .WithMany("Universes")
                        .HasForeignKey("MultiverseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", "Owner")
                        .WithMany("Universes")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Multiverse");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserContentAccessModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.UserData.LoreScopeModel", null)
                        .WithMany("UserAccess")
                        .HasForeignKey("LoreScopeModelId");

                    b.HasOne("InfiniLore.Server.Data.Models.Content.UserData.MultiverseModel", null)
                        .WithMany("UserAccess")
                        .HasForeignKey("MultiverseModelId");

                    b.HasOne("InfiniLore.Server.Data.Models.Content.UserData.UniverseModel", null)
                        .WithMany("UserAccess")
                        .HasForeignKey("UniverseModelId");

                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", "User")
                        .WithMany("ContentAccesses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.Account.InfiniLoreUser", b =>
                {
                    b.Navigation("ContentAccesses");

                    b.Navigation("JwtRefreshTokens");

                    b.Navigation("LoreScopes");

                    b.Navigation("Multiverses");

                    b.Navigation("Universes");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.LoreScopeModel", b =>
                {
                    b.Navigation("Multiverses");

                    b.Navigation("UserAccess");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.MultiverseModel", b =>
                {
                    b.Navigation("Universes");

                    b.Navigation("UserAccess");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Content.UserData.UniverseModel", b =>
                {
                    b.Navigation("UserAccess");
                });
#pragma warning restore 612, 618
        }
    }
}
