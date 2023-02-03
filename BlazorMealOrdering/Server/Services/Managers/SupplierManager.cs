using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorMealOrdering.Server.Data.Context;
using BlazorMealOrdering.Server.Data.Models;
using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorMealOrdering.Server.Services.Managers
{
    public class SupplierManager : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly MealOrderinDbContext _context;
        public SupplierManager(IMapper mapper, MealOrderinDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SupplierDto>> GetAllSuppliers()
        {
            return await _context.Suppliers
                                  .Where(u => u.IsActive)
                                  .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync();
        }

        public async Task<SupplierDto> GetSupplierById(Guid id)
        {
            return await _context.Suppliers
                                 .Where(u => u.Id == id)
                                 .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync();
        }

        public async Task<SupplierDto> CreateSupplier(SupplierDto supplier)
        {
            var dbSupplier = _mapper.Map<Supplier>(supplier);
            await _context.Suppliers.AddAsync(dbSupplier);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierDto>(dbSupplier);
        }
        public async Task<SupplierDto> UpdateSupplier(SupplierDto supplier)
        {
            var dbSupplier = await _context.Suppliers.FindAsync(supplier.Id);
            if (dbSupplier == null) throw new Exception("Supplier not found");
            _mapper.Map(supplier, dbSupplier);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierDto>(dbSupplier);
        }

        public async Task DeleteSupplierById(Guid id)
        {
            var dbSupplier = await _context.Suppliers.FindAsync(id);

            if (dbSupplier == null) throw new Exception("Supplier not found");

            int orderCount = await _context.Suppliers.Include(s => s.Orders).Select(s => s.Orders.Count).FirstOrDefaultAsync();

            if (orderCount > 0) throw new Exception($"This supplier has {orderCount} order(s).");

            _context.Suppliers.Remove(dbSupplier);
            await _context.SaveChangesAsync();
        }
    }
}