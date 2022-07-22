using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.Flutterwave.Data;
using SmartStore.Flutterwave.Domain;
using SmartStore.Flutterwave.Filters;
using SmartStore.Flutterwave.Services;
using SmartStore.Web.Controllers;

namespace SmartStore.Flutterwave
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<FlutterwaveService>().As<IFlutterwaveService>().InstancePerRequest();

            //register named context
            builder.Register<IDbContext>(c => new FlutterwaveObjectContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(FlutterwaveObjectContext.ALIASKEY)
                .InstancePerRequest();

            builder.Register(c => new FlutterwaveObjectContext(DataSettings.Current.DataConnectionString))
                .InstancePerRequest();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<FlutterwaveRecord>>()
                .As<IRepository<FlutterwaveRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(FlutterwaveObjectContext.ALIASKEY))
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
