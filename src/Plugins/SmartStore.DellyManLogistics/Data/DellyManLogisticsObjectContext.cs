using SmartStore.Core;
using SmartStore.Core.Domain.Directory;
using SmartStore.Data;
using SmartStore.Data.Setup;
using SmartStore.DellyManLogistics.Data.Migrations;
using SmartStore.DellyManLogistics.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace SmartStore.DellyManLogistics.Data
{
    /// <summary>
    /// Object context
    /// </summary>
    public class DellyManLogisticsObjectContext : ObjectContextBase
    {
        public const string ALIASKEY = "sm_object_context_DellyManLogistics";

        static DellyManLogisticsObjectContext()
        {
            var initializer = new MigrateDatabaseInitializer<DellyManLogisticsObjectContext, Configuration>
            {
                TablesToCheck = new[] { "DellyManLogistics" }
            };
           
            Database.SetInitializer(initializer);
        }

       
        /// <summary>
        /// For tooling support, e.g. EF Migrations
        /// </summary>
        public DellyManLogisticsObjectContext()
            : base()
        {
        }

        public DellyManLogisticsObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString, ALIASKEY)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DellyManLogisticsRecord>();

            //disable EdmMetadata generation
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}