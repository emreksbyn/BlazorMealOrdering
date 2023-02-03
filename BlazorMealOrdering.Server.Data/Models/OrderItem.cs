using BlazorMealOrdering.Server.Data.Models.Base;

namespace BlazorMealOrdering.Server.Data.Models
{
    public class OrderItem : BaseModel
    {
        public Guid CreatedUserId { get; set; }
        public Guid OrderId { get; set; }
        public string? Description { get; set; }
    }
}