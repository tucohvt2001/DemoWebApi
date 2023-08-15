using DemoWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public TagController(MyDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("get-tags")]
        public async Task<IActionResult> GetTag()
        {
            var tags = await _context.Tags.ToListAsync();
            return Ok(tags);
        }

        [HttpPost("create-tag")]
        public async Task<IActionResult> CreateTag([FromBody] Tags newTag)
        {
            if (newTag == null)
            {
                return BadRequest("Invalid tag data.");
            }

            try
            {
                var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == newTag.TagName);
                if (existingTag != null)
                {
                    return Conflict("Tag with the same name already exists.");
                }

                _context.Tags.Add(newTag);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTag), new { id = newTag.TagId }, newTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("get-tags-from-api")]
        public async Task<IActionResult> GetTagFromApi()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var accessToken = "9NzzImKBgLaYUtf20GYhKIPySqK54jvMIXX9FZSpmKHqHc0M9JNL1K520aa5IRC_VsKOImrSgZ5ANn9P9qY0JmbjMJT7R85sPdPOEnTQ-N8vHLG3Lstp439gQcf74lvxDZqjA7CAzMj9855_FoBr25a88MLw4PGeEJC1Qqmgp2Gk16zBQpV2N10T4qnz5ySB2pHlG74mnIWXGareRZFw02e7BcfD3Oqx2pGhK6a1_WiwArHgIJp_7Gb9NNDOPTPf6tDY6LH7pWiGKNbDQc3sMYL1H284G-znLd54P31Xw7ziKamb6cUOIZWH8Y9POAmmF7K1Mte3enyfSNDmJaIaFnT0u2864QvT"; // Replace with your actual access token
            httpClient.DefaultRequestHeaders.Add("access_token", accessToken);

            var apiEndpoint = "https://openapi.zalo.me/v2.0/oa/tag/gettagsofoa";
            var response = await httpClient.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonContent);

                if (jsonObject.TryGetProperty("data", out var dataArray) && dataArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in dataArray.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.String)
                        {
                            var tagName = item.GetString();

                            var existingTag = _context.Tags.FirstOrDefault(tag => tag.TagName == tagName);

                            if (existingTag == null)
                            {
                                var newTag = new Tags { TagName = tagName };
                                _context.Tags.Add(newTag);
                            }
                        }
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return Ok("Thành công!");
                }
                else
                {
                    return BadRequest("Không có data!");
                }
            }
            else
            {
                return BadRequest("Lỗi!");
            }
        }

        [HttpDelete("delete-by-tagname")]
        public async Task<IActionResult> DeleteTagByTagName(string tagName)
        {
            try
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
                if (tag == null)
                {
                    return NotFound($"Không tìm thấy '{tagName}'.");
                }
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();

                return Ok($"Xóa thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("update-tag/{tagId}")]
        public async Task<IActionResult> UpdateTag(int tagId, [FromBody] Tags updatedTag)
        {
            try
            {
                var existingTag = await _context.Tags.FindAsync(tagId);

                if (existingTag == null)
                {
                    return NotFound($"Tag với ID {tagId} không tồn tại.");
                }

                // Kiểm tra xem tên thẻ được cập nhật đã tồn tại trong cơ sở dữ liệu chưa
                var isTagNameTaken = await _context.Tags.AnyAsync(t => t.TagId != tagId && t.TagName == updatedTag.TagName);
                if (isTagNameTaken)
                {
                    return Conflict($"Tagname '{updatedTag.TagName}' đã tồn tại.");
                }

                // cập nhật với values mới
                existingTag.TagName = updatedTag.TagName;

                _context.Tags.Update(existingTag);
                await _context.SaveChangesAsync();

                return Ok($"Tag với ID {tagId} đã cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}