using AutoMapper;
using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Domain.Model;
using System.IO.MemoryMappedFiles;

namespace PrimeiraAPI.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(
                    dest => dest.NameEmployee,
                    m => m.MapFrom(orig => orig.name)
                );
        }
    }
}
