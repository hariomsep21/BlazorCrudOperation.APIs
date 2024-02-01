using AutoMapper;
using DemoCRUD.DTO.ModelDtos;
using DemoCRUD.Model.Models;


namespace DealerApp.Service.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Students, StudentsDto>();
            CreateMap<Students, StudentsDto>().ReverseMap();
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<Students, StudentGet>();
            CreateMap<Students, StudentGet>().ReverseMap();
            CreateMap<Students, StudentSave>();
            CreateMap<Students, StudentSave>().ReverseMap();
        }

    }
}
