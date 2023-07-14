using ControlDiv.API.Data;
using ControlDiv.Shared.Entities;

namespace ControlDiv.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _dataContext;

        public AccountRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

       
    }
}
