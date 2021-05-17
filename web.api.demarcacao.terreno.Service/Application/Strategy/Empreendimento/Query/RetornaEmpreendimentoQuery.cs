namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class RetornaEmpreendimentoQuery
    {
        public RetornaEmpreendimentoQuery(long idEmpreendimento)
        {
            IdEmpreendimento = idEmpreendimento;
        }
        public long IdEmpreendimento { get; }
    }

    public class RetornarEmpreendimentoQueryResponse : EmpreendimentoRequest
    {
    }
}
