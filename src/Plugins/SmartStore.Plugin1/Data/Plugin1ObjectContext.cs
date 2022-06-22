using SmartStore.Core;
using SmartStore.Data;
using SmartStore.Data.Setup;
using SmartStore.Plugin1.Data.Migrations;
using SmartStore.Plugin1.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SmartStore.Plugin1.Data
{
    /// <summary>
    /// Object context
    /// </summary>
    public class Plugin1ObjectContext : ObjectContextBase
    {
        public const string ALIASKEY = "sm_object_context_Plugin1";

        static Plugin1ObjectContext()
        {
            var initializer = new MigrateDatabaseInitializer<Plugin1ObjectContext, Configuration>
            {
                TablesToCheck = new[] { "Plugin1" }
            };
            Database.SetInitializer(initializer);
        }

        /// <summary>
        /// For tooling support, e.g. EF Migrations
        /// </summary>
        public Plugin1ObjectContext()
            : base()
        {
        }

        public Plugin1ObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString, ALIASKEY)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plugin1Record>();

            //disable EdmMetadata generation
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}