using SavedMessages.DataAccessLayer;
using SavedMessages.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.Repositories.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ProjectContext context;
        private readonly DbSet<TEntity> table;

        public Repository(ProjectContext _context)
        {
            context = _context;
            table = _context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            try
            {
                table.Add(entity);
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    string message = "Object: " + validationError.Entry.Entity.ToString();

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        message = message + err.ErrorMessage + "";
                    }
                    message += 1;
                }

            }
        }

        public TEntity GetById(int id)
        {
            return table.Find(id);
        }

        public TEntity GetById(Guid id)
        {
            return table.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return table.ToList();
        }

        public void Delete(int id)
        {
            var existing = table.Find(id);
            table.Remove(existing);
            context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var existing = table.Find(id);
            table.Remove(existing);
            context.SaveChanges();
        }
        
        public void Edit(int id, TEntity entity)
        {
            var mode = table.Find(id);
            if (mode == null)
            {
                return;
            }
            context.Entry(mode).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public void Edit(Guid id, TEntity entity)
        {
            var mode = table.Find(id);
            if (mode == null)
            {
                return;
            }
            context.Entry(mode).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            var entry = context.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Modified:
                    context.Set<TEntity>().Add(entity);
                    break;
                case EntityState.Added:
                    context.Set<TEntity>().Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
