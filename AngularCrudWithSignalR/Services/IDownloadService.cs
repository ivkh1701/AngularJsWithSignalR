using AngularCrudWithSignalR.Data.Entites;

namespace AngularCrudWithSignalR.Services
{
    public interface IDownloadService
    {
        Task<Download> GetDownloadAsync(int id);
        Task InsertDownloadAsync(Download download);
        Task DeleteDownloadAsync(Download download);
        Task<Download> GetDownloadById(int downloadId);
        Task<byte[]> GetDownloadBitsAsync(IFormFile file);

    }
}
