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
    [Route("api/scores")]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreRepository _scoreRepository;

        public ScoresController(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        // GET: api/scores
        [HttpGet]
        [ProducesResponseType(typeof(List<L_Score>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            if (search == null)
            {
                List<L_Score> scoresAll = await _scoreRepository.GetAllScores();
                string json = JsonSerializer.Serialize(scoresAll);
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
            //L_Score score = _scoreRepository.
            //return Ok(score);
        }

        // GET: api/scores/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(L_Score), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<L_Score>> GetById(int id)
        {
            if (await _scoreRepository.GetScoreById(id) is L_Score score)
            {
                string json = JsonSerializer.Serialize(score);
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            return NotFound();
        }

        // POST: api/scores
        [HttpPost]
        [ProducesResponseType(typeof(L_Score), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(L_Score score)
        {
            _scoreRepository.AddScore(score);
            return CreatedAtAction(nameof(GetById), new { id = score.ScoreId }, score);
        }

        // PUT: api/scores/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] L_Score score)
        {
            // successful update for PUT returns 204 No Content with empty body, or 200 OK
            if (await _scoreRepository.GetScoreById(id) is L_Score oldScore)
            {
                oldScore.ScoreId = score.ScoreId;
                return NoContent();
                //return StatusCode(204);
            }
            return NotFound();
        }

        // DELETE: api/scores/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            // successful DELETE returns 204 No Content with empty body
            if (await _scoreRepository.GetScoreById(id) is L_Score score)
            {
                await _scoreRepository.DeleteScoreById(score.ScoreId);
                return NoContent();
            }
            return NotFound();
        }
    }
}
