using AutoMapper;
using BlazorMealOrdering.Server.Data.Models;
using BlazorMealOrdering.Shared.Dtos;

namespace BlazorMealOrdering.Server.Services.Extensions
{
    public static class ConfigureMappingExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection services)
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(conf =>
                                                                       {
                                                                           conf.AddProfile(new MappinProfile());
                                                                       });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }

    public class MappinProfile : Profile
    {
        public MappinProfile()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;

            CreateMap<Supplier, SupplierDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>()
                    .ForMember(dto => dto.SupplierName, conf => conf.MapFrom(o => o.Supplier.Name))
                    .ForMember(dto => dto.CreateUserFullName, conf => conf.MapFrom(o => o.User.FirstName + " " + o.User.LastName));

            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(dto => dto.CreateUserFullName, conf => conf.MapFrom(oi => oi.User.FirstName + " " + oi.User.LastName))
                    .ForMember(dto => dto.OrderName, conf => conf.MapFrom(oi => oi.Order.Name ?? ""));
        }
    }
}