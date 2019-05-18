using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Business.Interface;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IService<Model.Task> taskService;

        public TasksController(IService<Model.Task> taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns>List of tasks</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await this.taskService.GetAll();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Gets the task for the specified task id.
        /// </summary>
        /// <returns>A task.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var tasks = await this.taskService.Get(id);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Model.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.taskService.Create(task);

                return CreatedAtAction("POST ", new { id = task.Id }, task);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            
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
