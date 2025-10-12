using AutoMapper;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Models.Ticket;

namespace HelpPoint.Infrastructure.Profiles.TicketProfiles;

public class TicketRequestProfile : Profile
{
    public TicketRequestProfile() => CreateMap<TicketRequest, Ticket>(MemberList.None)
        .ForMember(dest => dest.Titulo,
            opts => opts.MapFrom(src => src.Titulo))
        .ForMember(dest => dest.Descripcion,
            opts => opts.MapFrom(src => src.Descripcion))
        .ForMember(dest => dest.EstadoId,
            opt =>
                opt.MapFrom(src => src.EstadoId))
        .ForMember(dest => dest.TipoId,
            opt =>
                opt.MapFrom(src => src.TipoId))
        .ForMember(dest => dest.PrioridadId,
            opt =>
                opt.MapFrom(src => src.PrioridadId));
}

