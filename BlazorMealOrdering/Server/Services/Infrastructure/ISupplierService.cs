using BlazorMealOrdering.Shared.Dtos;

namespace BlazorMealOrdering.Server.Services.Infrastructure
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetAllSuppliers();
        Task<SupplierDto> GetSupplierById(Guid id);
        Task<SupplierDto> CreateSupplier(SupplierDto supplier);
        Task<SupplierDto> UpdateSupplier(SupplierDto supplier);
        Task DeleteSupplierById(Guid id);
    }
}