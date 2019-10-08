using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.BusinessLogic.Managers
{
    public interface IManager<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        TEntity GetById(Guid guid);

        void Edit(int id, TEntity entity);

        void Edit(Guid id, TEntity entity);

        void Delete(int id);

        void Delete(Guid id);

        void Create(TEntity entity);

        void Update(TEntity entity);
    }
}
