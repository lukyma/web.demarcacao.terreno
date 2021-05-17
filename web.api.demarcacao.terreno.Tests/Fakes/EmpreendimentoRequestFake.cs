using web.api.demarcacao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.terreno.Tests.Fakes
{
    public class EmpreendimentoRequestFake
    {
        public EmpreendimentoRequest GetEmpreendimentoRequest_Valido()
        {
            return new EmpreendimentoRequest()
            {
                Descricao = "Descricao de teste",
                GrupoEmpresa = "Grupo de teste",
                Endereco = EnderecoFake.GetEndereco_Valido()
            };
        }

        public CadastraEmpreendimentoRequest GetCadastraEmpreendimentoRequest_Valido()
        {
            return new CadastraEmpreendimentoRequest()
            {
                Descricao = "Descricao de teste",
                GrupoEmpresa = "Grupo de teste",
                Endereco = EnderecoFake.GetEndereco_Valido()
            };
        }

        public AtualizaEmpreendimentoRequest GetAtualizaEmpreendimentoRequest_Valido()
        {
            return new AtualizaEmpreendimentoRequest()
            {
                IdEmpreendimento = 1,
                Descricao = "Descricao de teste",
                GrupoEmpresa = "Grupo de teste",
                Endereco = EnderecoFake.GetEndereco_Valido()
            };
        }

        public RetornaEmpreendimentoQuery GetRetornaEmpreendimentoQuery_Valido()
        {
            return new RetornaEmpreendimentoQuery(1);
        }

        public RetornaEmpreendimentoQuery GetRetornaEmpreendimentoQuery_Invalido()
        {
            return new RetornaEmpreendimentoQuery(0);
        }
    }
}
