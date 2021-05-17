using web.api.demarcacao.terreno.Data.Context;
using web.api.demarcacao.terreno.Data.Repository.Common;
using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.terreno.Data.Repository
{
    public class CoordenadaRepository : Repository<Coordenada, long>, ICoordenadaRepository
    {
        public CoordenadaRepository(IDemarcacaoPostgressContext dbContext)
            : base(dbContext)
        {
        }
    }
}
