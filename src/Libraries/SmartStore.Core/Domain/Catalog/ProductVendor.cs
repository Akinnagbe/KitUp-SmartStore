using SmartStore.Core.Domain.Localization;
using SmartStore.Core.Domain.Security;
using SmartStore.Core.Domain.Seo;
using SmartStore.Core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Core.Domain.Catalog
{
    [DataContract]
    public partial class ProductVendor : BaseEntity, IAuditable, ISoftDeletable, ILocalizedEntity, ISlugSupported, IAclSupported, IStoreMappingSupported
    {
        private ICollection<Product> _products;

        /// <summary>
        /// Specifies the userId that registers this Vendor. This property is from the ShopOwner Client Project
        /// </summary>
        [DataMember]
        public string ApplicationUserId { get; set; }

        [DataMember]
        public string BusinessName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
		[DataMember]
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
		[DataMember]
        public DateTime UpdatedOnUtc { get; set; }

        [Index]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL.
        /// </summary>
        [DataMember]
        [Index]
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        [DataMember]
        public bool LimitedToStores { get; set; }


        [DataMember]
        public virtual ICollection<Product> Products
        {
            get => _products ?? (_products = new HashSet<Product>());
            protected set => _products = value;
        }

    }
}
