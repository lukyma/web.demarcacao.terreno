using web.api.demarcacao.terreno.Domain.Entities;

namespace web.api.demarcacao.terreno.Tests.Fakes
{
    public static class EnderecoFake
    {
        public static Endereco GetEndereco_Valido()
        {
            return new Endereco()
            {
                Logradouro = "Rua de teste",
                Numero = "1234",
                Bairro = "Teste",
                Cep = "32110911",
                Cidade = "Belo horizonte",
                Complemento = "",
                Estado = "MG"
            };
        }
    }
}
