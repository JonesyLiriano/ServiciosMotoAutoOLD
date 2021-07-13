using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ComplianceApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ComplianceApi.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace ComplianceApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {        
        private IRepositoryComplianceWrapper _repoWrapper;
        private readonly IConfiguration _configuration;
        public UsersController(IRepositoryComplianceWrapper repoWrapper, IConfiguration configuration)
        {
            _repoWrapper = repoWrapper;
            _configuration = configuration;
        }
        
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserPagingParameters userPagingParameters)
        {
            var users = await _repoWrapper.User.GetAllUsersAsync(userPagingParameters);
            var metadata = new
            {
                users.TotalCount,
                users.PageSize,
                users.CurrentPage,
                users.TotalPages,
                users.HasNext,
                users.HasPrevious

            };
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        }
        // GET: api/users/5
        [HttpGet("{userId}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _repoWrapper.User.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            return Ok(user);
        }
        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object.");
            }
            _repoWrapper.User.CreateUser(user);
            await _repoWrapper.SaveAsync();

            return CreatedAtRoute(
                  "GetUserById",
                  new { userId = user.UserId },
                  user);
        }
        // PUT: api/users/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var userToUpdate = await _repoWrapper.User.GetUserByIdAsync(userId);
            if (userToUpdate == null)
            {
                return NotFound("The user record couldn't be found.");
            }

            _repoWrapper.User.UpdateUser(userToUpdate, user);
            await _repoWrapper.SaveAsync();
            return NoContent();
        }
        // DELETE: api/users/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            User user = await _repoWrapper.User.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            _repoWrapper.User.DeleteUser(user);
            await _repoWrapper.SaveAsync();
            return NoContent();
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            if (_repoWrapper.User.AuthenticateUser(user))
            {
                var tokenString = await _repoWrapper.User.GenerateJWTAsync(user, _configuration);
                if (tokenString != "null")
                    return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }

    }
}
