using BlazorMealOrdering.Shared.Dtos.Base;

namespace BlazorMealOrdering.Shared.Dtos
{
    public class OrderDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid CreateUserId { get; set; }
        public string CreateUserFullName { get; set; }
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}