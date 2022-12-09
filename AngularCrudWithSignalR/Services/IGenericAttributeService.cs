using AngularCrudWithSignalR.Data.Entites;

namespace AngularCrudWithSignalR.Services
{
    public interface IGenericAttributeService
    {
        Task<GenericAttribute> GetGenericAttributeAsync(string key, int customerId);
        Task InsertGenericAttributeAsync(GenericAttribute genericAttribute);
        Task DeleteGenericAttributeAsync(GenericAttribute genericAttribute);
        Task UpdateGenericAttributeAsync(GenericAttribute genericAttribute);
        Task DeleteAllGenericAttributeAsync(int customerId);

        Task UpdateAllGenericAttribute(int customerId, Dictionary<string, string> dic);
    }
}
