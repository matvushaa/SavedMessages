using SavedMessages.DataAccessLayer;
using SavedMessages.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SavedMessages.Api
{
    public class UserRepository
    {
        //ScryptEncoder encoder = new ScryptEncoder();
        private readonly ProjectContext context = new ProjectContext();
        public User Get(string login, string password)
        {
            return context.User
                .Where(a => a.Email == login
                     && a.Password == password).FirstOrDefault();
        }
    }
}