using Autofac;
using Autofac.Integration.Mvc;
using SavedMessages.BusinessLogic.Managers;
using SavedMessages.DataAccessLayer;
using SavedMessages.DataAccessLayer.Entities;
using SavedMessages.Repositories.Interfaces;
using SavedMessages.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SavedMessages
{
    public class Injection
    {
        public static void Inject()
        {
            ProjectContext context = new ProjectContext();
            var builder = new ContainerBuilder();


            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<Manager<User>>().As<IManager<User>>();
            builder.RegisterInstance<IRepository<User>>(
                new Repository<User>(context));

            builder.RegisterType<Manager<Message>>().As<IManager<Message>>();
            builder.RegisterInstance<IRepository<Message>>(
                new Repository<Message>(context));

            builder.RegisterType<Manager<FileLocation>>().As<IManager<FileLocation>>();
            builder.RegisterInstance<IRepository<FileLocation>>(
                new Repository<FileLocation>(context));

            builder.RegisterType<Manager<Permissions>>().As<IManager<Permissions>>();
            builder.RegisterInstance<IRepository<Permissions>>(
                new Repository<Permissions>(context));

            builder.RegisterType<Manager<Sticker>>().As<IManager<Sticker>>();
            builder.RegisterInstance<IRepository<Sticker>>(
                new Repository<Sticker>(context));

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}