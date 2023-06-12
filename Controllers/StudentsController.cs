using CourseApi.DAL;
using CourseApi.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("")]
        public ActionResult Create(Student student,int groupId)
        {
            if (!_context.Groups.Any(x => x.Id == groupId))
            {
                ModelState.AddModelError("groupId", "GroupId is not correct");
                return BadRequest(ModelState);
            }

            student.GroupId = groupId;

            _context.Students.Add(student);
            _context.SaveChanges();

            return StatusCode(201, new { Id = student.Id, GroupId = student.GroupId });
        }

        [HttpGet("")]
        public ActionResult<List<Student>> GetAll()
        {
            var data = _context.Students.ToList();

            return Ok(data);
        }

        [HttpPut("")]
        public ActionResult Edit(Student student)
        {
            Student existStudent = _context.Students.Find(student.Id);

            if (existStudent == null) return NotFound();


            existStudent.FullName = existStudent.FullName;

            _context.Students.Add(existStudent);
            _context.SaveChanges();

            return StatusCode(201, new { Id = student.Id });
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
