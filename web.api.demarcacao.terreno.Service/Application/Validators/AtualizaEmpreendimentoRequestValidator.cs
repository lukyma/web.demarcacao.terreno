using System.Collections.Generic;
using web.api.demarcacao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.terreno.Service.Application.Validators
{
    public class AtualizaEmpreendimentoRequestValidator : EmpreendimentoRequestValidator<AtualizaEmpreendimentoRequest>
    {
        public AtualizaEmpreendimentoRequestValidator()
        {
        }

        protected override IDictionary<string, string> RetornarStringsObrigatorias(AtualizaEmpreendimentoRequest request)
        {
            var response = base.RetornarStringsObrigatorias(request);
            response.Add(nameof(request.IdEmpreendimento), request.IdEmpreendimento == 0 ? "" : request.IdEmpreendimento.ToString());
            return response;
        }
    }
}
