using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NXWalks.API.Models.DTO;
using NXWalks.API.Repositories;

namespace NXWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //POST method URL : /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };
             //Just provide the user name and password then Microsoft.identity will take care of creating a user
            var identityResults = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if(identityResults.Succeeded)
            {
                //Checking if the string array has any roles then assign them to the user
                if (registerRequestDTO.Roles !=null && registerRequestDTO.Roles.Any())
                {
                    //Now we want to add roles to this created user
                    identityResults = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    //Now Afer Assigning the roles to user the check if its succeeded then print messege
                    if (identityResults.Succeeded)
                    {
                        return Ok("User Has been Registered...! Please Login..!");
                    }
                }

            }

            return BadRequest("Opss..! Something Went Wrong");  

        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
              var CheckPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(CheckPassword)
                {
                    //First get the roles of the user 
                    var roles = await userManager.GetRolesAsync(user);
                    //now Check if roles are not null then create teh token 
                    if(roles != null)
                    {
                      var jwtToken =  tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JWTToken = jwtToken,
                        };
                        return Ok(response);

                    }

                }
            }

            return BadRequest("Incorrect UserName Or Password...!");
        }
    }
}
