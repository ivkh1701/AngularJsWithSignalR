using AngularCrudWithSignalR.Data.Entites;

namespace AngularCrudWithSignalR.Services
{
    public interface IStateService
    {
        Task InsertStateAsync(State state);
        Task DeleteStateAsync(State state);
        Task UpdateStateAsync(State state);
        Task<State> GetStateById(int stateId);
    }
}
