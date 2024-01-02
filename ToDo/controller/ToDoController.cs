
using Microsoft.AspNetCore.Mvc;
using ToDo.Model;
using ToDo.services;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ToDo.controller
{
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoInterface _ToDo ;
        public ToDoController(ToDoInterface ToDo)
        {
            _ToDo = ToDo;

        }
       
        [Route("tasks/")]
        [HttpPost]
        public IActionResult createtask([FromBody] Tasks data) {
            try
            {
                Tasks ToDoTask = _ToDo.createtask(data.Title,data.Description,data.IsCompleted, data.Category);
                if (ToDoTask == null) return NotFound(new { errors = "Error in creating Task!!" });
                else 
                {
                    var token = GenerateJwtToken(data.Title);
                    return Ok(new { Task = ToDoTask, Token = token });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Authorize]
        [Route("tasks/")]
        [HttpPut]
        public IActionResult updatetask([FromBody] Tasks data, int taskid)
        {
            try
            {
                Tasks ToDoTask = _ToDo.updatetask(data.Title, data.Description,data.IsCompleted, data.Category, taskid);
                if (ToDoTask == null) return NotFound(new { errors = "Error in updating Task!!" });
                else return Ok(new { Task = ToDoTask });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Authorize]
        [Route("tasks/all")]
        [HttpGet]
        public IActionResult viewAlltaskes()
        {
            try
            {
               var taskes = _ToDo.viewAlltaskes();
                return Ok(new { tskes = taskes });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Authorize]
        [Route("tasks/")]
        [HttpGet]
        public IActionResult viewtask(int taskid)
        {
            try
            {
                Tasks ToDoTask = _ToDo.viewtask(taskid);
                if (ToDoTask == null) return NotFound(new { errors = "Error ! Not Found" });
                else return Ok(new { Task = ToDoTask });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Authorize]
        [Route("tasks/")]
        [HttpDelete]
        public IActionResult deletetask(int taskid)
        {
            try
            {
                _ToDo.deletetask(taskid);
                return Ok(new { });
            }
            catch (Exception )
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }

        private string GenerateJwtToken(string title)
        { 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("your-issuer", "your-audience", new[]
            {
        new Claim(ClaimTypes.Name, title)
    }, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
