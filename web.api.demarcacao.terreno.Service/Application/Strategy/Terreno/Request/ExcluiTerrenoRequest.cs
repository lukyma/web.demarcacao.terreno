namespace web.api.demarcacao.terreno.Service.Application.Strategy.Terreno.Request
{
    public class ExcluiTerrenoRequest : TerrenoQuery
    {
        public ExcluiTerrenoRequest(long id)
        {
            Id = id;
        }
    }
}
