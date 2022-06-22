using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using SmartStore.Core.Data;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Data;
using SmartStore.DellyManLogistics.Data;
using SmartStore.DellyManLogistics.Domain;
using SmartStore.DellyManLogistics.Services;
using System.Net.Http;

namespace SmartStore.DellyManLogistics
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<DellyManLogisticsService>().As<IDellyManLogisticsService>().InstancePerRequest();

            //register named context
            builder.Register<IDbContext>(c => new DellyManLogisticsObjectContext(DataSettings.Current.DataConnectionString))
                .Named<IDbContext>(DellyManLogisticsObjectContext.ALIASKEY)
                .InstancePerRequest();

            builder.Register(c => new DellyManLogisticsObjectContext(DataSettings.Current.DataConnectionString))
                .InstancePerRequest();

            builder.Register(c => new HttpClient()).InstancePerDependency();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<DellyManLogisticsRecord>>()
                .As<IRepository<DellyManLogisticsRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(DellyManLogisticsObjectContext.ALIASKEY))
                .InstancePerRequest();

        }

        public int Order
        {
            get { return 1; }
        }
    }
}
