using SavedMessages.DataAccessLayer;
using SavedMessages.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.BusinessLogic.Managers
{
    public class AccountManager<TEntity> : IAccountManager<TEntity> where TEntity : class
    {
        private readonly IAccountRepository<TEntity> _repo;

        public AccountManager()
        {

        }

        public AccountManager(IAccountRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public TEntity GetAccount(LoginVM loginVm)
        {
            return _repo.GetAccount(loginVm);
        }

        public bool IsExistedByName(string login)
        {
            return _repo.IsExistedByName(login);
        }

        public void EmailConfirm(Guid id)
        {
            _repo.EmailConfirm(id);
        }


    }
}
