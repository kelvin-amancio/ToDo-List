using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Repositories.Interfaces;
using ToDoApi.ViewModels;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskItemController(ITaskItemRepository taskItemRepository) : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;

        [HttpGet()]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _taskItemRepository.GetAllAsync(pageNumber, pageSize);

            if (result.Tasks.Any())
                return Ok(result);

            return NotFound();
        }

        [HttpPost()]
        public async Task<IActionResult> Create(TaskItemViewModel taskItemViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var result = await _taskItemRepository.AddAsync(taskItemViewModel);

            if (result.Data is not null)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(TaskItemViewModel taskItemViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var result = await _taskItemRepository.UpdateAsync(taskItemViewModel);

            if (result.Data is not null)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _taskItemRepository.DeleteByIdAsync(id);

            if (result.Data is not null)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
