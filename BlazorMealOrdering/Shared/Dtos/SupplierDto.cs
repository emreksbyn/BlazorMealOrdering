using BlazorMealOrdering.Shared.Dtos.Base;

namespace BlazorMealOrdering.Shared.Dtos
{
    public class SupplierDto : BaseDto
    {
        public string Name { get; set; }
        public string WebURL { get; set; }
        public bool IsActive { get; set; }
    }
}