using HR.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Application.Service.Helpers
{
    internal class GenerateJwtToken
    {

        public string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySecretKeyForJWTTokenGeneration"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                // Add more claims as needed for authorization purposes
            };
            var expirationTime = DateTime.UtcNow.AddHours(1); // For example, setting the expiration to 1 hour from now


            var token = new JwtSecurityToken(

                issuer: "HR",
                audience: "YourAudience",
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;


        }
    }
}
