using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Requests;
using TestRestCrudAPI.Response;
using TestRestCrudAPI.Services;

namespace TestRestCrudAPI.Controllers
{

    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request) {

            if (!ModelState.IsValid) {

                return BadRequest(new AuthFailedResponse { 
                Errors= ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResponse = await _userService.RegisterAsync(request.Email, request.Password, request.name);
            if (!authResponse.Success) {

                return BadRequest( new AuthFailedResponse
                {
                   Errors= authResponse.Errors
                });
            }

            return Ok(
                new AuthSuccessResponse
                {
                    Token= authResponse.Token
                }
                );
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResponse = await _userService.LoginAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {

                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(
                new AuthSuccessResponse
                {
                    Token = authResponse.Token
                }
                );
        }
    }
}
