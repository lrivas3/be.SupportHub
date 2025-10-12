using AutoMapper;
using HelpPoint.Common.Constants;
using HelpPoint.Infrastructure.Dtos.Response;
using HelpPoint.Infrastructure.Models.Support;

namespace HelpPoint.Infrastructure.Profiles;

public class SupporRequesResponseProfile : Profile
{
    public SupporRequesResponseProfile() => CreateMap<SupportRequest, SupportRequestResponse>()
        .ForMember(x => x.Id,
            src => src.MapFrom(x => x.Id))
        .ForMember(x => x.Titulo,
            src => src.MapFrom(x => x.Titulo))
        .ForMember(x => x.Descripcion,
            src => src.MapFrom(x => x.Descripcion))
        .ForMember(dest => dest.NombreEstado,
            opt => opt.MapFrom(src => ((AppConstants.EstadosSolicitudes)src.EstadoId).ToString())
        )
        .ForMember(dest => dest.FechaCreacion,
            opt => opt.MapFrom(src => src.FechaCreacion))
        .ForMember(dest => dest.FechaResolucion,
            opt => opt.MapFrom(src => src.FechaResolucion))
        .ForMember(dest => dest.EmpleadoId,
            opt => opt.MapFrom(src => src.EmpleadoId))
        .ForMember(dest => dest.Email,
            opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.TokenVerificacion,
            opt => opt.MapFrom(src => src.TokenVerificacion != null));
}
