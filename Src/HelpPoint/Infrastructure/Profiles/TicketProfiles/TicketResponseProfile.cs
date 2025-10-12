using AutoMapper;
using HelpPoint.Infrastructure.Dtos.Response;
using HelpPoint.Infrastructure.Models.Ticket;

namespace HelpPoint.Infrastructure.Profiles.TicketProfiles;

public class TicketResponseProfile : Profile
{
    public TicketResponseProfile() => CreateMap<Ticket, TicketResponse>()
        .ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.OrdenEnTablero,
            opt => opt.MapFrom(src => src.OrdenEnTablero))
        .ForMember(dest => dest.Titulo,
            opt => opt.MapFrom(src => src.Titulo))
        .ForMember(dest => dest.Descripcion,
            opt => opt.MapFrom(src => src.Descripcion));
}
