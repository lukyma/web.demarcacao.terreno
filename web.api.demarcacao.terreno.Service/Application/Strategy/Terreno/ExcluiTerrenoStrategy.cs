using patterns.strategy;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.CrossCutting.Core;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;
using web.api.demarcacao.terreno.Service.Application.Strategy.Terreno.Request;

namespace web.api.demarcacao.terreno.Service.Application.Strategy.Terreno
{
    public class ExcluiTerrenoStrategy : IStrategy<ExcluiTerrenoRequest, DefaultResponse>
    {
        private ITerrenoRepository TerrenoRepository { get; }
        private IDemarcacaoUnitOfWork UnitOfWork { get; }
        public ExcluiTerrenoStrategy(ITerrenoRepository terrenoRepository,
                                     IDemarcacaoUnitOfWork unitOfWork)
        {
            TerrenoRepository = terrenoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<DefaultResponse> HandleAsync(ExcluiTerrenoRequest request, CancellationToken cancellationToken)
        {
            TerrenoRepository.Delete(await TerrenoRepository.GetAsync(request.Id, cancellationToken));
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return new DefaultResponse();
        }
    }
}
