using System.Collections.Generic;

namespace web.api.demarcacao.terreno.Endpoint.Models.Reseponse
{
    public class ListaEmpreendimentoResponseVM : PaginacaoResponseVM
    {
        public IEnumerable<EmpreendimentoVM> Itens { get; set; }
    }
}
