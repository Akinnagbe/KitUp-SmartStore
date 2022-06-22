using SmartStore.Web.Framework.Modelling;

namespace SmartStore.Plugin1.Models
{
    [CustomModelPart]
    public class AdminEditTabModel : ModelBase
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public bool IsActive { get; set; }
    }
}