using SmartStore.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Services.Orders
{
    public partial interface IOrderItemService
    {
        OrderItem GetOrderItemById(int orderItemId);
        List<OrderItem> GetOrderItemsByProductId(int productId);

        void UpdateRange(List<OrderItem> entities);
    }
}
