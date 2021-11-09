using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using UserService.Exceptions;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenGeneratorService tokenGeneratorService;
        public UserController(IUserService service, ITokenGeneratorService tokenGeneratorService)
        {
            this.userService = service;
            this.tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(User user)
        {
            try
            {
                return Ok(userService.RegisterUser(user));
            }
            catch (UserNameAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (WeakPasswordException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult LoginUser(User user)
        {
            try
            {
                if (userService.LoginUser(user))
                {
                    var result = userService.GetUserByUserName(user.UserName);
                    return Ok(tokenGeneratorService.GenerateJWTToken(user.UserName, result.Role));
                }
                else
                {
                    return StatusCode(401, "Invalid UserId or Password");
                }
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidCredentialsException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPut("update")]
        //[Authorize(Roles = "CUSTOMER")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                return Ok(userService.UpdateUser(user.UserName, user.Password, user));
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("updaterole")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult UpdateRoleOfUser(User user)
        {
            try
            {
                return Ok(userService.UpdateRoleofUser(user.UserName, user.Role));
            }
            catch (UserNotFoundException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("getuser")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult GetUsers()
        {
            try
            {
                return Ok(userService.GetUsers());
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
