using Microsoft.AspNetCore.Mvc;
using Uniaraxa.Domain.Entities;
using Uniaraxa.Domain.Interfaces;
using Uniaraxa.Service.Services;

namespace Uniaraxa.API.Controllers
{
    [ApiController]
    
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        [HttpGet]
        [Route("api/teacher/{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound("Teacher not found");
            }
            return Ok(teacher);
        }

        [HttpGet]
        [Route("api/teacher")]
        public IActionResult GetAllTeachers()
        {
            var teacher = _teacherService.GetAllTeachers();
                if (teacher == null || !teacher.Any())
            {
                return NotFound("No teachers found");
            }
            return Ok(teacher);
        }

        [HttpPost]
        [Route("api/teacher")]
        public IActionResult AddTeacher([FromBody] Teacher teacher)
        {
            _teacherService.AddTeacher(teacher);
            return CreatedAtAction(nameof(GetTeacherById), new {id = teacher.Id }, teacher);
        }

        [HttpDelete]
        [Route("api/teacher/{id}")]
        public IActionResult DeleteTeacherById(int id)
        {
            _teacherService.DeleteTeacher(id);
            return NoContent();
        }

        [HttpPut]
        [Route("api/teacher/{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            var existing = _teacherService.GetTeacherById(id);
            if (existing == null)
            {
                return NotFound("Teacher not found");
            }

            teacher.Id = id;
            _teacherService.UpdateTeacher(teacher);
            return NoContent();
        }
    }
}
