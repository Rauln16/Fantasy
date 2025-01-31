using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Security.Claims;

namespace Fantasy.Frontend.AuthenticationProviders;

public class AuthenticationProviderTest : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonimous = new ClaimsIdentity();
        var user = new ClaimsIdentity(authenticationType: "test");
        var admin = new ClaimsIdentity(new List<Claim>
        {
            new Claim("firstName", "Raul"),
            new Claim("lastName", "Nieto"),
            new Claim(ClaimTypes.Name, "raulnietogarcia16@gmail.com"),
            new Claim(ClaimTypes.Role, "Admin")
        },
        authenticationType: "test");

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));
    }
}