using AngularCrudWithSignalR.Data.Entites;
using Data.Repository;
using System.Data;

namespace AngularCrudWithSignalR.Services
{
    public partial class DownloadService : IDownloadService
    {

        #region fields

        private readonly IRepository<Download> _DownloadRepository;

        #endregion

        #region ctor

        public DownloadService(IRepository<Download> DownloadRepository)
        {
            _DownloadRepository = DownloadRepository;
        }

        #endregion

        #region methods

        public virtual async Task<Download> GetDownloadAsync(int id)
        {
            if (id < 0)
                throw new NoNullAllowedException();

           return await _DownloadRepository.GetByIdAsync(id);
        }

        public virtual async Task UpdateDownloadAsync(Download Download)
        {
            if (Download == null)
                throw new NoNullAllowedException();

            await _DownloadRepository.UpdateAsync(Download);
        }

        public virtual async Task InsertDownloadAsync(Download Download)
        {
            if (Download == null)
                throw new NoNullAllowedException();

            await _DownloadRepository.InsertAsync(Download);
        }

        public virtual async Task DeleteDownloadAsync(Download Download)
        {
            if (Download == null)
                throw new NoNullAllowedException();

            await _DownloadRepository.DeleteAsync(Download);
        }

        public virtual async Task<Download> GetDownloadById(int DownloadId)
        {
            //if (DownloadId <= 0)
            //    throw new NoNullAllowedException();

            return await _DownloadRepository.GetByIdAsync(DownloadId);
        }

        public virtual async Task<byte[]> GetDownloadBitsAsync(IFormFile file)
        {
            await using var fileStream = file.OpenReadStream();
            await using var ms = new MemoryStream();
            await fileStream.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            return fileBytes;
        }

        #endregion
    }
}
