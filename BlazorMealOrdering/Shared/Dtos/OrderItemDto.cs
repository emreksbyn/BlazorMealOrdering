using BlazorMealOrdering.Shared.Dtos.Base;

namespace BlazorMealOrdering.Shared.Dtos
{
    public class OrderItemDto : BaseDto
    {
        public string Description { get; set; }
        public Guid CreateUserId { get; set; }
        public string CreateUserFullName { get; set; }
        public Guid OrderId { get; set; }
        public string OrderName { get; set; }
    }
}