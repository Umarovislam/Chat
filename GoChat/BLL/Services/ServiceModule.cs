using GoChat.DAL.Interfaces;
using GoChat.DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}
