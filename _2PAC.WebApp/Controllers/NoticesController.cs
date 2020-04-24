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
    [Route("api/notices")]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepository _noticeRepository;

        public NoticesController(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }

        // GET: api/notices
        [HttpGet]
        [ProducesResponseType(typeof(List<L_Notice>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            if (search == null)
            {
                List<L_Notice> noticesAll = await _noticeRepository.GetAllNotices();
                string json = JsonSerializer.Serialize(noticesAll);
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
            //L_Notice notice = _noticeRepository.
            //return Ok(notice);
        }

        // GET: api/notices/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(L_Notice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<L_Notice>> GetById(int id)
        {
            if (await _noticeRepository.GetNoticeById(id) is L_Notice notice)
            {
                string json = JsonSerializer.Serialize(notice);
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            return NotFound();
        }

        // POST: api/notices
        [HttpPost]
        [ProducesResponseType(typeof(L_Notice), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(L_Notice notice)
        {
            _noticeRepository.AddNotice(notice);
            return CreatedAtAction(nameof(GetById), new { id = notice.NoticeId }, notice);
        }

        // PUT: api/notices/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] L_Notice notice)
        {
            // successful update for PUT returns 204 No Content with empty body, or 200 OK
            if (await _noticeRepository.GetNoticeById(id) is L_Notice oldNotice)
            {
                await _noticeRepository.UpdateNotice(notice);
                return NoContent();
                //return StatusCode(204);
            }
            return NotFound();
        }

        // DELETE: api/notices/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            // successful DELETE returns 204 No Content with empty body
            if (await _noticeRepository.GetNoticeById(id) is L_Notice notice)
            {
                await _noticeRepository.DeleteNoticeById(notice.NoticeId);
                return NoContent();
            }
            return NotFound();
        }
    }
}