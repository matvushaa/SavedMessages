using SavedMessages.DataAccessLayer;
using SavedMessages.DataAccessLayer.Entities;
using SavedMessages.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Scrypt;

namespace SavedMessages.Repositories.Repositories
{
    public class AccountRepository<TEntity> : IAccountRepository<TEntity> where TEntity : User
    {
        ProjectContext _context;
        DbSet<TEntity> _dbSet;
        //ScryptEncoder encoder = new ScryptEncoder();

        public AccountRepository()
        {
            _context = new ProjectContext();
            _dbSet = _context.Set<TEntity>();

        }

        public AccountRepository(ProjectContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity GetAccount(LoginVM loginVm)
        {
            return _dbSet.Where(a => a.Email == loginVm.Email
                     && a.Password == loginVm.Password).FirstOrDefault();
        }

        public bool IsExistedByName(string login)
        {
            return _dbSet.Any(a => a.Email == login);

        }
        public bool IsAccountExists(string login)
        {
            return _dbSet.Any(a => a.Email == login);
        }

        public void EmailConfirm(Guid id)
        {
            var mode = _context.User.Find(id);
            var model = mode;
            model.IsVerifyed = true;
            _context.Entry(mode).CurrentValues.SetValues(model);
            _context.SaveChanges();
        }

        
    }
}
