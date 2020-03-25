using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public class TokenDecoder
    {

        public static string GetUserRole(HttpRequest request)
        {
            string role = "";

            if (request.Headers.TryGetValue("Authorization", out var accessToken)) {
                accessToken = accessToken.ToString().Replace("Bearer ", string.Empty);
                var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                var roleClaim = token.Claims.FirstOrDefault(r => r.Type == "https://property.com/roles");
                if(roleClaim != null)
                {
                    role = roleClaim.Value;
                }
                else
                {
                    role = "Guest";
                }
            }
            else
            {
                role = "Guest";
            }

            return role;
        }

        public static void PrintClaims(HttpRequest request)
        {
            var accessToken = request.Headers["Authorization"];
            accessToken = accessToken.ToString().Replace("Bearer ", string.Empty);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var claimList = token.Claims.ToList();

            foreach (var item in claimList)
            {
                Console.WriteLine(item);
            }
        }

        public static string GetUserEmail(HttpRequest request)
        {
            string email = "";

            if (request.Headers.TryGetValue("Authorization", out var accessToken))
            {
                accessToken = accessToken.ToString().Replace("Bearer ", string.Empty);
                var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                var emailClaim = token.Claims.FirstOrDefault(c => c.Type == "email");
                if (emailClaim != null)
                {
                    email = emailClaim.Value;
                }
            }
            return email;
        }
    }
}
