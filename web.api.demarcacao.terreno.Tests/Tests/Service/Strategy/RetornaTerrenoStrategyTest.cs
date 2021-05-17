using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
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
    public class RetornaTerrenoStrategyTest
    {
        [Fact]
        public async Task HandleAsync_Sucesso()
        {
            var mockTerrenoRepo = TerrenoRepositoryMock.GetMock();
            var mockCoordenadaRepo = CoordenadaRepositoryMock.GetMock();

            var terreno = new TerrenoRequestFake().GetTerrenoCom2Coordenadas();

            mockTerrenoRepo.Setup(o => o.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(terreno);

            var strategy = new RetornaTerrenoStrategy(AutomapperMock.GetProfilesMapper(),
                                                       mockTerrenoRepo.Object,
                                                       mockCoordenadaRepo.Object);

            mockCoordenadaRepo.Setup(o => o.Find(It.IsAny<Expression<Func<Coordenada, bool>>>(), It.IsAny<bool>()))
                .Returns(terreno.Coordenadas.AsQueryable());

            var request = new TerrenoRequestFake().GetRetornaTerrenoQuery_Valido();

            var response = await strategy.HandleAsync(request,
                                                      CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal(2, response.Coordenadas.Count());
            Assert.True(response.SomaDistanciaPontos.Item1 > 0);
            Assert.Equal(0, response.AreaTotal.Item1);
        }

        [Fact]
        public async Task HandleAsync_SucessoAreaCalculada()
        {
            var mockTerrenoRepo = TerrenoRepositoryMock.GetMock();
            var mockCoordenadaRepo = CoordenadaRepositoryMock.GetMock();

            var terreno = new TerrenoRequestFake().GetTerrenoCom3Coordenadas();

            mockTerrenoRepo.Setup(o => o.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(terreno);

            var strategy = new RetornaTerrenoStrategy(AutomapperMock.GetProfilesMapper(),
                                                       mockTerrenoRepo.Object,
                                                       mockCoordenadaRepo.Object);

            mockCoordenadaRepo.Setup(o => o.Find(It.IsAny<Expression<Func<Coordenada, bool>>>(), It.IsAny<bool>()))
                .Returns(terreno.Coordenadas.AsQueryable());

            var request = new TerrenoRequestFake().GetRetornaTerrenoQuery_Valido();

            var response = await strategy.HandleAsync(request,
                                                      CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal(3, response.Coordenadas.Count());
            Assert.True(response.SomaDistanciaPontos.valor > 0);
            Assert.True(response.AreaTotal.valor > 0);
        }
    }
}
