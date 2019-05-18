using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data.Interface;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository<Model.Task> taskRepository;

        public TasksController(ITaskRepository<Model.Task> taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns></returns>
        // GET api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            IEnumerable<Model.Task> tasks = this.taskRepository.GetAll();
            return Ok(tasks);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Model.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await this.taskRepository.Create(task);

            return CreatedAtAction("POST ", new { id = task.Id }, task);
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Updates the task for the specified fields.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string value)
        {
        }
    }
}
