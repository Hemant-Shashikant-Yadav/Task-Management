using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server3.Data;
using Server3.DTOs;
using Server3.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server3.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _Db;
        public UserController(ApplicationDbContext Db)
        {
            _Db = Db;
        }

        [HttpGet("api/users")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var data = await _Db.User.ToListAsync();
            return Ok(data);
        }

        [HttpGet("api/users/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var data = await _Db.User.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

            [HttpPost("api/user/login")]
            public async Task<ActionResult<User>> LoginUser(LoginRequest loginRequest)
            {
            var data = await _Db.User.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (data == null || data.Password != loginRequest.Password)
            {
                return NotFound(new { message = "Invalid email or password" });
            }
            return Ok(data);

        }

        [HttpPost("api/users/create")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _Db.User.AddAsync(user);
            await _Db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("api/users/update/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _Db.Entry(user).State = EntityState.Modified;
            await _Db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("api/users/delete/{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var data = await _Db.User.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            _Db.User.Remove(data);
            await _Db.SaveChangesAsync();
            return Ok(data);
        }
    }
}
