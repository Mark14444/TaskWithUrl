using Inforce.Service.Dto.UrlDtos;
using Inforce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inforce.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _service;
        public UrlController(IUrlService service)
        {
            _service = service;
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetUrlById(int id)
        {
            var url = await _service.GetUrlByIdAsync(id);
            return Ok(url);
        }
        [HttpGet("get-by-short-url")]
        public async Task<IActionResult> GetUrlByShortUrl(string shortUrl)
        {
            var url = await _service.GetUrlByShortName(shortUrl);
            if(url == null) return NotFound();
            return Ok(url);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateShortUrl(CreateShortUrlDto url)
        {
            await _service.AddAsync(url);
            return Ok();
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUrls()
        {
            var urls = await _service.GetAllUrlsAsync();
            return Ok(urls);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            return await _service.DeleteAsync(id) ? Ok():BadRequest();
        }
    }
}
