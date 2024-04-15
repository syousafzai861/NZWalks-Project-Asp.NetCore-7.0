using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NXWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            
            //Create Claims What token will Claim 
            var Claims = new List<Claim>();

            //Now Adding in the Claims 
            Claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                //As We have List of Roles thats why using forEach to iterate over each role 
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            var crendentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Now as we have the claims key and credentials done now creating token 

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: crendentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
