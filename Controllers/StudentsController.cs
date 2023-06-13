using CourseApi.DAL;
using CourseApi.Dtos.StudentDtos;
using CourseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public StudentsController(CourseDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> Get(int id)
        {
            var data = _context.Students.Include(x => x.Group).FirstOrDefault(x => x.Id == id);

            if (data == null)
                return NotFound();

            var studentDto = new StudentGetDto
            {
                Id = id,
                FullName = data.FullName,
                Email = data.Email,
                AvgPoint = data.AvgPoint,
                Group = new GroupInStudentGetDto
                {
                    Id = data.GroupId,
                    No = data.Group.No
                }
            };

            return Ok(studentDto);
        }

        [HttpPost("")]
        public ActionResult Create(StudentPostDto studentDto)
        {
            if (!_context.Groups.Any(x => x.Id == studentDto.GroupId))
            {
                ModelState.AddModelError("GroupId", "Group not found");
                return BadRequest(ModelState);
            }

            if (_context.Students.Any(x => x.Email == studentDto.Email))
            {
                ModelState.AddModelError("Email", "Email is already taken");
                return BadRequest(ModelState);
            }

            Student std = new Student
            {
                FullName = studentDto.FullName,
                Email = studentDto.Email,
                AvgPoint = studentDto.AvgPoint,
                GroupId = studentDto.GroupId,
            };

            _context.Students.Add(std);
            _context.SaveChanges();
            return StatusCode(201, new { Id = std.Id });
        }

        [HttpGet("")]
        public ActionResult<List<StudentGetAllDto>> GetAll()
        {
            var data = _context.Students.ToList();

            return Ok(data);
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, StudentPutDto student)
        {
            Student existStudent = _context.Students.Find(id);

            if (existStudent == null) return NotFound();


            existStudent.FullName = existStudent.FullName;

            _context.Students.Add(existStudent);
            _context.SaveChanges();

            return StatusCode(201, new { Id = id});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = _context.Students.Find(id);

            if (student == null) return StatusCode(404);

            _context.Students.Remove(student);
            _context.SaveChanges();

            return StatusCode(200);
        }
    }
}
