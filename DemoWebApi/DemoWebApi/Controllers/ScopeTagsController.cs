using DemoWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopeTagsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ScopeTagsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost("create-scope-tags")]
        public async Task<IActionResult> CreateScopeTag([FromBody] ScopeTags newScopeTag)
        {
            if (newScopeTag == null)
            {
                return BadRequest("Invalid tag data.");
            }

            try
            {
                var existingScopeTag = await _context.ScopeTags.FirstOrDefaultAsync(t => t.ScopeId == newScopeTag.ScopeId && t.TagId == newScopeTag.TagId);
                if (existingScopeTag != null)
                {
                    return Conflict("TagId và ScopeId đã tồn tại !");
                }
                _context.ScopeTags.Add(newScopeTag);
                await _context.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
