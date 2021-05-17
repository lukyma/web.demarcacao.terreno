using Moq;
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
    public class CadastraTerrenoStrategyTest
    {
        [Fact]
        public async Task HandleAsync_Sucesso()
        {
            var mockUnitOfWork = DemarcacaoUnitOfWorkMock.GetMock();
            var mockTerrenoRepo = TerrenoRepositoryMock.GetMock();

            mockUnitOfWork.Setup(o => o.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            mockTerrenoRepo.Setup(o => o.AddAsync(It.IsAny<Terreno>(), It.IsAny<CancellationToken>()));

            var strategy = new CadastraTerrenoStrategy(AutomapperMock.GetProfilesMapper(),
                                                       mockTerrenoRepo.Object,
                                                       mockUnitOfWork.Object);

            var request = new TerrenoRequestFake().GetTerrenoRequest_Valido();

            var response = await strategy.HandleAsync(request,
                                                      CancellationToken.None);
            Assert.IsType<CadastraTerrenoResponse>(response);
            Assert.NotNull(response);
        }
    }
}
