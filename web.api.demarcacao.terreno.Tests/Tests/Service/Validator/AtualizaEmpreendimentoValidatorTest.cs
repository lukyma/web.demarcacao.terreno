using System.Linq;
using web.api.demarcacao.terreno.Service.Application.Validators;
using web.api.demarcacao.terreno.Tests.Fakes;
using Xunit;

namespace web.api.demarcacao.terreno.Tests.Tests.Service.Validator
{
    public class AtualizaEmpreendimentoRequestValidatorTest
    {
        [Fact]
        public void AtualizaEmpreendimentoRequestValidator_Valido()
        {
            var validator = new AtualizaEmpreendimentoRequestValidator();
            var request = new EmpreendimentoRequestFake().GetAtualizaEmpreendimentoRequest_Valido();
            var validate = validator.Validate(request);
            Assert.True(validate.IsValid);
            Assert.False(validator.CamposObrigatorios.Any());
        }

        [Fact]
        public void AtualizaEmpreendimentoRequestValidator_InvalidoDescricao()
        {
            var validator = new AtualizaEmpreendimentoRequestValidator();
            var request = new EmpreendimentoRequestFake().GetAtualizaEmpreendimentoRequest_Valido();
            request.Descricao = "";
            var validate = validator.Validate(request);

            Assert.False(validate.IsValid);
            Assert.Contains(validator.CamposObrigatorios, o => o == "Descricao");
        }

        [Fact]
        public void AtualizaEmpreendimentoRequestValidator_InvalidoEnderecoLogradouro()
        {
            var validator = new AtualizaEmpreendimentoRequestValidator();
            var request = new EmpreendimentoRequestFake().GetAtualizaEmpreendimentoRequest_Valido();
            request.Endereco.Logradouro = "";
            var validate = validator.Validate(request);

            Assert.False(validate.IsValid);
            Assert.Contains(validator.CamposObrigatorios, o => o == "Endereco.Logradouro");
        }

        [Fact]
        public void AtualizaEmpreendimentoRequestValidator_InvalidoIdEmpreendimento()
        {
            var validator = new AtualizaEmpreendimentoRequestValidator();
            var request = new EmpreendimentoRequestFake().GetAtualizaEmpreendimentoRequest_Valido();
            request.IdEmpreendimento = 0;
            var validate = validator.Validate(request);

            Assert.False(validate.IsValid);
            Assert.Contains(validator.CamposObrigatorios, o => o == "IdEmpreendimento");
        }
    }
}
