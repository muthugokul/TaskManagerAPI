using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
                throw;
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
                var task = await this.taskService.Get(id);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateTask task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.taskService.Create(TaskMapper.Map(task));

                return Ok();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="task">The task</param>
        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] Model.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.taskService.Update(task);

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the task for the specified fields.
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("{id}/end")]
        [Produces("application/json")]
        public async Task<IActionResult> EndTask(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.taskService.EndTask(id);

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
