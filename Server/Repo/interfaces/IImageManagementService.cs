namespace Server.Repo.interfaces
{
    public interface IImageManagementService
    {
        Task<List<string>> AddImagesAsync(IFormFileCollection files, string src);
        Task DeleteImageAsync(string src);
    }
}
