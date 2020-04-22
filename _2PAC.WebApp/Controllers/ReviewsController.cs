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
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/reviews
        [HttpGet]
        [ProducesResponseType(typeof(List<L_Review>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            if (search == null)
            {
                List<L_Review> reviewsAll = await _reviewRepository.GetAllReviews();
                string json = JsonSerializer.Serialize(reviewsAll);
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
            //L_Review review = _reviewRepository.
            //return Ok(review);
        }

        // GET: api/reviews/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(L_Review), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<L_Review>> GetById(int id)
        {
            if (await _reviewRepository.GetReviewById(id) is L_Review review)
            {
                string json = JsonSerializer.Serialize(review);
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            return NotFound();
        }

        // POST: api/reviews
        [HttpPost]
        [ProducesResponseType(typeof(L_Review), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(L_Review review)
        {
            _reviewRepository.AddReview(review);
            _reviewRepository.Save();
            return CreatedAtAction(nameof(GetById), new { id = review.ReviewId }, review);
        }

        // PUT: api/reviews/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] L_Review review)
        {
            // successful update for PUT returns 204 No Content with empty body, or 200 OK
            if (await _reviewRepository.GetReviewById(id) is L_Review oldReview)
            {
                oldReview.ReviewId = review.ReviewId;
                return NoContent();
                //return StatusCode(204);
            }
            return NotFound();
        }

        // DELETE: api/reviews/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            // successful DELETE returns 204 No Content with empty body
            if (await _reviewRepository.GetReviewById(id) is L_Review review)
            {
                await _reviewRepository.DeleteReviewById(review.ReviewId);
                return NoContent();
            }
            return NotFound();
        }
    }
}