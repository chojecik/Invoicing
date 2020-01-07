using AutoMapper;
using Invoicing.Core.Models;
using Invoicing.Data.Entities;

namespace Invoicing.Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
        }
    }
}
