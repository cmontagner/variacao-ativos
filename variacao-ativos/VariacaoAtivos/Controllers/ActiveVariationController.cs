using Microsoft.AspNetCore.Mvc;
using VariacaoAtivos.Domain.Models;
using VariacaoAtivos.Domain.Models.Filter;
using VariacaoAtivos.Domain.Response;
using VariacaoAtivos.Service.Services.Interfaces;

namespace VariacaoAtivos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActiveVariationController : ControllerBase
    {
        private readonly ILogger<ActiveVariationController> _logger;
        private readonly IActiveVariationService _activeVariationService;

        public ActiveVariationController
        (
            ILogger<ActiveVariationController> logger,
            IActiveVariationService activeVariationService
        )
        {
            _logger = logger;
            _activeVariationService = activeVariationService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(ApiResponse<List<ActiveVariation>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActiveVariation()
        {
            _logger.LogInformation("Obtendo Dados Ativo - Início.");

            var result = await _activeVariationService.GetActiveVariation();

            _logger.LogInformation("Obtendo Dados Ativo - Fim.");

            if (!result.Success)
            {
                if (result.Exceptions.Any())
                {
                    result.Id = "Estamos com problemas, favor tentar novamente em alguns minutos.";
                    _logger.LogError($"Obtendo Dados Ativo Disponíveis - {result.Exceptions?.FirstOrDefault()}.");
                    return StatusCode(500, result);
                }
                else
                {
                    if (result.Data == null)
                    {
                        _logger.LogInformation("Obtendo Dados Ativo Disponíveis - Return NotFound.");
                        return NotFound(result);
                    }
                }
            }

            _logger.LogInformation("Obtendo Dados Ativos - Return OK.");
            return Ok(result);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<List<ActiveVariation>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertActiveVariation()
        {
            _logger.LogInformation("Inserindo Dados Ativo - Início.");

            var result = await _activeVariationService.CreateActiveVariation();

            _logger.LogInformation("Inserindo Dados Ativo - Fim.");

            if (!result.Success)
            {
                if (result.Exceptions.Any())
                {
                    result.Id = "Estamos com problemas, favor tentar novamente em alguns minutos.";
                    _logger.LogError($"Inserindo Dados Ativo Disponíveis - {result.Exceptions?.FirstOrDefault()}.");
                    return StatusCode(500, result);
                }
                else
                {
                    if (result.Data == null)
                    {
                        _logger.LogInformation("Inserindo Dados Ativo Disponíveis - Return NotFound.");
                        return NotFound(result);
                    }
                }
            }

            _logger.LogInformation("Obtendo Dados Ativos - Return OK.");
            return Ok(result);
        }
    }
}