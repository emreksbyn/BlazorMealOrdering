using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorMealOrdering.Server.Data.Context;
using BlazorMealOrdering.Server.Data.Models;
using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorMealOrdering.Server.Services.Managers
{
    public class OrderManager : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly MealOrderinDbContext _context;
        public OrderManager(IMapper mapper, MealOrderinDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        #region Order
        public async Task<List<OrderDto>> GetAllOrders(DateTime OrderDate)
        {
            var list = await _context.Orders.Include(i => i.Supplier)
                      .Where(i => i.CreateDate.Date == OrderDate.Date)
                      .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();

            return list;
        }

        public async Task<OrderDto> GetOrderById(Guid Id)
        {
            return await _context.Orders.Where(i => i.Id == Id)
                       .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync();
        }

        public async Task<OrderDto> CreateOrder(OrderDto Order)
        {
            var dbOrder = _mapper.Map<Order>(Order);
            await _context.AddAsync(dbOrder);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(dbOrder);
        }

        public async Task<OrderDto> UpdateOrder(OrderDto Order)
        {
            var dbOrder = await _context.Orders.FirstOrDefaultAsync(i => i.Id == Order.Id);
            if (dbOrder == null) throw new Exception("Order not found");
            _mapper.Map(Order, dbOrder);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(dbOrder);
        }

        public async Task DeleteOrder(Guid OrderId)
        {
            var detailCount = await _context.OrderItems.Where(i => i.OrderId == OrderId).CountAsync();

            if (detailCount > 0) throw new Exception($"This order has {detailCount} suborder(s).");

            var order = await _context.Orders.FirstOrDefaultAsync(i => i.Id == OrderId);
            if (order == null) throw new Exception("Order not found");

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region OrderItem
        public async Task<List<OrderItemDto>> GetOrderItems(Guid OrderId)
        {
            return await _context.OrderItems.Where(i => i.OrderId == OrderId)
                      .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();
        }

        public async Task<OrderItemDto> GetOrderItemsById(Guid Id)
        {
            return await _context.OrderItems.Include(i => i.Order).Where(i => i.Id == Id)
                      .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        public async Task<OrderItemDto> CreateOrderItem(OrderItemDto OrderItem)
        {
            var order = await _context.Orders
                .Where(i => i.Id == OrderItem.OrderId)
                .Select(i => i.ExpireDate)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception("İlgili Siparişin Ana Kaydı Bulunamadı.");

            if (order <= DateTime.Now)
                throw new Exception("Kapanmış Siparişe Yeni Giriş Yapılamaz!!!");


            var dbOrder = _mapper.Map<OrderItem>(OrderItem);
            await _context.AddAsync(dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDto>(dbOrder);
        }

        public async Task<OrderItemDto> UpdateOrderItem(OrderItemDto OrderItem)
        {
            var dbOrder = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItem.Id);
            if (dbOrder == null) throw new Exception("Sipariş Bulunamadı");

            _mapper.Map(OrderItem, dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDto>(dbOrder);
        }

        public async Task DeleteOrderItem(Guid OrderItemId)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItemId);
            if (orderItem == null)
                throw new Exception("Sipariş Detayı Bulunamadı");

            _context.OrderItems.Remove(orderItem);

            await _context.SaveChangesAsync();
        }
        #endregion
    }
}