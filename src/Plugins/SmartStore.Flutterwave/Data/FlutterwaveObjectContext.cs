using SmartStore.Core;
using SmartStore.Data;
using SmartStore.Data.Setup;
using SmartStore.Flutterwave.Data.Migrations;
using SmartStore.Flutterwave.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SmartStore.Flutterwave.Data
{
    /// <summary>
    /// Object context
    /// </summary>
    public class FlutterwaveObjectContext : ObjectContextBase
    {
        public const string ALIASKEY = "sm_object_context_Flutterwave";

        static FlutterwaveObjectContext()
        {
            var initializer = new MigrateDatabaseInitializer<FlutterwaveObjectContext, Configuration>
            {
                TablesToCheck = new[] { "Flutterwave" }
            };
            Database.SetInitializer(initializer);
        }

        /// <summary>
        /// For tooling support, e.g. EF Migrations
        /// </summary>
        public FlutterwaveObjectContext()
            : base()
        {
        }

        public FlutterwaveObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString, ALIASKEY)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlutterwaveRecord>();

            //disable EdmMetadata generation
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}