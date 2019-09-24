using AutoMapper;
using Rikkonbi.Core.Entities;
using Rikkonbi.Infrastructure.Identity;
using Rikkonbi.WebAPI.ViewModels;

namespace Rikkonbi.WebAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<CreateProductViewModel, Product>();
            CreateMap<EditProductViewModel, Product>();

            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderHistoryViewModel>();
            CreateMap<CreateOrderViewModel, Order>();
            CreateMap<EditOrderViewModel, Order>();

            CreateMap<OrderDetail, OrderDetailViewModel>();
            CreateMap<CreateOrderDetailViewModel, OrderDetail>();
            CreateMap<EditOrderDetailViewModel, OrderDetail>();

            CreateMap<Device, DeviceViewModel>();
            CreateMap<CreateDeviceViewModel, Device>();
            CreateMap<EditDeviceViewModel, Device>();

            CreateMap<Borrow, DeviceBorrowViewModel>();
            CreateMap<CreateDeviceBorrowViewModel, Borrow>();
        }
    }
}