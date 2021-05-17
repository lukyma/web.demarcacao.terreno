using web.api.demarcacao.terreno.Domain.Entities.Core;

namespace web.api.demarcacao.terreno.Domain.Entities
{
    public class UsuarioInterface
    {
        public long IdUsuario { get; set; }
        public long IdInterface { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Interface Interface { get; set; }
    }
}
