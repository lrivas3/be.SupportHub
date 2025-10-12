using AutoMapper;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Models.Support;

namespace HelpPoint.Infrastructure.Profiles;

public class SupportRequestUpdateProfile : Profile
{
    public SupportRequestUpdateProfile() => CreateMap<SupportRequestUpdateRequest, SupportRequest>(MemberList.None)
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.TokenVerificacion, opt =>
            opt.Condition(src => !string.IsNullOrEmpty(src.TokenVerificacion)))
        .ForMember(dest => dest.Descripcion, opt =>
                opt.Condition(src => !string.IsNullOrEmpty(src.Descripcion)))
        .ForMember(dest => dest.Titulo, opt =>
                opt.Condition(src => !string.IsNullOrEmpty(src.Titulo)))
        .ForMember(dest => dest.EstadoId, opt =>
                opt.Condition(src => src.EstadoId != 0))
    ;
}
