using System.Collections.Generic;

namespace web.api.demarcacao.terreno.Service.Application.Strategy.Request
{
    public class ListaEmpreendimentoQuery
    {

    }

    public class ListaEmpreendimentoQueryResponse
    {
        public IEnumerable<EmpreendimentoRequest> Itens { get; set; }
    }
}
