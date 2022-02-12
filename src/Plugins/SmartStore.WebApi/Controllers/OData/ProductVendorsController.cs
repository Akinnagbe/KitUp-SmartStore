using SmartStore.Core.Domain.Catalog;
using SmartStore.Core.Security;
using SmartStore.Services.Catalog;
using SmartStore.Web.Framework.WebApi.OData;
using SmartStore.Web.Framework.WebApi.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace SmartStore.WebApi.Controllers.OData
{
    [IEEE754Compatible]
    public class ProductVendorsController : WebApiEntityController<ProductVendor, IProductVendorService>
    {

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public IHttpActionResult Get()
        {
            return Ok(GetEntitySet());
        }

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public IHttpActionResult Get(int key)
        {
            return Ok(GetByKey(key));
        }

        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public IHttpActionResult GetProperty(int key, string propertyName)
        {
            return GetPropertyValue(key, propertyName);
        }

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditCategory)]
        public IHttpActionResult Post(ProductVendor entity)
        {
            var result = Insert(entity, () => Service.InsertProductVendor(entity));
            return result;
        }

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditCategory)]
        public async Task<IHttpActionResult> Put(int key, ProductVendor entity)
        {
            var result = await UpdateAsync(entity, key, () => Service.UpdateProductVendor(entity));
            return result;
        }

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditCategory)]
        public async Task<IHttpActionResult> Patch(int key, Delta<ProductVendor> model)
        {
            var result = await PartiallyUpdateAsync(key, model, entity => Service.UpdateProductVendor(entity));
            return result;
        }

        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditCategory)]
        public async Task<IHttpActionResult> Delete(int key)
        {
            var result = await DeleteAsync(key, entity => Service.DeleteProductVendor(entity));
            return result;
        }

        #region Navigation properties               

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public IHttpActionResult GetProduct(int key)
        {
            return Ok(GetRelatedEntity(key, x => x.Products));
        }

        #endregion
    }
}
