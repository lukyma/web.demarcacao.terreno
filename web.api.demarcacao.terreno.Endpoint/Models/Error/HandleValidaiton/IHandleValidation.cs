namespace web.api.demarcacao.terreno.Endpoint.Models.HandleValidaiton
{
    public interface IHandleValidation
    {
        bool HasErroMessage();
        ErrorMessage GetAllMessages();
    }
}
