using DemoWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTagsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserTagsController(MyDbContext context)
        {
            _context = context;
        }


        //[HttpPost("create-user-tag")]
        //public async Task<IActionResult> CreateUserTag([FromBody] UserTags newUserTag)
        //{
        //    if (newUserTag == null)
        //    {
        //        return BadRequest("Invalid tag data.");
        //    }

        //    try
        //    {
        //        var existingUserTag = await _context.UserTags.FirstOrDefaultAsync(t => t.UserId == newUserTag.UserId && t.TagId == newUserTag.TagId);
        //        if (existingUserTag != null)
        //        {
        //            return Conflict("UserId và TagId đã tồn tại !");
        //        }
        //        _context.UserTags.Add(newUserTag);
        //        await _context.SaveChangesAsync();

        //        return StatusCode(201);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        [HttpPost("insert-user-tag")]
        public async Task<IActionResult> InsertUserTag(long userId, string tagName)
        {
            try
            {
                var user = await _context.UserProfile.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null)
                {
                    return NotFound($"User với ID: {userId} không tồn tại.");
                }

                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
                if (tag == null)
                {
                    return NotFound($"Không tìm thấy '{tagName}'.");
                }

                var existingUserTag = await _context.UserTags
                    .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TagId == tag.TagId);

                if (existingUserTag != null)
                {
                    return Conflict($"Bảng User-Tag đã tồn tại User ID: {userId} và Tag Name: {tagName}.");
                }

                var newUserTag = new UserTags
                {
                    UserId = userId,
                    TagId = tag.TagId
                };

                _context.UserTags.Add(newUserTag);
                await _context.SaveChangesAsync();

                return StatusCode(201); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete("delete-by-userid-tagname")]
        public async Task<IActionResult> DeleteByUserIdAndTagName(long userId, string tagName)
        {
            try
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
                if (tag == null)
                {
                    return NotFound($"Không tìm thấy '{tagName}'.");
                }

                var userTagsToDelete = await _context.UserTags
                    .Where(ut => ut.UserId == userId && ut.TagId == tag.TagId)
                    .ToListAsync();

                if (userTagsToDelete.Count == 0)
                {
                    return NotFound($"Không có bản ghi UserTags nào có User ID: {userId} và Tag Name: {tagName}.");
                }

                _context.UserTags.RemoveRange(userTagsToDelete);
                await _context.SaveChangesAsync();

                return Ok($"Xóa thành công !");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
