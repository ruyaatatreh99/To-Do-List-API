
using Microsoft.AspNetCore.Mvc;
using ToDo.Model;
using ToDo.services;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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
        public IActionResult createtask([FromBody] Dictionary<string, string> data) {
            try
            {
                Tasks ToDoTask = _ToDo.createtask(data["title"], data["description"], Int16.Parse(data["IsCompleted"]), data["Category"]);
                if (ToDoTask == null) return NotFound(new { errors = "Error in creating Task!!" });
                else return Ok(new { Task = ToDoTask });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }

        [Route("tasks/")]
        [HttpPut]
        public IActionResult updatetask([FromBody] Dictionary<string, string> data,int taskid)
        {
            try
            {
                Tasks ToDoTask = _ToDo.updatetask(data["title"], data["description"], Int16.Parse(data["IsCompleted"]), data["Category"], taskid);
                if (ToDoTask == null) return NotFound(new { errors = "Error in updating Task!!" });
                else return Ok(new { Task = ToDoTask });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        
        [Route("tasks/all")]
        [HttpGet]
        public IActionResult viewAlltaskes()
        {
            try
            {
               List<Tasks> taskes = _ToDo.viewAlltaskes();
                return Ok(new { tskes = taskes });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        
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

    }
}
