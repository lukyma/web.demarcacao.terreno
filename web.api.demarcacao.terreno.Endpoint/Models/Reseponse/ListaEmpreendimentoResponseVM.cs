using System.Collections.Generic;

namespace web.api.demarcacao.terreno.Endpoint.Models.Reseponse
{
    public class ListaEmpreendimentoResponseVM
    {
        public IEnumerable<EmpreendimentoVM> Empreendimentos { get; set; }
    }
}
