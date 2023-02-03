using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using BlazorMealOrdering.Shared.ResponseModels.Base;
using BlazorMealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService _supplierService)
        {
            _supplierService = _supplierService;
        }


        [HttpGet("GetSupplierById/{Id}")]
        public async Task<ServiceResponse<SupplierDto>> GetSupplierById(Guid Id)
        {
            return new ServiceResponse<SupplierDto>()
            {
                Value = await _supplierService.GetSupplierById(Id)
            };
        }


        [HttpGet("GetAllSuppliers")]
        public async Task<ServiceResponse<List<SupplierDto>>> GetAllSuppliers()
        {
            return new ServiceResponse<List<SupplierDto>>()
            {
                Value = await _supplierService.GetAllSuppliers()
            };
        }


        [HttpPost("CreateSupplier")]
        public async Task<ServiceResponse<SupplierDto>> CreateSupplier(SupplierDto Supplier)
        {
            return new ServiceResponse<SupplierDto>()
            {
                Value = await _supplierService.CreateSupplier(Supplier)
            };
        }


        [HttpPost("UpdateSupplier")]
        public async Task<ServiceResponse<SupplierDto>> UpdateSupplier(SupplierDto Supplier)
        {
            return new ServiceResponse<SupplierDto>()
            {
                Value = await _supplierService.UpdateSupplier(Supplier)
            };
        }


        [HttpPost("DeleteSupplierById")]
        public async Task<BaseResponse> DeleteSupplierById([FromBody] Guid SupplierId)
        {
            await _supplierService.DeleteSupplierById(SupplierId);
            return new BaseResponse();
        }
    }
}