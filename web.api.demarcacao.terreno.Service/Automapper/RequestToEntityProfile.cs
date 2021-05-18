using AutoMapper;
using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.terreno.Service.Automapper
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<CadastraEmpreendimentoRequest, Empreendimento>();
            CreateMap<AtualizaEmpreendimentoRequest, Empreendimento>()
                .ForMember(o => o.Id, o => o.MapFrom(o => o.IdEmpreendimento));
            CreateMap<Empreendimento, RetornarEmpreendimentoQueryResponse>();
            CreateMap<CadastraTerrenoRequest, Terreno>();
            CreateMap<CoordenadaRequest, Coordenada>();
            CreateMap<Terreno, RetornaTerrenoQueryResponse>();
            CreateMap<Coordenada, CoordenadaRequest>();
            CreateMap<AtualizaTerrenoRequest, Terreno>();
            CreateMap<Empreendimento, EmpreendimentoRequest>();
            CreateMap<Terreno, TerrenoRequest>();
        }
    }
}
