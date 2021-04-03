using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Entities;
using Todo.Web.Services;

namespace Todo.Web.Controllers
{
    [Route("api/task/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(
            ITaskService taskService,
            IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tasks>> GetAll() => _taskService.GetTasks();

        [HttpGet("{id}", Name = nameof(GetById))]
        public ActionResult<IEnumerable<Tasks>> GetById(int id)
        {
            var task = _taskService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<Tasks> Post([FromBody] CreateTaskRequest request, CancellationToken cancellationToken = default)
        {
            var taskToCreate = _mapper.Map<Tasks>(request);

            var createdTask = _taskService.Create(taskToCreate);

            return CreatedAtRoute(nameof(GetById), new { id = createdTask.ID }, createdTask);
        }

        [HttpPut("{id}")]
        public ActionResult<Tasks> Put(int id, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken = default)
        {
            var task = _mapper.Map<Tasks>(request);

            var updateTask = _taskService.Update(id, task);

            return Ok(updateTask);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, CancellationToken cancellationToken = default)
        {
            _taskService.Delete(id);

            return NoContent();
        }
    }
}
