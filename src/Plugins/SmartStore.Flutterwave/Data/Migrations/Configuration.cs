using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SmartStore.Flutterwave.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SmartStore.Flutterwave.Data.FlutterwaveObjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
            ContextKey = "SmartStore.Flutterwave"; // DO NOT CHANGE!
        }

        protected override void Seed(SmartStore.Flutterwave.Data.FlutterwaveObjectContext context)
        {
        }
    }
}
