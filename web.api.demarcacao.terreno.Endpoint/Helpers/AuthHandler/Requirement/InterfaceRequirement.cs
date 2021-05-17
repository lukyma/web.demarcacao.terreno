using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.api.demarcacao.terreno.Endpoint.Helpers.AuthHandler.Requirement
{
    public struct InterfaceRequirement : IAuthorizationRequirement
    {
        public string Tag { get; }
        public InterfaceRequirement(string tag)
        {
            Tag = tag;
        }
    }
}
