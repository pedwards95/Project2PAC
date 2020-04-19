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
    [Route("api/Games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _GameRepository;

        public GamesController(IGameRepository GameRepository)
        {
            _GameRepository = GameRepository;
        }

        // GET: api/Games
        [HttpGet]
        [ProducesResponseType(typeof(List<L_Game>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            if (search == null)
            {
                List<L_Game> GamesAll = await _GameRepository.GetAllGames();
                string json = JsonSerializer.Serialize(GamesAll);
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
                // (200 OK response, with the notes serialized in the response body -- instead of some view's HTML)
            }
            // TODO:
            throw new NotImplementedException();
            //L_Game Game = _GameRepository.
            //return Ok(Game);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(L_Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<L_Game>> GetById(int id)
        {
            if (await _GameRepository.GetGameById(id) is L_Game Game)
            {
                string json = JsonSerializer.Serialize(Game);
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            return NotFound();
        }

        // POST: api/Games
        [HttpPost]
        [ProducesResponseType(typeof(L_Game), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(L_Game Game)
        {
            _GameRepository.AddGame(Game);
            return CreatedAtAction(nameof(GetById), new { id = Game.GameId }, Game);
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] L_Game Game)
        {
            // successful update for PUT returns 204 No Content with empty body, or 200 OK
            if (await _GameRepository.GetGameById(id) is L_Game oldGame)
            {
                oldGame.GameId = Game.GameId;
                return NoContent();
                //return StatusCode(204);
            }
            return NotFound();
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            // successful DELETE returns 204 No Content with empty body
            if (await _GameRepository.GetGameById(id) is L_Game Game)
            {
                await _GameRepository.DeleteGameById(Game.GameId);
                return NoContent();
            }
            return NotFound();
        }
    }
}