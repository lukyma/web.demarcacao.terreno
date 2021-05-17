using AutoMapper;
using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Endpoint.Helpers.AuthHandler;
using web.api.demarcacao.terreno.Endpoint.Models.Reseponse;
using web.api.demarcacao.terreno.Service.Application.Strategy;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;

namespace web.api.demarcacao.terreno.Endpoint.Models.Automapper
{
    public class ModelToRequestProfile : Profile
    {
        public ModelToRequestProfile()
        {
            CreateMap<EnderecoVM, Endereco>();
            CreateMap<Endereco, EnderecoVM>();
            CreateMap<EmpreendimentoVM, CadastraEmpreendimentoRequest>();
            CreateMap<EmpreendimentoVM, AtualizaEmpreendimentoRequest>();
            CreateMap<RetornarEmpreendimentoQueryResponse, EmpreendimentoVM>();
            CreateMap<ListaEmpreendimentoQueryResponse, ListaEmpreendimentoResponseVM>();
            CreateMap<TerrenoVM, CadastraTerrenoRequest>();
            CreateMap<CoordenadasVM, CoordenadaRequest>();
            CreateMap<TerrenoVM, AtualizaTerrenoRequest>();
            CreateMap<AuthTokenVM, AuthUserQuery>();
            CreateMap<AuthUserQueryResponse, AuthTokenResponseVM>()
                .ForMember(o => o.AccessToken, o => o.MapFrom(p => GegerateToken.Generate(p)));
        }
    }
}
