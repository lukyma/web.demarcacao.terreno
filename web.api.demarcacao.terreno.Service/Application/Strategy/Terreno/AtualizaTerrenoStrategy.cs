using AutoMapper;
using patterns.strategy;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.CrossCutting;
using web.api.demarcacao.terreno.CrossCutting.Core;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class AtualizaTerrenoStrategy : IStrategy<AtualizaTerrenoRequest, DefaultResponse>
    {
        private IMapper Mapper { get; }
        private ITerrenoRepository TerrenoRepository { get; }
        private IDemarcacaoUnitOfWork UnitOfWork { get; }
        public AtualizaTerrenoStrategy(IMapper mapper,
                                       ITerrenoRepository terrenoRepository,
                                       IDemarcacaoUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            TerrenoRepository = terrenoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<DefaultResponse> HandleAsync(AtualizaTerrenoRequest request, CancellationToken cancellationToken)
        {
            if (!TerrenoRepository.Any(o => o.Id == request.Id))
            {
                return new DefaultResponse();
            }
            var terrenoEntity = Mapper.Map<Domain.Entities.Terreno>(request);
            terrenoEntity.Coordenadas.Where(o => o.IdTerreno == 0).ForEach(o =>
            {
                o.IdTerreno = request.Id;
            });
            TerrenoRepository.Update(terrenoEntity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return new DefaultResponse(true);
        }
    }
}
