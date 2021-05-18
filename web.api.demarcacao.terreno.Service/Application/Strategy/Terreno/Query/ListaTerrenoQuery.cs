using System;
using System.Collections.Generic;
using System.Text;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class ListaTerrenoQuery : PaginacaoBase
    {
    }

    public class ListaTerrenoQueryResponse : PaginacaoBase
    {
        public ListaTerrenoQueryResponse(IEnumerable<TerrenoRequest> itens)
        {
            Itens = itens;
        }
        public IEnumerable<TerrenoRequest> Itens { get; }
    }
}
