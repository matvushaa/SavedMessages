using SavedMessages.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.Repositories.Interfaces
{
    public interface IAccountRepository<TEntity> where TEntity : class
    {
        TEntity GetAccount(LoginVM loginVm);

        bool IsExistedByName(string login);

        void EmailConfirm(Guid id);
    }
}
