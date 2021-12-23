using AutoMapper;
using Valhalla.Dominio.Models;
using Valhalla.WebApp.Models;

namespace Valhalla.WebApp.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<Phone, PhoneViewModel>().ReverseMap();
        }
    }
}
