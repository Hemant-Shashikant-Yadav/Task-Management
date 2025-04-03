using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server3.Data;
using Server3.DTOs;
using Server3.Models;
using Task = Server3.Models.Task;

namespace Server3.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _Db;
        public TaskController(ApplicationDbContext Db)
        {
            _Db = Db;
        }

        [HttpGet("api/task/{id}")]
        public async Task<ActionResult<List<Task>>> GetTaskByUserId(int id)
        {
            var tasks = await _Db.Task.Where(t => t.UserId == id).ToListAsync();
            if (tasks == null || tasks.Count == 0)
            {
                return NotFound();
            }
            return Ok(tasks);
        }

        [HttpPost("api/task/create")]
        public async Task<ActionResult<Task>> CreateUser(Task tt)
        {

            if (tt == null)
            {
                return BadRequest();
            }

           
            await _Db.Task.AddAsync(tt);
            await _Db.SaveChangesAsync();
            return Ok(tt);
        }


        [HttpPut("api/task/{taskId}")]
        public async Task<ActionResult> UpdateTaskStatus(int taskId, [FromBody] UpdateTaskStatus updateTaskStatusDto)
        {
            var task = await _Db.Task.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            task.Status = updateTaskStatusDto.Status;
            _Db.Entry(task).State = EntityState.Modified;
            await _Db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("api/task/delete/{taskId}")]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            var task = await _Db.Task.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            _Db.Task.Remove(task);
            await _Db.SaveChangesAsync();

            return NoContent();
        }
    }
}
