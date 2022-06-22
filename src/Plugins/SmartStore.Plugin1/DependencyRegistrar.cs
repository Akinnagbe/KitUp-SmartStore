using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.Plugin1.Data;
using SmartStore.Plugin1.Domain;
using SmartStore.Plugin1.Filters;
using SmartStore.Plugin1.Services;
using SmartStore.Web.Controllers;

namespace SmartStore.Plugin1
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<Plugin1Service>().As<IPlugin1Service>().InstancePerRequest();

            //register named context
            builder.Register<IDbContext>(c => new Plugin1ObjectContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(Plugin1ObjectContext.ALIASKEY)
                .InstancePerRequest();

            builder.Register(c => new Plugin1ObjectContext(DataSettings.Current.DataConnectionString))
                .InstancePerRequest();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Plugin1Record>>()
                .As<IRepository<Plugin1Record>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(Plugin1ObjectContext.ALIASKEY))
                .InstancePerRequest();

            builder.RegisterType<SampleActionFilter>()
                .AsActionFilterFor<ProductController>(x => x.ProductDetails(default(int), default(string), null))
                .InstancePerRequest();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
