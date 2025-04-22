using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RythuKooliAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTypeController : ControllerBase
    {
        private readonly IWorkTypeRepository _workTypeRepository;

        public WorkTypeController(IWorkTypeRepository workTypeRepository)
        {
            _workTypeRepository = workTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkType>>> GetAll()
        {
            return Ok(await _workTypeRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkType>> GetById(int id)
        {
            var workType = await _workTypeRepository.GetByIdAsync(id);
            if (workType == null) return NotFound();
            return Ok(workType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkType workType)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _workTypeRepository.AddAsync(workType);
            return CreatedAtAction(nameof(GetById), new { id = workType.Id }, workType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkType workType)
        {
            if (id != workType.Id) return BadRequest();
            await _workTypeRepository.UpdateAsync(workType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _workTypeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
