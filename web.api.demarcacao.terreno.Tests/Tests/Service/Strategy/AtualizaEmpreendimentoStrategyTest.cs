using FluentValidation.Results;
using Moq;
using patterns.strategy;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public class AtualizaEmpreendimentoStrategyTest
    {
        [Fact]
        public async Task HandleAsync_Sucesso()
        {
            var mockUnitOfWork = DemarcacaoUnitOfWorkMock.GetMock();
            var mockEmpreendimentoRepo = EmpreendimentoRepositoryMock.GetMock();

            mockUnitOfWork.Setup(o => o.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            mockEmpreendimentoRepo.Setup(o => o.Any(It.IsAny<Expression<Func<Empreendimento, bool>>>()))
                .Returns(true);

            mockEmpreendimentoRepo.Setup(o => o.Update(It.IsAny<Empreendimento>()));

            var strategy = new AtualizaEmpreendimentoStrategy(AutomapperMock.GetProfilesMapper(),
                                                              mockUnitOfWork.Object,
                                                              mockEmpreendimentoRepo.Object);

            var request = new EmpreendimentoRequestFake().GetAtualizaEmpreendimentoRequest_Valido();

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

            mockEmpreendimentoRepo.Setup(o => o.Update(It.IsAny<Empreendimento>()));

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

            var strategy = (IStrategy<AtualizaEmpreendimentoRequest, DefaultResponse>)
                            new ProxyFake().CreateProxyValidatorFake<AtualizaEmpreendimentoRequest, DefaultResponse>(
                                new AtualizaEmpreendimentoStrategy(AutomapperMock.GetProfilesMapper(),
                                mockUnitOfWork.Object,
                                mockEmpreendimentoRepo.Object), validationFailures);

            var request = new EmpreendimentoRequestFake().GetAtualizaEmpreendimentoRequest_Valido();
            request.Descricao = "";
            request.IdEmpreendimento = 0;
            var response = await strategy.HandleAsync(request, CancellationToken.None);

            Assert.False(response.IsNotDefault);
            Assert.Contains(validationFailures, o => o.ErrorMessage == "Os seguintes campos obrigatórios não foram informados: Descricao,IdEmpreendimento");
        }
    }
}
