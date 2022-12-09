using AngularCrudWithSignalR.Data.Entites;
using Data.Repository;

namespace AngularCrudWithSignalR.Services
{
    public partial class StateService : IStateService
    {
        #region fields

        private readonly IRepository<State> _stateRepository;

        #endregion

        #region ctor

        public StateService(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        #endregion

        #region methods

        public async Task DeleteStateAsync(State state)
        {
            await _stateRepository.DeleteAsync(state);
        }

        public async Task<State> GetStateById(int stateId)
        {
            if (stateId < 0)
                throw new ArgumentOutOfRangeException(nameof(stateId));

            return await _stateRepository.GetByIdAsync(stateId);
        }

        public async Task InsertStateAsync(State state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            await _stateRepository.InsertAsync(state);
        }

        public async Task UpdateStateAsync(State state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            await _stateRepository.UpdateAsync(state);
        }

        #endregion
    }
}
