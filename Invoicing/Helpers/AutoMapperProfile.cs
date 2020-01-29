using AutoMapper;
using Invoicing.Core.Database.Entities;
using Invoicing.Web.Models;
using System.Collections;
using System.Collections.Generic;

namespace Invoicing.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterModel, User>();

            CreateMap<InvoiceModel, Invoice>()
                .ForMember(dest => dest.Number, opts => opts.MapFrom(src => src.InvoiceNumber))
                .ForPath(dest => dest.Attachment.DirectoryPath, opts => opts.MapFrom(src => src.FilePath))
                .ForPath(dest => dest.Attachment.FileExtension, opts => opts.MapFrom(src => src.FileExtension))
                .ReverseMap();
        }
    }
}
