namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class AuthUserQuery
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class AuthUserQueryResponse
    {
        public AuthUserQueryResponse(long idUsuario, string[] interfaces)
        {
            IdUsuario = idUsuario;
            Interfaces = interfaces;
        }
        public long IdUsuario { get; set; }
        public string[] Interfaces { get; set; }
    }
}
