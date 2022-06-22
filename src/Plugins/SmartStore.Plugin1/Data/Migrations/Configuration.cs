using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SmartStore.Plugin1.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SmartStore.Plugin1.Data.Plugin1ObjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
            ContextKey = "SmartStore.Plugin1"; // DO NOT CHANGE!
        }

        protected override void Seed(SmartStore.Plugin1.Data.Plugin1ObjectContext context)
        {
        }
    }
}
