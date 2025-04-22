using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RythuKooliAPI.Controllers
{
    [Route("api/work")]
    [ApiController]
    public class ConfirmController : ControllerBase
    {
        private readonly IConfirmRepository _confirmRepository;

        public ConfirmController(IConfirmRepository confirmRepository)
        {
            _confirmRepository = confirmRepository;
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmWork([FromBody] ConfirmWorkRequest request)
        {
            bool isConfirmed = await _confirmRepository.ConfirmWork(request.WorkId, request.UserId);
            if (!isConfirmed)
            {
                return BadRequest("Work already confirmed or not found.");
            }
            return Ok(new { message = "Work confirmed successfully!" });
        }
    }
}
