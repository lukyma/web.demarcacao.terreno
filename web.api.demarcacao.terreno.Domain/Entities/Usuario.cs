using System.Collections.Generic;
using web.api.demarcacao.terreno.Domain.Entities.Core;

namespace web.api.demarcacao.terreno.Domain.Entities
{
    public class Usuario : AggregateRoot<long>
    {
        public Usuario()
        {
            UsuarioInterfaces = new HashSet<UsuarioInterface>();
        }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UsuarioInterface> UsuarioInterfaces { get; set; }
    }
}
