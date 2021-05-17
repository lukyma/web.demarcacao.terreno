using Swashbuckle.AspNetCore.Filters;
using System.Linq;
using web.api.demarcacao.terreno.Endpoint.Models;
using static web.api.demarcacao.terreno.Endpoint.Config.Swagger.SwaggerErrorMessages;

namespace web.api.demarcacao.terreno.Endpoint.Config.Swagger
{
    public class EmpreendimentoExampleMessage
    {
        public class EmpreendimentoPostMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                var erros = Erros.Append(new Error("003", "Campos obrigatório não foram informados. Favor verificar a documentação"));

                return new ErrorMessage(erros);
            }
        }

        public class EmpreendimentoPutMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                var erros = Erros.Append(new Error("003", "Campos obrigatório não foram informados. Favor verificar a documentação"));
                return new ErrorMessage(erros);
            }
        }

        public class EmpreendimentoDeleteMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }

        public class EmpreendimentoGetIdMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }
    }
}
