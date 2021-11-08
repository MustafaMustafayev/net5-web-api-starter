using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Core.Utility
{
    public class UtilService : IUtilService
    {
        private readonly IConfiguration _configuration;

        public UtilService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int GetUserIdFromToken(string tokenString)
        {
            var jwtEncodedString = tokenString.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            int userId = Convert.ToInt32(token.Claims.First(c => c.Type == "nameid").Value);
            return userId;
        }

        public bool IsValidToken(string tokenString)
        {
            if(string.IsNullOrEmpty(tokenString) || tokenString.Length < 7)
            {
                return false;
            }
            tokenString = tokenString.Substring(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:SecretKey").Value);
            try
            {
                tokenHandler.ValidateToken(tokenString, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                // return true from JWT token if validation successful
                return true;
            }
            catch (Exception ex)
            {
                // return false if validation fails
                return false;
            }
        }
    }
}
