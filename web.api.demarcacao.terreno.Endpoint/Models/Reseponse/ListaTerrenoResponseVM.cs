using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.api.demarcacao.terreno.Endpoint.Models.Reseponse
{
    public class ListaTerrenoResponseVM : PaginacaoResponseVM
    {
        public IEnumerable<TerrenoVM> Itens { get; set; }
    }
}
