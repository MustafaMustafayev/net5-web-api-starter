using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.ActionFilters;
using BLL.Services.IServices;
using Core.Constants;
using Core.Utility;
using DTO.DTOs;
using DTO.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogActionFilter))]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [SwaggerOperation(Summary = "login endpoint, check login credentials, if success, return user data and token string")]
        /// <summary>
        /// login endpoint, check login credentials, if success, return user data and token string
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            try
            {
                string userSalt = await _authService.GetUserSalt(loginDTO.Username);
                if (string.IsNullOrEmpty(userSalt))
                {
                    return BadRequest(new SuccessDataResult<Result>(Messages.UserDoesNotExist));
                }
                loginDTO.Password = SecurityHelper.HashPassword(loginDTO.Password, userSalt);
                IDataResult<UserToListDTO> userDTO = await _authService.Login(loginDTO);
                if (!userDTO.Success)
                {
                    return Ok(userDTO);
                }
                DateTime expirationDate = DateTime.Now.AddHours(3);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userDTO.Data.UserId.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, userDTO.Data.Username.ToString()));
                claims.Add(new Claim(ClaimTypes.Expiration, expirationDate.ToString()));

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:SecretKey").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expirationDate,
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenValue = tokenHandler.WriteToken(token);

                LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
                {
                    Token = tokenValue,
                    User = userDTO.Data
                };

                return Ok(new SuccessDataResult<LoginResponseDTO>(loginResponseDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDataResult<Result>(Messages.GeneralError));
            }
        } 
    }
}
