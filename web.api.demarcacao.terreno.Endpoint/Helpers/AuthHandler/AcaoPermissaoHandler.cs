using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using patterns.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Endpoint.Helpers.AuthHandler.Requirement;
using web.api.demarcacao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.terreno.Endpoint.Helpers.AuthHandler
{
    public class AcaoPermissaoHandler : IAuthorizationHandler
    {
        private IStrategyContext StrategyContext { get; }
        public AcaoPermissaoHandler(IStrategyContext strategyContext)
        {
            StrategyContext = strategyContext;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                await Task.Run(() => context.Fail());
                return;
            }

            var requirements = context.PendingRequirements.Select(o => (InterfaceRequirement)o);
            var claimsJson = context.User.Claims.FirstOrDefault(o => o.Type == "interfaces").Value;
            var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(claimsJson);

            if (!requirements.Any(o => claims.Any(p => p.Key == o.Tag)))
            {
                await Task.Run(() => context.Fail());
            }
            else
            {
                await Task.Run(() => context.Succeed(requirements.FirstOrDefault()));
            }
        }
    }
}
