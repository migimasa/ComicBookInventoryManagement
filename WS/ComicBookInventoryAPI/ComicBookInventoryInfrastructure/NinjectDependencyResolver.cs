using System;
using Ninject;
using Ninject.Syntax;
using System.Web.Http.Dependencies;


using ComicBookInventory.Domain.Abstract;
using ComicBookInventory.Domain.ComicBook;
using ComicBookInventory.Data.Abstract;
using ComicBookInventory.Data.Access;


namespace ComicBookInventory.Infrastructure
{
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}
