using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Services.Impl
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _environment;
        private readonly string _baseDirectory;
        private readonly long _maxFileSize = 2 * 1024 * 1024;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        public FileService(IHostingEnvironment environment)
        {
            _environment = environment;
            _baseDirectory = Path.Combine(_environment.ContentRootPath, "../Assets");
            Directory.CreateDirectory(_baseDirectory);
        }

        public bool DeleteImage(string imageName)
        {
            try
            {
                string fullPath = Path.Combine(_baseDirectory, imageName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch
            (Exception ex)
            {
                return false;
            }
        }

        public string GetImageUrl(string imageName)
        {
            return Path.Combine("/assets", imageName);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is invalid or empty");
            }
            if (file.Length > _maxFileSize)
            {
                throw new ArgumentException($"File size exceeds the limit of {_maxFileSize}");
            }
            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("File type is not allowed");
            }
            string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            string filePath = Path.Combine(_baseDirectory, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
    }
}
