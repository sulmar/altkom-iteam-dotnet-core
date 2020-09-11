using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.WebApi.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthorizationService authorizationService;

        public BasicAuthenticationHandler(
            IAuthorizationService authorizationService,
            IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.authorizationService = authorizationService;
        }

        private const string authorizationKey = "Authorization";

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(authorizationKey))
            {
                return AuthenticateResult.Fail("Missing authorization header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[authorizationKey]);

            if (authHeader.Scheme!="Basic")
            {
                return AuthenticateResult.Fail("Invalid scheme");
            }

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

            var username = credentials[0];
            var password = credentials[1];

            if (authorizationService.TryAuthenticate(username, password, out Customer customer))
            {
                ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);

                Claim claim1 = new Claim("Kat", "B");
                Claim roleClaim1 = new Claim(ClaimTypes.Role, "Trainer");
                Claim roleClaim2 = new Claim(ClaimTypes.Role, "Developer");

                Claim phoneClaim = new Claim(ClaimTypes.MobilePhone, "555-666-777");
                Claim emailClaim = new Claim(ClaimTypes.Email, customer.Email);

                identity.AddClaim(claim1);
                identity.AddClaim(roleClaim1);
                identity.AddClaim(roleClaim2);
                identity.AddClaim(emailClaim);

                
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }


            // TODO: ....



            return AuthenticateResult.Fail("Not implemented");
            


            




        }
    }
}
