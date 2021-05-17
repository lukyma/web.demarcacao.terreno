﻿namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class CadastraTerrenoRequest : TerrenoRequest
    {
    }

    public class CadastraTerrenoResponse
    {
        public long IdTerreno { get; }
        public CadastraTerrenoResponse(long idTerreno)
        {
            IdTerreno = idTerreno;
        }
    }
}
