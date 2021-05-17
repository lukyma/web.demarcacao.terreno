using System.Collections.Generic;
using web.api.demarcacao.terreno.Domain.Entities.Core;

namespace web.api.demarcacao.terreno.Domain.Entities
{
    public class Empreendimento : AggregateRoot<long>
    {
        public Empreendimento()
        {
            Terrenos = new HashSet<Terreno>();
        }
        public string Descricao { get; set; }
        public string GrupoEmpresa { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Terreno> Terrenos { get; set; }
    }
}
