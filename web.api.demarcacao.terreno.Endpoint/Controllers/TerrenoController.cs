using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using patterns.strategy;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Endpoint.Config;
using web.api.demarcacao.terreno.Endpoint.Models;
using web.api.demarcacao.terreno.Endpoint.Models.HandleValidaiton;
using web.api.demarcacao.terreno.Service.Application.Strategy;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;
using web.api.demarcacao.terreno.Service.Application.Strategy.Terreno.Request;
using static web.api.demarcacao.terreno.Endpoint.Config.Swagger.TerrenoExampleMessage;

namespace web.api.demarcacao.terreno.Endpoint.Controllers
{
    [Route("api")]
    [ApiController]
    public class TerrenoController : BaseApiController
    {
        public TerrenoController(IMapper mapper,
                                 IStrategyContext strategyContext,
                                 IHandleValidation handleValidation) : base(mapper, strategyContext, handleValidation)
        {
        }
        /// <summary>
        /// Cadastro inicial de um terreno.
        /// </summary>
        /// <param name="terreno"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status201Created, SwaggerConstants.Descricao201, typeof(IEnumerable<TerrenoVM>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(TerrenoPostMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpPost("v{version:apiVersion}/[controller]")]
        [Authorize("CadTerr")]
        public async Task<IActionResult> PostAsync([FromBody] TerrenoVM terreno, CancellationToken cancellationToken)
        {
            var response = await StrategyContext.HandlerAsync<CadastraTerrenoRequest, CadastraTerrenoResponse>(Mapper.Map<CadastraTerrenoRequest>(terreno), cancellationToken);
            return await ApiResponseAsync(Created($"api/v1/terreno/{response?.IdTerreno}", terreno));
        }

        /// <summary>
        /// Listagem de todos os terrenos
        /// </summary>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao200, typeof(IEnumerable<TerrenoVM>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400)]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpGet("v{version:apiVersion}/[controller]")]
        [Authorize("ListTerr")]
        public async Task<IActionResult> GetAsync()
        {
            return await Task.FromResult(Ok());
        }

        /// <summary>
        /// Listagem de terrenos de acordo com o empreendimento.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao200, typeof(IEnumerable<TerrenoVM>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(TerrenoGetIdMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpGet("v{version:apiVersion}/[controller]/empreendimento/{idEmpreendimento}")]
        [Authorize("ListTerr")]
        public async Task<IActionResult> GetTerrenosEmpreendimentoAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult(Ok());
        }

        /// <summary>
        /// Exibe os detalhes de um terreno específico. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao200, typeof(TerrenoVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(TerrenoGetIdMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpGet("v{version:apiVersion}/[controller]/{id:long}")]
        [Authorize("ListTerr")]
        public async Task<IActionResult> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            RetornaTerrenoQueryResponse response = await StrategyContext
                .HandlerAsync<RetornaTerrenoQuery,
                              RetornaTerrenoQueryResponse>(new RetornaTerrenoQuery(id), cancellationToken);
            if (response == null)
            {
                return await ApiResponseAsync(NotFound());
            }
            return await ApiResponseAsync(Ok(response));
        }

        /// <summary>
        /// Atualiza as informações de um terreno.
        /// </summary>
        /// <param name="id">identificador do terreno</param>
        /// <param name="terreno">recurso terreno</param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status204NoContent, SwaggerConstants.Descricao204)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(TerrenoPutMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpPut("v{version:apiVersion}/[controller]/{id:long}")]
        [Authorize("AtualTerr")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] TerrenoVM terreno, CancellationToken cancellationToken)
        {
            var request = Mapper.Map<AtualizaTerrenoRequest>(terreno);
            request.Id = id;
            var response = await StrategyContext.HandlerAsync<AtualizaTerrenoRequest, DefaultResponse>(request, cancellationToken);
            if(!response.IsNotDefault)
            {
                return await ApiResponseAsync(NotFound());
            }
            return await ApiResponseAsync(NoContent());
        }

        /// <summary>
        /// Excluí um terreno.
        /// </summary>
        /// <param name="id">identificador do terreno</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status204NoContent, SwaggerConstants.Descricao204)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(TerrenoDeleteMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpDelete("v{version:apiVersion}/[controller]/{id:int}")]
        [Authorize("ExcTerr")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var request = new ExcluiTerrenoRequest(id);
            await StrategyContext.HandlerAsync<ExcluiTerrenoRequest, DefaultResponse>(request, cancellationToken);
            return await ApiResponseAsync(Ok());
        }
    }
}
