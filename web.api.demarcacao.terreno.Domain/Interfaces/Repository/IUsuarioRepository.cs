using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository.Common;

namespace web.api.demarcacao.terreno.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
    }
}
