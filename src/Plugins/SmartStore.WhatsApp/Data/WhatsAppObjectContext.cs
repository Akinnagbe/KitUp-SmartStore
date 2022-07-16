using SmartStore.Core;
using SmartStore.Data;
using SmartStore.Data.Setup;
using SmartStore.WhatsApp.Data.Migrations;
using SmartStore.WhatsApp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SmartStore.WhatsApp.Data
{
    /// <summary>
    /// Object context
    /// </summary>
    public class WhatsAppObjectContext : ObjectContextBase
    {
        public const string ALIASKEY = "sm_object_context_WhatsApp";

        static WhatsAppObjectContext()
        {
            var initializer = new MigrateDatabaseInitializer<WhatsAppObjectContext, Configuration>
            {
                TablesToCheck = new[] { "WhatsApp" }
            };
            Database.SetInitializer(initializer);
        }

        /// <summary>
        /// For tooling support, e.g. EF Migrations
        /// </summary>
        public WhatsAppObjectContext()
            : base()
        {
        }

        public WhatsAppObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString, ALIASKEY)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WhatsAppRecord>();

            //disable EdmMetadata generation
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}