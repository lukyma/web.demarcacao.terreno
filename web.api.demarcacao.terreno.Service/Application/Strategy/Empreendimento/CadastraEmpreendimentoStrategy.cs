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
    public class CadastraEmpreendimentoStrategy : IStrategy<CadastraEmpreendimentoRequest, DefaultResponse>
    {
        private IMapper Mapper { get; }
        private IDemarcacaoUnitOfWork UnitOfWork { get; }
        private IEmpreendimentoRepository EmpreendimentoRepository { get; }
        public CadastraEmpreendimentoStrategy(IMapper mapper,
                                              IDemarcacaoUnitOfWork unitOfWork,
                                              IEmpreendimentoRepository empreendimentoRepository)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            EmpreendimentoRepository = empreendimentoRepository;
        }
        [Validator(typeof(EmpreendimentoRequestValidator<CadastraEmpreendimentoRequest>))]
        public async Task<DefaultResponse> HandleAsync(CadastraEmpreendimentoRequest request, CancellationToken cancellationToken)
        {
            await EmpreendimentoRepository.AddAsync(Mapper.Map<Empreendimento>(request), cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return new DefaultResponse(true);
        }
    }
}
