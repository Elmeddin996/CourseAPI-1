using CourseApi.DAL;
using CourseApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public GroupsController(CourseDbContext context)
        {
            _context = context;
        }

        [HttpPost("")]
        public ActionResult Create(Group group)
        {
            if (_context.Groups.Any(x => x.No == group.No))
            {
                ModelState.AddModelError("No", "No is already exist");
                return BadRequest(ModelState);
            }

            _context.Groups.Add(group);
            _context.SaveChanges();

            return StatusCode(201, new { Id = group.Id });
        }

        [HttpGet("")]
        public ActionResult<List<Group>> GetAll()
        {
            var data = _context.Groups.ToList();

            return Ok(data);
        }

        [HttpPut("")]
        public ActionResult Edit(Group group)
        {
            Group existGroup = _context.Groups.Find(group.Id);

            if (existGroup == null) return NotFound();

            if (group.No != existGroup.No && _context.Groups.Any(x => x.No == group.No))
            {
                ModelState.AddModelError("Name", "Name is already taken");
                return NotFound(ModelState);
            }

            existGroup.No = group.No;

            _context.Groups.Add(existGroup);
            _context.SaveChanges();

            return StatusCode(201, new { Id = group.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Group group = _context.Groups.Find(id);

            if (group == null) return StatusCode(404);

            _context.Groups.Remove(group);
            _context.SaveChanges();

            return StatusCode(200);
        }
    }
}
