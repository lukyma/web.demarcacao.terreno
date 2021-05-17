using FluentValidation.Results;
using Moq;
using patterns.strategy;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Service.Application.Strategy;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;
using web.api.demarcacao.terreno.Tests.Fakes;
using web.api.demarcacao.terreno.Tests.Mocks;
using web.api.demarcacao.terreno.Tests.Mocks.Data;
using Xunit;

namespace web.api.demarcacao.terreno.Tests.Tests.Service.Strategy
{
    public class CadastraEmpreendimentoStrategyTest
    {
        [Fact]
        public async Task HandleAsync_Sucesso()
        {
            var mockUnitOfWork = DemarcacaoUnitOfWorkMock.GetMock();
            var mockEmpreendimentoRepo = EmpreendimentoRepositoryMock.GetMock();

            mockUnitOfWork.Setup(o => o.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            mockEmpreendimentoRepo.Setup(o => o.AddAsync(It.IsAny<Empreendimento>(), It.IsAny<CancellationToken>()));

            var strategy = new CadastraEmpreendimentoStrategy(AutomapperMock.GetProfilesMapper(),
                                                              mockUnitOfWork.Object,
                                                              mockEmpreendimentoRepo.Object);

            var request = new EmpreendimentoRequestFake().GetCadastraEmpreendimentoRequest_Valido();

            var response = await strategy.HandleAsync(request,
                                                      CancellationToken.None);
            Assert.IsType<DefaultResponse>(response);
            Assert.True(response.IsNotDefault);
        }

        [Fact]
        public async Task HandleAsync_FalhaValidationProxy()
        {
            var mockUnitOfWork = DemarcacaoUnitOfWorkMock.GetMock();
            var mockEmpreendimentoRepo = EmpreendimentoRepositoryMock.GetMock();

            mockUnitOfWork.Setup(o => o.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            mockEmpreendimentoRepo.Setup(o => o.AddAsync(It.IsAny<Empreendimento>(), It.IsAny<CancellationToken>()));

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

            var strategy = (IStrategy<CadastraEmpreendimentoRequest, DefaultResponse>)
                            new ProxyFake().CreateProxyValidatorFake<CadastraEmpreendimentoRequest, DefaultResponse>(
                                new CadastraEmpreendimentoStrategy(AutomapperMock.GetProfilesMapper(),
                                mockUnitOfWork.Object,
                                mockEmpreendimentoRepo.Object), validationFailures);

            var request = new EmpreendimentoRequestFake().GetCadastraEmpreendimentoRequest_Valido();
            request.Descricao = "";
            var response = await strategy.HandleAsync(request, CancellationToken.None);

            Assert.False(response.IsNotDefault);
            Assert.Contains(validationFailures, o => o.ErrorMessage == "Os seguintes campos obrigatórios não foram informados: Descricao");
        }
    }
}
