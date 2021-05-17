using AutoMapper;
using patterns.strategy;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.CrossCutting.Core;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class CadastraTerrenoStrategy : IStrategy<CadastraTerrenoRequest, CadastraTerrenoResponse>
    {
        private IMapper Mapper { get; }
        private ITerrenoRepository TerrenoRepository { get; }
        private IDemarcacaoUnitOfWork UnitOfWork { get; }
        public CadastraTerrenoStrategy(IMapper mapper,
                                       ITerrenoRepository terrenoRepository,
                                       IDemarcacaoUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            TerrenoRepository = terrenoRepository;
            UnitOfWork = unitOfWork;
        }
        public async Task<CadastraTerrenoResponse> HandleAsync(CadastraTerrenoRequest request, CancellationToken cancellationToken)
        {
            var terrenoEntity = Mapper.Map<Domain.Entities.Terreno>(request);
            await TerrenoRepository.AddAsync(terrenoEntity, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return new CadastraTerrenoResponse(terrenoEntity.Id);
        }
    }
}
