using Swashbuckle.AspNetCore.Filters;
using System.Linq;
using web.api.demarcacao.terreno.Endpoint.Models;
using static web.api.demarcacao.terreno.Endpoint.Config.Swagger.SwaggerErrorMessages;

namespace web.api.demarcacao.terreno.Endpoint.Config.Swagger
{
    public class TerrenoExampleMessage
    {
        public class TerrenoPostMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                var erros = Erros.Append(new Error("003", "Campos obrigatório não foram informados. Favor verificar a documentação"));

                return new ErrorMessage(erros);
            }
        }

        public class TerrenoPutMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                var erros = Erros.Append(new Error("003", "Campos obrigatório não foram informados. Favor verificar a documentação"));

                return new ErrorMessage(erros);
            }
        }

        public class TerrenoDeleteMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }

        public class TerrenoGetIdMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }
    }
}
