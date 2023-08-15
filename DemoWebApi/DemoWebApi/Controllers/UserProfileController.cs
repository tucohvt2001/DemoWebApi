using DemoWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserProfileController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-user-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var users = await _context.UserProfile.ToListAsync();
            return Ok(users);
        }

        [HttpGet("get-users-by-tag")]
        public async Task<IActionResult> GetUsersByTagName(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return BadRequest("Tag name cannot be empty.");
            }

            try
            {
                var usersWithTag = await _context.UserProfile
                .Join(_context.UserTags, u => u.UserId, ut => ut.UserId, (u, ut) => new { UserProfile = u, UserTag = ut })
                .Join(_context.Tags, uut => uut.UserTag.TagId, t => t.TagId, (uut, t) => new { uut.UserProfile, uut.UserTag, Tag = t })
                .Where(ut => ut.Tag.TagName == tagName)
                .Select(uut => uut.UserProfile)
                .ToListAsync();

                return Ok(usersWithTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
