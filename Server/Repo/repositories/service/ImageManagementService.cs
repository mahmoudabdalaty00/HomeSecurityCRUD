using Microsoft.Extensions.FileProviders;
using Server.Repo.interfaces;

namespace Server.Repo.repositories.service
{
    public class ImageManagementService : IImageManagementService
    {

        private readonly IFileProvider _fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<List<string>> AddImagesAsync(IFormFileCollection files, string src)
        {
            try
            {
                var saveImageSrc = new List<string>();
                var safeSrc = GetSafeFolderName(src);
                var imageDirectory = Path.Combine("wwwroot", "Images", safeSrc);
                if (Directory.Exists(imageDirectory) is not true)
                {
                    Directory.CreateDirectory(imageDirectory);
                }
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = file.FileName;
                        var filePath = $"Images/{safeSrc}/{fileName}";

                        var root = Path.Combine(imageDirectory, fileName);

                        using (var stream = new FileStream(root, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        saveImageSrc.Add(filePath);
                    }
                    else
                    {
                        throw new Exception("File is empty");
                    }

                }
                return saveImageSrc;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while uploading image: {ex.Message}");
            }
        }

        public Task DeleteImageAsync(string src)
        {

            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            if (info.Exists)
            {
                File.Delete(root);
                return Task.CompletedTask;
            }
            else
            {
                throw new Exception("File not found");
            }
        }

        private string GetSafeFolderName(string name)
        {
            name = name.Trim();
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name.Replace(" ", "_");
        }
    }
}
