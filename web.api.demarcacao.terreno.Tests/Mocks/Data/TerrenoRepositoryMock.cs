using Moq;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.terreno.Tests.Mocks.Data
{
    public class TerrenoRepositoryMock
    {
        public static Mock<ITerrenoRepository> GetMock()
        {
            return new Mock<ITerrenoRepository>();
        }
    }
}
