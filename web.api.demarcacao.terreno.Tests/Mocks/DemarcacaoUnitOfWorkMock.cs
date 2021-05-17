using Moq;
using web.api.demarcacao.terreno.CrossCutting.Core;

namespace web.api.demarcacao.terreno.Tests.Mocks
{
    public class DemarcacaoUnitOfWorkMock
    {
        public static Mock<IDemarcacaoUnitOfWork> GetMock()
        {
            return new Mock<IDemarcacaoUnitOfWork>();
        }
    }
}
