using SmartStore.Core.Data;
using SmartStore.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Services.Catalog
{
    public partial class ProductVendorService : IProductVendorService
    {
        private readonly IRepository<ProductVendor> _productVendorRepository;

        public ProductVendorService(IRepository<ProductVendor> productVendorRepository)
        {
            _productVendorRepository = productVendorRepository;
        }

        public void DeleteProductVendor(ProductVendor productVendor)
        {
            Guard.NotNull(productVendor, nameof(productVendor));

            _productVendorRepository.Delete(productVendor);

        }

        public IList<ProductVendor> GetAllProductVendors()
        {
            var vendors = _productVendorRepository.Table.Where(p=>!p.Deleted).ToList();
            return vendors;
        }

        public void InsertProductVendor(ProductVendor productVendor)
        {
            if (productVendor == null)
                throw new ArgumentNullException("productVendor");

            _productVendorRepository.Insert(productVendor);
        }

        public void UpdateProductVendor(ProductVendor productVendor)
        {
            if (productVendor == null)
                throw new ArgumentNullException("productVendor");

            _productVendorRepository.Update(productVendor);
        }
    }
}
