using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace digital.Frontend.AuthenticationProviders
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimous = new ClaimsIdentity();

            var admin = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Rocio"),
                new Claim("LastName", "Armando"),
                new Claim(ClaimTypes.Name, "rocio@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");



            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));
        }

    }
}
