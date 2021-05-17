using AutoMapper;
using patterns.strategy;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.CrossCutting.Core;
using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;
using web.api.demarcacao.terreno.Service.Application.Validators;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class AtualizaEmpreendimentoStrategy : IStrategy<AtualizaEmpreendimentoRequest, DefaultResponse>
    {
        private IMapper Mapper { get; }
        private IDemarcacaoUnitOfWork UnitOfWork { get; }
        private IEmpreendimentoRepository EmpreendimentoRepository { get; }
        public AtualizaEmpreendimentoStrategy(IMapper mapper,
                                              IDemarcacaoUnitOfWork unitOfWork,
                                              IEmpreendimentoRepository empreendimentoRepository)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            EmpreendimentoRepository = empreendimentoRepository;
        }
        [Validator(typeof(AtualizaEmpreendimentoRequestValidator))]
        public async Task<DefaultResponse> HandleAsync(AtualizaEmpreendimentoRequest request, CancellationToken cancellationToken)
        {
            if (!EmpreendimentoRepository.Any(o => o.Id == request.IdEmpreendimento))
            {
                return new DefaultResponse();
            }
            EmpreendimentoRepository.Update(Mapper.Map<Empreendimento>(request));
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return new DefaultResponse(true);
        }
    }
}
