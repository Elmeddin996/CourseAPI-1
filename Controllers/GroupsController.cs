using CourseApi.DAL;
using CourseApi.Dtos.GroupDtos;
using CourseApi.Dtos;
using CourseApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        
        /// <summary>
        /// Get all groups by selected page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult<List<GroupGetAllDto>> GetAll(int page = 1)
        {
            var query = _context.Groups.Include(x => x.Students).AsQueryable();

            var items = query.Skip((page - 1) * 4).Take(4).Select(x => new GroupGetAllDto { Id = x.Id, No = x.No, StudentsCount = x.Students.Count}).ToList();
            var totalPages = (int)Math.Ceiling(query.Count() / 4d);

            var data = new PaginatedListDto<GroupGetAllDto>(items, totalPages, page);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            var data = _context.Groups.Include(x => x.Students).Include(x => x.Teachers).FirstOrDefault(x => x.Id == id);

            if (data == null)
                return NotFound();

            GroupGetDto groupDto = new GroupGetDto
            {
                Id = data.Id,
                No = data.No,
                Students = data.Students.Select(x => new StudentItemInGroupGetDto { Id = x.Id, FullName = x.FullName, AvgPoint = x.AvgPoint }).ToList(),
                Teachers = data.Teachers.Select(x=> new TeacherItemInGroupGetDto { Id = x.Id, FullName= x.FullName,Subject = x.Subject }).ToList()
            };

            return Ok(groupDto);
        }

        [HttpPost("")]
        public ActionResult Create(GroupPostDto groupDto)
        {
            if (_context.Groups.Any(x => x.No == groupDto.No))
            {
                ModelState.AddModelError("No", "No is already exist");
                return BadRequest(ModelState);
            }

            Group group = new Group
            {
                No = groupDto.No,
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return StatusCode(201, new { Id = group.Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupDto"></param>
        /// <returns></returns>
        /// <response code="204">Data updated</response>
        /// <response code="404">Data not found</response>
        /// <response code="400">Data is not valid</response>
        /// 
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Edit(int id, GroupPutDto groupDto)
        {
            Group group = _context.Groups.Find(id);

            if (group == null)
                return NotFound();

            if (group.No != groupDto.No && _context.Groups.Any(x => x.No == groupDto.No))
            {
                ModelState.AddModelError("No", "No is already exist");
                return BadRequest(ModelState);
            }

            group.No = groupDto.No;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Group group = _context.Groups.Find(id);
            if (group == null)
                return NotFound();

            _context.Groups.Remove(group);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
