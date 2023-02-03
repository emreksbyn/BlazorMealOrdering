using BlazorMealOrdering.Shared.Dtos.Base;

namespace BlazorMealOrdering.Shared.Dtos
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}