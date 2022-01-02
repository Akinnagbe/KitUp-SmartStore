using SmartStore.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Data.Mapping.Catalog
{
    public class ProductVendorMap:EntityTypeConfiguration<ProductVendor>
    {
        public ProductVendorMap()
        {
            this.ToTable("ProductVendor");
            this.HasKey(p => p.Id);

            this.Property(p => p.ApplicationUserId)
              .HasMaxLength(450)
              .IsRequired();

            this.Property(p => p.BusinessName)
               .HasMaxLength(225)
               .IsRequired();

            this.Property(p => p.Address)
              .HasMaxLength(450)
              .IsRequired();

            this.Property(p => p.Email)
             .HasMaxLength(225)
             .IsRequired();

            this.Property(p => p.PhoneNumber)
             .HasMaxLength(11)
             .IsRequired();

            this.HasMany(p=>p.Products)
                .WithOptional(p=>p.ProductVendor)
                .HasForeignKey(p=>p.ProductVendorId);
        }
    }
}
