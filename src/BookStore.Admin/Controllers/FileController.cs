using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var filePath = await _fileService.UploadImageAsync(file);
            var fileUrl = _fileService.GetImageUrl(Path.GetFileName(filePath));
            return Ok(new { path = filePath, url = fileUrl });
        }
    }
}
