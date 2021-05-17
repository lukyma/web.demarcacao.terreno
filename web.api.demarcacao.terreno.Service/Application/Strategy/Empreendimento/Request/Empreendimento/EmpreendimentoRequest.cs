using web.api.demarcacao.terreno.Domain.Entities;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class EmpreendimentoRequest
    {
        public string Descricao { get; set; }
        public string GrupoEmpresa { get; set; }
        public Endereco Endereco { get; set; }
    }
}
