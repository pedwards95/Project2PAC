using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _2PAC.Domain.LogicModel;
using _2PAC.Domain.Interfaces;
using _2PAC.WebApp.WebAPIModel;
using _2PAC.DataAccess.Repositories;


namespace _2PAC.WebApp.Controllers
{
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
                string json = JsonSerializer.Serialize(usersAll);
                return Ok(usersAll);
                //return new ContentResult
                //{
                //    StatusCode = 200,
                //    ContentType = "application/json",
                //    Content = json
                //};
                // (200 OK response, with the notes serialized in the response body -- instead of some view's HTML)
            }
            // TODO:
            throw new NotImplementedException();
            //L_User user = _userRepository.
            //return Ok(user);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(L_User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<L_User>> GetById(int id)
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
            return NotFound();
        }

        // POST: api/users
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
                oldUser.PictureId = user.PictureId;
                oldUser.Admin = user.Admin;
                oldUser.Description = user.Description;
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;
                oldUser.Password = user.Password;
                oldUser.Username = user.Username;
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
    }
}
