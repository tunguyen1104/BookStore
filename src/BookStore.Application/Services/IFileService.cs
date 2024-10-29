using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Services
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file);
        bool DeleteImage(string imageName);
        string GetImageUrl(string imageName);
    }
}
