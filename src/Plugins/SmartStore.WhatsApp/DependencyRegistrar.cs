using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.Web.Controllers;
using SmartStore.WhatsApp.Data;
using SmartStore.WhatsApp.Domain;
using SmartStore.WhatsApp.Filters;
using SmartStore.WhatsApp.Services;

namespace SmartStore.WhatsApp
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<WhatsAppService>().As<IWhatsAppService>().InstancePerRequest();

            //register named context
            builder.Register<IDbContext>(c => new WhatsAppObjectContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(WhatsAppObjectContext.ALIASKEY)
                .InstancePerRequest();

            builder.Register(c => new WhatsAppObjectContext(DataSettings.Current.DataConnectionString))
                .InstancePerRequest();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<WhatsAppRecord>>()
                .As<IRepository<WhatsAppRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(WhatsAppObjectContext.ALIASKEY))
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
