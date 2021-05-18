using AutoMapper;
using patterns.strategy;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;

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
            var itens = await EmpreendimentoRepository.AllAsync(request.Pagina, 10, cancellationToken, true);
            var response = new ListaEmpreendimentoQueryResponse(Mapper.Map<IEnumerable<EmpreendimentoRequest>>(itens));
            response.Pagina = request.Pagina;
            return response;
        }
    }
}
