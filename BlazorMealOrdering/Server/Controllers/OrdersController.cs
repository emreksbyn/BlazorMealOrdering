using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using BlazorMealOrdering.Shared.ResponseModels.Base;
using BlazorMealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService OrderService)
        {
            _orderService = OrderService;
        }



        #region Order Methods

        [HttpGet("OrderById/{Id}")]
        public async Task<ServiceResponse<OrderDto>> GetOrderById(Guid Id)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.GetOrderById(Id)
            };
        }

        [HttpGet("OrdersByDate")]
        public async Task<ServiceResponse<List<OrderDto>>> GetOrder(DateTime OrderDate)
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await _orderService.GetAllOrders(OrderDate)
            };
        }

        [HttpGet("TodaysOrder")]
        public async Task<ServiceResponse<List<OrderDto>>> GetTodaysOrder()
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await _orderService.GetAllOrders(DateTime.Now)
            };
        }

        [HttpPost("CreateOrder")]
        public async Task<ServiceResponse<OrderDto>> CreateOrder(OrderDto Order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.CreateOrder(Order)
            };
        }

        [HttpPost("UpdateOrder")]
        public async Task<ServiceResponse<OrderDto>> UpdateOrder(OrderDto Order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.UpdateOrder(Order)
            };
        }

        [HttpPost("DeleteOrder")]
        public async Task<BaseResponse> DeleteOrder([FromBody] Guid OrderId)
        {
            await _orderService.DeleteOrder(OrderId);
            return new BaseResponse();
        }

        #endregion

        #region OrderItem Methods

        #region Get

        [HttpGet("OrderItemsById/{Id}")]
        public async Task<ServiceResponse<OrderItemDto>> GetOrderItemsById(Guid Id)
        {
            return new ServiceResponse<OrderItemDto>()
            {
                Value = await _orderService.GetOrderItemsById(Id)
            };
        }

        #endregion


        [HttpPost("CreateOrderItem")]
        public async Task<ServiceResponse<OrderItemDto>> CreateOrderItem(OrderItemDto OrderItem)
        {
            return new ServiceResponse<OrderItemDto>()
            {
                Value = await _orderService.CreateOrderItem(OrderItem)
            };
        }

        [HttpPost("UpdateOrderItem")]
        public async Task<ServiceResponse<OrderItemDto>> UpdateOrderItem(OrderItemDto OrderItem)
        {
            return new ServiceResponse<OrderItemDto>()
            {
                Value = await _orderService.UpdateOrderItem(OrderItem)
            };
        }


        [HttpPost("DeleteOrderItem")]
        public async Task<BaseResponse> DeleteOrderItem([FromBody] Guid OrderItemId)
        {
            await _orderService.DeleteOrderItem(OrderItemId);
            return new BaseResponse();
        }

        [HttpGet("OrderItems")]
        public async Task<ServiceResponse<List<OrderItemDto>>> GetOrderItems(Guid OrderId)
        {
            return new ServiceResponse<List<OrderItemDto>>()
            {
                Value = await _orderService.GetOrderItems(OrderId)
            };
        }
        #endregion
    }
}