using AngularCrudWithSignalR.Data.Entites;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace AngularCrudWithSignalR.Services
{
    public class GenericAttributeService : IGenericAttributeService
    {

        #region fields

        private readonly IRepository<GenericAttribute> _genericAttributeRepository;

        #endregion

        #region ctor

        public GenericAttributeService(IRepository<GenericAttribute> genericAttributeRepository)
        {
            _genericAttributeRepository = genericAttributeRepository;
        }

        #endregion

        #region methods

        public virtual async Task<GenericAttribute> GetGenericAttributeAsync(string key, int customerId)
        {
            var query = from g in _genericAttributeRepository.Table
                        where g.CustomerId == customerId
                        && g.Key == key
                        select g;

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task InsertGenericAttributeAsync(GenericAttribute genericAttribute)
        {
            if (genericAttribute == null)
                throw new ArgumentNullException(nameof(genericAttribute));

            await _genericAttributeRepository.InsertAsync(genericAttribute);
        }

        public virtual async Task UpdateGenericAttributeAsync(GenericAttribute genericAttribute)
        {
            if (genericAttribute == null)
                throw new ArgumentNullException(nameof(genericAttribute));

            await _genericAttributeRepository.UpdateAsync(genericAttribute);
        }

        public virtual async Task DeleteGenericAttributeAsync(GenericAttribute genericAttribute)
        {
            if (genericAttribute == null)
                throw new ArgumentNullException(nameof(genericAttribute));

            await _genericAttributeRepository.DeleteAsync(genericAttribute);
        }

        public virtual async Task DeleteAllGenericAttributeAsync(int customerId)
        {
            var query = from g in _genericAttributeRepository.Table
                        where g.CustomerId == customerId
                        select g;

            foreach (var attri in query)
                await DeleteGenericAttributeAsync(attri);

        }

        public virtual async Task UpdateAllGenericAttribute(int customerId, Dictionary<string, string> dic)
        {
            foreach (var item in dic)
            {
                var genericAttribute = await GetGenericAttributeAsync(item.Key, customerId);
                if (genericAttribute != null)
                {
                    genericAttribute.Value = item.Value;
                    await UpdateGenericAttributeAsync(genericAttribute);
                }
            }
        }

        #endregion

    }
}
