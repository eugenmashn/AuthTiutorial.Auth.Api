using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthTiutorial.Auth.Api.Models;
using AuthTutorial.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;


namespace AuthTiutorial.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;
        public AuthController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }


        private List<Account> Accounts => new List<Account>
        {
            new Account()
            {
                Id = Guid.Parse("7aa1eb39-9fcf-4bb9-be84-42a18fac65db"),
                Email = "user@gmail.com",
                Passwword = "user",
                Roles = new Role[]{Role.User}
            },
            new Account()
            {
                Id = Guid.Parse("6ed37231-7e84-478b-a490-aad1b8c7054b"),
                Email = "user2@gmail.com",
                Passwword = "user2",
                Roles = new Role[]{Role.User}
            }
            ,
            new Account()
            {
                Id = Guid.Parse("f15c00b5-bdf2-4466-bd0f-18c8603ef77e"),
                Email = "user3@gmail.com",
                Passwword = "user3",
                Roles = new Role[]{Role.User}
            }
        };

        [Route("login")]
        [HttpPost]
        public IActionResult Login( [FromBody] Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);
            if(user != null)
            {
                var token = GenerateJWT(user);
                return Ok(
                    new
                    {
                        access_token = token
                    }) ;
            }

            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Passwword == password);
        }
        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            foreach( var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
