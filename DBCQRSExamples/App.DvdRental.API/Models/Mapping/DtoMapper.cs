using System.ComponentModel;
using App.DvdRental.Domain.Models.Entity;
using AutoMapper;

namespace App.DvdRental.API.Models.Mapping;
public class DtoMapper : Profile
{
    public DtoMapper()
    {
        CreateMap<CategoryDto,Category>()
          .ForMember(dest=> dest.Last_Update 
          , opt=> opt.MapFrom(src => DateTime.Now) );
    }
}
