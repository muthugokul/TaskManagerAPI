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
    public class ParentTasksController : ControllerBase
    {
        private readonly IService<Model.ParentTask> parentTaskService;

        public ParentTasksController(IService<Model.ParentTask> parentTaskService)
        {
            this.parentTaskService = parentTaskService;
        }

        /// <summary>
        /// Retrieves all parent tasks.
        /// </summary>
        /// <returns>List of parent tasks</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await this.parentTaskService.GetAll();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
