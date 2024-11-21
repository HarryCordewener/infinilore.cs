// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using FastEndpoints;
using InfiniLore.Server.Contracts.Services;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.Account;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace InfiniLore.Server.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IApiSignInService>(LifeTime.Scoped)]
public class ApiSignInService(SignInManager<InfiniLoreUser> signInManager, ILogger logger) : IApiSignInService {
    public async Task<UserIdentityResult> SignInAsync(string username, string password, CancellationToken ct = default) {
        if (await signInManager.UserManager.FindByNameAsync(username) is not {} user) {
            logger.Warning("User {@Username} not found", username);
            return "Invalid username";
        }

        if (!await signInManager.CanSignInAsync(user)) {
            logger.Warning("User {@Username} cannot sign in", username);
            return "Unable to sign in.";
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, password, false);
        switch (signInResult) {
            case { Succeeded: false, IsLockedOut: false }:
                logger.Warning("Invalid password for user {@Username}", username);
                return "Invalid username or password.";
            case { Succeeded: false, IsLockedOut: true }:
                logger.Warning("User {@Username} is locked out", username);
                return "User is locked out.";
        }

        logger.Information("User {@Username} signed in", username);
        return user;
    }
}
