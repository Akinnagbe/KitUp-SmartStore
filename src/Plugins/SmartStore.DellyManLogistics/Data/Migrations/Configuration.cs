using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SmartStore.DellyManLogistics.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SmartStore.DellyManLogistics.Data.DellyManLogisticsObjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
            ContextKey = "SmartStore.DellyManLogistics"; // DO NOT CHANGE!
            
        }

        protected override void Seed(SmartStore.DellyManLogistics.Data.DellyManLogisticsObjectContext context)
        {
           
            //var country = context.Country.FirstOrDefault(c => c.Name == "Nigeria");
            //if (country != null)
            //{
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Abia",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Adamawa",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Akwa Ibom",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Anambra",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Bauchi",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Bayelsa",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Benue",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Borno",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Cross River",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Delta",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Ebonyi",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Edo",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Ekiti",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Enugu",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "FCT - Abuja",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Gombe",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Imo",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Jigawa",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Kaduna",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Kano",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Katsina",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Kebbi",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Kogi",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Kwara",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Lagos",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Nasarawa",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Niger",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Ogun",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Ondo",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Osun",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Oyo",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Plateau",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Rivers",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Sokoto",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Taraba",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Yobe",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
            //    context.StateProvince.AddOrUpdate(new Core.Domain.Directory.StateProvince
            //    {
            //        Name = "Zamfara",
            //        Abbreviation = "",
            //        CountryId = country.Id,
            //        DisplayOrder = 0,
            //    });
                
            //}
        }
    }
}
