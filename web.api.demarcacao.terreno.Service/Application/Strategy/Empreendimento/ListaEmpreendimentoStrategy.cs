using AutoMapper;
using patterns.strategy;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class ListaEmpreendimentoStrategy : IStrategy<ListaEmpreendimentoQuery, ListaEmpreendimentoQueryResponse>
    {
        private IMapper Mapper { get; }
        private IEmpreendimentoRepository EmpreendimentoRepository { get; }
        public ListaEmpreendimentoStrategy(IMapper mapper,
                                           IEmpreendimentoRepository empreendimentoRepository)
        {
            Mapper = mapper;
            EmpreendimentoRepository = empreendimentoRepository;
        }

        public async Task<ListaEmpreendimentoQueryResponse> HandleAsync(ListaEmpreendimentoQuery request,
                                                                        CancellationToken cancellationToken)
        {
            return Mapper.Map<ListaEmpreendimentoQueryResponse>(await EmpreendimentoRepository.AllAsync(cancellationToken, true));
        }
    }
}
