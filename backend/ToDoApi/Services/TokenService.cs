using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class TokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string Create(User user)
        {
            var handle = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.TokenKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(8),
                Subject = GenareteClaims(user)
            };
            var token = handle.CreateToken(tokenDescriptor);
            return handle.WriteToken(token);
        }

        private static ClaimsIdentity GenareteClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.UserName));
            ci.AddClaim(new Claim("UserId", user.Id));

            return ci;
        }

        public string GetUserId()
        {
            var jwt = new JwtSecurityTokenHandler();
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(token))
            {
                token = token.Substring("Bearer ".Length).Trim();

                if (jwt.ReadJwtToken(token).Payload.TryGetValue("UserId", out var userId))
                    return userId.ToString()!;
            }

            return null!;
        }
    }
}
