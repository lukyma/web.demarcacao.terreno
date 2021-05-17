using System.Collections.Generic;

namespace web.api.demarcacao.terreno.Endpoint.Models
{
    public class ErrorMessage
    {
        public ErrorMessage(IEnumerable<Error> errors)
        {
            Errors = errors;
        }
        public IEnumerable<Error> Errors { get; set; }
    }

    public class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
