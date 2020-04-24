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
    [Route("api/gameData")]
    public class gameDataController : ControllerBase
    {
        private readonly IGameDataRepository _gameDataRepository;

        public gameDataController(IGameDataRepository gameDataRepository)
        {
            _gameDataRepository = gameDataRepository;
        }

        // GET: api/gameData
        [HttpGet]
        [ProducesResponseType(typeof(List<L_GameData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            if (search == null)
            {
                List<L_GameData> gameDataAll = await _gameDataRepository.GetAllGameData();
                string json = JsonSerializer.Serialize(gameDataAll);
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
            //L_GameData gameData = _gameDataRepository.
            //return Ok(gameData);
        }

        // GET: api/gameData/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(L_GameData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<L_GameData>> GetById(int id)
        {
            if (await _gameDataRepository.GetGameDataById(id) is L_GameData gameData)
            {
                string json = JsonSerializer.Serialize(gameData);
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            return NotFound();
        }

        // POST: api/gameData
        [HttpPost]
        [ProducesResponseType(typeof(L_GameData), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(L_GameData gameData)
        {
            _gameDataRepository.AddGameData(gameData);
            return CreatedAtAction(nameof(GetById), new { id = gameData.DataId }, gameData);
        }

        // PUT: api/gameData/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] L_GameData gameData)
        {
            // successful update for PUT returns 204 No Content with empty body, or 200 OK
            if (await _gameDataRepository.GetGameDataById(id) is L_GameData oldGameData)
            {
                await _gameDataRepository.UpdateGameData(gameData);
                return NoContent();
                //return StatusCode(204);
            }
            return NotFound();
        }

        // DELETE: api/gameData/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            // successful DELETE returns 204 No Content with empty body
            if (await _gameDataRepository.GetGameDataById(id) is L_GameData gameData)
            {
                await _gameDataRepository.DeleteGameDataById(gameData.DataId);
                return NoContent();
            }
            return NotFound();
        }
    }
}