using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using _2PAC.Domain.LogicModel;
using _2PAC.Domain.Interfaces;
using _2PAC.WebApp.WebAPIModel;
using _2PAC.DataAccess.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace _2PAC.WebApp.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        [ProducesResponseType(typeof(List<L_User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            if (search == null)
            {
                List<L_User> usersAll = await _userRepository.GetAllUsers();
                return Ok(usersAll);
            }
            List<L_User> users = await _userRepository.GetUserByString(search);
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(L_User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<L_User>> GetById(int id)
        {
            try
            {
                if (await _userRepository.GetUserById(id) is L_User user)
                {
                    string json = JsonSerializer.Serialize(user);
                    return new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = json
                    };
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NotFound();
        }

        // POST: api/users
        //[AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(L_User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(L_User user)
        {
            _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] L_User user)
        {
            // successful update for PUT returns 204 No Content with empty body, or 200 OK
            if (await _userRepository.GetUserById(id) is L_User oldUser)
            {
                await _userRepository.UpdateUser(user);
                return NoContent();
                //return StatusCode(204);
            }
            return NotFound();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            // successful DELETE returns 204 No Content with empty body
            if (await _userRepository.GetUserById(id) is L_User user)
            {
                await _userRepository.DeleteUserById(user.UserId);
                return NoContent();
            }
            return NotFound();
        }

        // [HttpPost("{username}, {password}")]
        // [ProducesResponseType(typeof(L_User), StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<IActionResult> Authenticate(string username, string password)
        // {
        //     L_User user = await _userRepository.GetUserByUsername(username);
        //     if(user.Password != password)
        //     {
        //         return BadRequest();
        //     }
        //     return Ok(user);
        // }


        //[AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await _userRepository.GetUserByUsername(model.Username);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            if(user.Password != model.Password)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_userRepository.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new W_User
            {
                UserId = user.UserId,
                PictureId = user.PictureId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Admin = user.Admin,
                Token = tokenString
            });
        }
    }
}