using BlazorMealOrdering.Shared.Dtos;

namespace BlazorMealOrdering.Server.Services.Infrastructure
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrders(DateTime OrderDate);
        Task<OrderDto> GetOrderById(Guid Id);
        Task<OrderDto> CreateOrder(OrderDto Order);
        Task<OrderDto> UpdateOrder(OrderDto Order);
        Task DeleteOrder(Guid OrderId);

        Task<List<OrderItemDto>> GetOrderItems(Guid OrderId);
        Task<OrderItemDto> GetOrderItemsById(Guid Id);
        Task<OrderItemDto> CreateOrderItem(OrderItemDto OrderItem);
        Task<OrderItemDto> UpdateOrderItem(OrderItemDto OrderItem);
        Task DeleteOrderItem(Guid OrderItemId);
    }
}