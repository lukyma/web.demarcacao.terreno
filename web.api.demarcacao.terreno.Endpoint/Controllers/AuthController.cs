using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using patterns.strategy;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Endpoint.Config;
using web.api.demarcacao.terreno.Endpoint.Models;
using web.api.demarcacao.terreno.Endpoint.Models.HandleValidaiton;
using web.api.demarcacao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.terreno.Endpoint.Controllers
{
    public class AuthController : BaseApiController
    {
        public AuthController(IMapper mapper,
                              IStrategyContext strategyContext,
                              IHandleValidation handleValidation) : base(mapper, strategyContext, handleValidation)
        {
        }

        /// <summary>
        /// Retorna um accessToken para autenticar e autorizar o usuário em vários endpoints, conforme interface.
        /// </summary>
        /// <param name="tokenVM">Body get token</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao201, typeof(AuthTokenResponseVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpPost("v{version:apiVersion}/[controller]")]
        public async Task<IActionResult> PostAsync([FromBody] AuthTokenVM tokenVM, CancellationToken cancellationToken)
        {
            var request = Mapper.Map<AuthUserQuery>(tokenVM);
            var response = await StrategyContext.HandlerAsync<AuthUserQuery, AuthUserQueryResponse>(request, cancellationToken);
            if(response == null)
            {
                return await ApiResponseAsync(StatusCode(401));
            }
            var responseVM = Mapper.Map<AuthTokenResponseVM>(response);
            return await ApiResponseAsync(Ok(responseVM));
        }
    }
}
