using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexleInterviewTesting.Infrastructure.Helpers
{
    public static class TokenHelpers
    {
        /// <summary>
        /// Generate a JWT access token by claims, secrets key, issue, etc,...
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public static string GenerateJwtToken(IEnumerable<Claim> claims, string key, string issuer, string audience, TimeSpan expires)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.Add(expires),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
