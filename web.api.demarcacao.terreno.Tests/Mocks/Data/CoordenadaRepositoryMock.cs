using Moq;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.terreno.Tests.Mocks.Data
{
    public class CoordenadaRepositoryMock
    {
        public static Mock<ICoordenadaRepository> GetMock()
        {
            return new Mock<ICoordenadaRepository>();
        }
    }
}
