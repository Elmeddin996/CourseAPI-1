using CourseApi.DAL;
using CourseApi.Dtos.TeacherDtos;
using CourseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public TeachersController(CourseDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<TeacherGetDto> Get(int id)
        {
            var data = _context.Teachers.Include(x => x.Group).FirstOrDefault(x => x.Id == id);

            if (data == null)
                return NotFound();

            var teacherDto = new TeacherGetDto
            {
                Id = id,
                FullName = data.FullName,
                Subject = data.Subject,
                Group = new GroupInTeacherGetDto
                {
                    Id = data.GroupId,
                    No = data.Group.No
                }
            };

            return Ok(teacherDto);
        }

        [HttpPost("")]
        public ActionResult Create(TeacherPostDto teacherDto)
        {
            if (!_context.Groups.Any(x => x.Id == teacherDto.GroupId))
            {
                ModelState.AddModelError("GroupId", "Group not found");
                return BadRequest(ModelState);
            }

            
            Teacher teacher = new Teacher
            {
                FullName = teacherDto.FullName,
                Subject = teacherDto.Subject,
                GroupId = teacherDto.GroupId
            };

            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return StatusCode(201, new { Id = teacher.Id });
        }

        [HttpGet("")]
        public ActionResult<List<TeacherGetAllDto>> GetAll()
        {
            var data = _context.Teachers.ToList();

            return Ok(data);
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, TeacherPutDto teacher)
        {
            Teacher existTeacher = _context.Teachers.Find(id);

            if (existTeacher == null) return NotFound();


            existTeacher.FullName = existTeacher.FullName;

            _context.Teachers.Add(existTeacher);
            _context.SaveChanges();

            return StatusCode(201, new { Id = id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Teacher teacher = _context.Teachers.Find(id);

            if (teacher == null) return StatusCode(404);

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return StatusCode(200);
        }
    }
}
