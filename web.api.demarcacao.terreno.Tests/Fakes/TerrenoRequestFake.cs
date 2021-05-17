using System.Collections.Generic;
using web.api.demarcacao.terreno.Domain.Entities;
using web.api.demarcacao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.terreno.Tests.Fakes
{
    public class TerrenoRequestFake
    {
        public CadastraTerrenoRequest GetTerrenoRequest_Valido()
        {
            return new CadastraTerrenoRequest()
            {
                Descricao = "Descricao de teste",
                IdEmpreendimento = 1,
                Coordenadas = new List<CoordenadaRequest>()
                {
                    new CoordenadaRequest()
                    {
                        Latitude = -19.883767472393615m,
                        Longitude = -44.00450101045925m,
                        Ordem = 1
                    },
                    new CoordenadaRequest()
                    {
                        Latitude = -19.885160612551847m,
                        Longitude = -44.00466571717958m,
                        Ordem = 2
                    }
                }
            };
        }

        public RetornaTerrenoQuery GetRetornaTerrenoQuery_Valido()
        {
            return new RetornaTerrenoQuery(0);
        }

        public Terreno GetTerrenoCom2Coordenadas()
        {
            return new Terreno()
            {
                Descricao = "Descricao de teste",
                IdEmpreendimento = 1,
                Coordenadas = new List<Coordenada>()
                            {
                                new Coordenada()
                                {
                                    Latitude = -19.883767472393615m,
                                    Longitude = -44.00450101045925m,
                                    Ordem = 1
                                },
                                new Coordenada()
                                {
                                    Latitude = -19.885160612551847m,
                                    Longitude = -44.00466571717958m,
                                    Ordem = 1
                                }
                            }
            };
        }

        public Terreno GetTerrenoCom3Coordenadas()
        {
            var response = GetTerrenoCom2Coordenadas();
            response.Coordenadas
                .Add(new Coordenada()
                {
                    Latitude = -19.88440646647618m,
                    Longitude = -44.00328199219226m,
                    Ordem = 1
                });

            return response;
        }
    }
}
