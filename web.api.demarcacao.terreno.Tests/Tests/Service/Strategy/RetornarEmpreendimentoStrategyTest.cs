using FluentValidation.Results;
using Moq;
using patterns.strategy;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Service.Application.Strategy;
using web.api.demarcacao.terreno.Tests.Fakes;
using web.api.demarcacao.terreno.Tests.Mocks;
using web.api.demarcacao.terreno.Tests.Mocks.Data;
using Xunit;

namespace web.api.demarcacao.terreno.Tests.Tests.Service.Strategy
{
    public class RetornarEmpreendimentoStrategyTest
    {
        [Fact]
        public async Task HandleAsync_Sucesso()
        {
            var mockEmpreendimentoRepo = EmpreendimentoRepositoryMock.GetMock();

            mockEmpreendimentoRepo.Setup(o => o.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Empreendimento());

            var strategy = new RetornaEmpreendimentoStrategy(AutomapperMock.GetProfilesMapper(),
                                                              mockEmpreendimentoRepo.Object);

            var query = new EmpreendimentoRequestFake().GetRetornaEmpreendimentoQuery_Valido();

            var response = await strategy.HandleAsync(query, CancellationToken.None);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task HandleAsync_SucessoVazio()
        {
            var mockEmpreendimentoRepo = EmpreendimentoRepositoryMock.GetMock();

            mockEmpreendimentoRepo.Setup(o => o.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(Empreendimento));

            var strategy = new RetornaEmpreendimentoStrategy(AutomapperMock.GetProfilesMapper(),
                                                              mockEmpreendimentoRepo.Object);

            var query = new EmpreendimentoRequestFake().GetRetornaEmpreendimentoQuery_Valido();

            var response = await strategy.HandleAsync(query, CancellationToken.None);
            Assert.Null(response);
        }

        [Fact]
        public async Task HandleAsync_FalhaValidationProxy()
        {
            var mockEmpreendimentoRepo = EmpreendimentoRepositoryMock.GetMock();

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

            var strategy = (IStrategy<RetornaEmpreendimentoQuery, RetornarEmpreendimentoQueryResponse>)
                            new ProxyFake().CreateProxyValidatorFake<RetornaEmpreendimentoQuery, RetornarEmpreendimentoQueryResponse>(
                                new RetornaEmpreendimentoStrategy(AutomapperMock.GetProfilesMapper(),
                                mockEmpreendimentoRepo.Object), validationFailures);

            var request = new EmpreendimentoRequestFake().GetRetornaEmpreendimentoQuery_Invalido();
            await strategy.HandleAsync(request, CancellationToken.None);
            Assert.Contains(validationFailures, o => o.ErrorMessage == "O campo \"idEmpreendimento\" não foi informado.");
        }
    }
}
