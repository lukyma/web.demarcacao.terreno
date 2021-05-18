using System.Collections.Generic;
using web.api.demarcacao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class ListaEmpreendimentoQuery : PaginacaoBase
    {
    }

    public class ListaEmpreendimentoQueryResponse : PaginacaoBase
    {
        public ListaEmpreendimentoQueryResponse(IEnumerable<EmpreendimentoRequest> itens)
        {
            Itens = itens;
        }
        public IEnumerable<EmpreendimentoRequest> Itens { get; }
    }
}
