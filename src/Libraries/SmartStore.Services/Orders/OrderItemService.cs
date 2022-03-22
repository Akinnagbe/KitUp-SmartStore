using SmartStore.Core.Data;
using SmartStore.Core.Domain.Orders;
using SmartStore.Data.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Services.Orders
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;

        public OrderItemService(IRepository<OrderItem> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        public OrderItem GetOrderItemById(int orderItemId)
        {

            if (orderItemId == 0)
                return null;

            return _orderItemRepository.GetByIdCached(orderItemId, "db.orderitem.id-" + orderItemId);
        }

        public List<OrderItem> GetOrderItemsByProductId(int productId)
        {
            var orderItems = _orderItemRepository.Table.Where(o=>o.ProductId == productId).ToList();    
            return orderItems;
        }

        public void UpdateRange(List<OrderItem> entities)
        {
            _orderItemRepository.UpdateRange(entities);

        }
    }
}
