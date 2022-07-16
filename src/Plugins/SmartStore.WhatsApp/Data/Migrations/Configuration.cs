using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SmartStore.WhatsApp.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SmartStore.WhatsApp.Data.WhatsAppObjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
            ContextKey = "SmartStore.WhatsApp"; // DO NOT CHANGE!
        }

        protected override void Seed(SmartStore.WhatsApp.Data.WhatsAppObjectContext context)
        {
        }
    }
}
