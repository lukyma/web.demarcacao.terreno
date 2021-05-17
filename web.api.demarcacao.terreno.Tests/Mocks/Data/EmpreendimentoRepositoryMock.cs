using Moq;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.terreno.Tests.Mocks.Data
{
    public class EmpreendimentoRepositoryMock
    {
        public static Mock<IEmpreendimentoRepository> GetMock()
        {
            return new Mock<IEmpreendimentoRepository>();
        }
    }
}
