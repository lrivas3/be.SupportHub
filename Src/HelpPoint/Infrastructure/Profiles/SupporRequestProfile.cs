using AutoMapper;
using HelpPoint.Common.Constants;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Models.Support;

namespace HelpPoint.Infrastructure.Profiles;

public class SupporRequestProfile : Profile
{
    public SupporRequestProfile() =>
        CreateMap<SupportRequestRequest, SupportRequest>(MemberList.None)
            .ForMember(dest => dest.Titulo,
                       opt => opt.MapFrom(src =>
                           $"Soporte de {src.Email} – {DateTime.UtcNow:yyyy-MM-dd}"))
            .ForMember(dest => dest.Descripcion,
                src => src.MapFrom(request => request.Descripcion))
            // Id de empleado se obtiene antes de mapeo
            .ForMember(dest => dest.EmpleadoId,
                src => src.MapFrom(request => request.EmpleadoId))
            .ForMember(dest => dest.Email, src => src.MapFrom(request => request.Email))
            .ForMember(dest => dest.EstadoId,
                opt => opt.MapFrom(src => (int)AppConstants.EstadosSolicitudes.ABIERTA))
            .ForMember(dest => dest.FechaCreacion,
                opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
            .ForMember(x => x.Estado, opt => opt.Ignore())
            .ForMember(x => x.Empleado, opt => opt.Ignore())
        ;
}
