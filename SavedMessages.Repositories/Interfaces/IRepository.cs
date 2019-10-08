using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        void Edit(int id, TEntity entity);

        void Edit(Guid id, TEntity entity);

        void Delete(int id);

        void Delete(Guid id);

        void Create(TEntity entity);

        TEntity GetById(Guid guid);

        void Update(TEntity entity);
    }
}
