using System.Linq;
using web.api.demarcacao.terreno.Service.Application.Strategy;
using web.api.demarcacao.terreno.Service.Application.Validators;
using web.api.demarcacao.terreno.Tests.Fakes;
using Xunit;

namespace web.api.demarcacao.terreno.Tests.Tests.Service.Validator
{
    public class CadastraEmpreendimentoRequestValidatorTest
    {
        [Fact]
        public void CadastraEmpreendimentoRequestValidator_Valido()
        {
            var validator = new EmpreendimentoRequestValidator<CadastraEmpreendimentoRequest>();
            var validate = validator.Validate(new EmpreendimentoRequestFake().GetCadastraEmpreendimentoRequest_Valido());

            Assert.True(validate.IsValid);
            Assert.False(validator.CamposObrigatorios.Any());
        }

        [Fact]
        public void CadastraEmpreendimentoRequestValidator_InvalidoDescricao()
        {
            var validator = new EmpreendimentoRequestValidator<CadastraEmpreendimentoRequest>();
            var request = new EmpreendimentoRequestFake().GetCadastraEmpreendimentoRequest_Valido();
            request.Descricao = "";
            var validate = validator.Validate(request);

            Assert.False(validate.IsValid);
            Assert.Contains(validator.CamposObrigatorios, o => o == "Descricao");
        }

        [Fact]
        public void CadastraEmpreendimentoRequestValidator_InvalidoEnderecoLogradouro()
        {
            var validator = new EmpreendimentoRequestValidator<CadastraEmpreendimentoRequest>();
            var request = new EmpreendimentoRequestFake().GetCadastraEmpreendimentoRequest_Valido();
            request.Endereco.Logradouro = "";
            var validate = validator.Validate(request);

            Assert.False(validate.IsValid);
            Assert.Contains(validator.CamposObrigatorios, o => o == "Endereco.Logradouro");
        }
    }
}
