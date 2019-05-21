using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManagerApi.Business.Interface;
using TaskManagerApi.Model.Contracts;
using TaskManagerApi.Model.Mapper;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IService<Model.Task> taskService;
        private readonly ILogger<TasksController> logger;

        public TasksController(IService<Model.Task> taskService, ILogger<TasksController> logger)
        {
            this.taskService = taskService;
            this.logger = logger;
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
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the task for the specified task id.
        /// </summary>
        /// <returns>A task.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var task = await this.taskService.Get(id);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateTask createTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newTask = TaskMapper.Map(createTask);
                await this.taskService.Create(newTask);

                return Ok(newTask.Id);
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="id">The task id to update</param>
        /// <param name="updateTask">Task to update</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTask updateTask)
        {
            if (id != updateTask.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entity = await this.taskService.Get(id);
                if (entity == null)
                {
                    return NotFound();
                }

                await this.taskService.Update(TaskMapper.Map(entity, updateTask));

                return NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the task for the specified fields.
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("{id}/end")]
        [Produces("application/json")]
        public async Task<IActionResult> EndTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entity = await this.taskService.Get(id);
                if (entity == null)
                {
                    return NotFound();
                }

                await this.taskService.EndTask(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
