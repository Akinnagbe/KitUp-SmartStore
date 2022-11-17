﻿using SmartStore.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Services.Catalog
{
    public interface IProductVendorService
    {
        IList<ProductVendor> GetAllProductVendors();

        void InsertProductVendor(ProductVendor productVendor);

        void UpdateProductVendor(ProductVendor productVendor);

        void DeleteProductVendor(ProductVendor productVendor);
    }
}