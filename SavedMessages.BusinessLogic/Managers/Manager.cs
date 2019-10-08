using SavedMessages.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.BusinessLogic.Managers
{
    public class Manager<TEntity> : IManager<TEntity> where TEntity : class
    {
        public readonly IRepository<TEntity> repo;
        public Manager() { }
        public Manager(IRepository<TEntity> repository)
        {
            repo = repository;
        }
        public void Create(TEntity entity)
        {
            repo.Create(entity);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public void Delete(Guid id)
        {
            repo.Delete(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return repo.GetAll();
        }

        public TEntity GetById(int id)
        {
            return repo.GetById(id);
        }

        public TEntity GetById(Guid guid)
        {
            return repo.GetById(guid);
        }

        public void Edit(int id, TEntity entity)
        {
            repo.Edit(id, entity);
        }

        public void Edit(Guid id, TEntity entity)
        {
            repo.Edit(id, entity);
        }

        public void Update(TEntity entity)
        {
            repo.Update(entity);
        }
    }
}
