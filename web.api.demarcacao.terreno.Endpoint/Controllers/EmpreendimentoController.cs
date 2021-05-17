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
using static web.api.demarcacao.terreno.Endpoint.Config.Swagger.EmpreendimentoExampleMessage;

namespace web.api.demarcacao.terreno.Endpoint.Controllers
{
    [Authorize]
    public class EmpreendimentoController : BaseApiController
    {
        public EmpreendimentoController(IMapper mapper,
                                        IStrategyContext strategyContext,
                                        IHandleValidation handleValidation) : base(mapper, strategyContext, handleValidation)
        {
        }

        /// <summary>
        /// Cadastro inicial de um empreendimento.
        /// </summary>
        /// <param name="empreendimento">Body empreendimento</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status201Created, SwaggerConstants.Descricao201, typeof(EmpreendimentoVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoPostMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpPost("v{version:apiVersion}/[controller]")]
        [Authorize("CadEmp")]
        public async Task<IActionResult> PostAsync([FromBody] EmpreendimentoVM empreendimento, CancellationToken cancellationToken)
        {
            var request = Mapper.Map<CadastraEmpreendimentoRequest>(empreendimento);
            await StrategyContext.HandlerAsync<CadastraEmpreendimentoRequest, DefaultResponse>(request, cancellationToken);
            return await ApiResponseAsync(Created("api/v1/empreendimento/1", empreendimento));
        }

        /// <summary>
        /// Listagem de todos os empreendimentos
        /// </summary>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao200, typeof(IEnumerable<EmpreendimentoVM>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoPutMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpGet("v{version:apiVersion}/[controller]")]
        [Authorize("ListEmp")]
        public async Task<IActionResult> GetAsync()
        {
            return await Task.FromResult(Ok());
        }

        /// <summary>
        /// Exibe os detalhes de um empreendimento específico. 
        /// </summary>
        /// <param name="id">identificador do empreendimento</param>
        /// <param name="cancellationToken">identificador do empreendimento</param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao200, typeof(EmpreendimentoVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoGetIdMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpGet("v{version:apiVersion}/[controller]/{id:int}")]
        [Authorize("ListEmp")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var response = Mapper.Map<EmpreendimentoVM>(await StrategyContext.HandlerAsync<RetornaEmpreendimentoQuery,
                                                        RetornarEmpreendimentoQueryResponse>(new RetornaEmpreendimentoQuery(id), cancellationToken));
            if (response == null)
            {
                return await ApiResponseAsync(NotFound());
            }

            return await ApiResponseAsync(Ok(response));
        }

        /// <summary>
        /// Atualiza as informações de um empreendimento.
        /// </summary>
        /// <param name="id">identificador do empreendimento</param>
        /// <param name="empreendimento">recurso empreendimento</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status204NoContent, SwaggerConstants.Descricao204)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoPutMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpPut("v{version:apiVersion}/[controller]/{id:int}")]
        [Authorize("AtualEmp")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] EmpreendimentoVM empreendimento, CancellationToken cancellationToken)
        {
            var request = Mapper.Map<AtualizaEmpreendimentoRequest>(empreendimento);
            request.IdEmpreendimento = id;
            var response = await StrategyContext.HandlerAsync<AtualizaEmpreendimentoRequest, DefaultResponse>(request, cancellationToken);
            if (!response.IsNotDefault)
            {
                return await ApiResponseAsync(NotFound());
            }
            return await ApiResponseAsync(NoContent());
        }


        /// <summary>
        /// Excluí um empreendimento.
        /// </summary>
        /// <param name="id">identificador do empreendimento</param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status204NoContent, SwaggerConstants.Descricao204)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoDeleteMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpDelete("v{version:apiVersion}/[controller]/{id:int}")]
        [Authorize("ExcEmp")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await Task.FromResult(NoContent());
        }
    }
}
