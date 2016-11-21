using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;

using ComicBookInventory.Domain.Abstract;
using ComicBookInventory.Domain.ComicBook;
using ComicBookInventory.Data.Abstract;
using ComicBookInventory.Data.Access;


namespace ComicBookInventory.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IAccess>().To<SqlAccess>();
            kernel.Bind<IIssueAccess>().To<ComicBookIssueAccess>();
            kernel.Bind<IComicBook>().To<ComicBookService>();
        }
    }
}
