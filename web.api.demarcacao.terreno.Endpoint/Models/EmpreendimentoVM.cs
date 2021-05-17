using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace web.api.demarcacao.terreno.Endpoint.Models
{
    public class EmpreendimentoVM
    {
        [Required(AllowEmptyStrings = true)]
        public string Descricao { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string GrupoEmpresa { get; set; }
        [Required(AllowEmptyStrings = true)]
        public EnderecoVM Endereco { get; set; }
    }
}
