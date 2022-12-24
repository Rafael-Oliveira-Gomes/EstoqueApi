using EstoqueApi.Interface.Service;
using EstoqueApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApi.Controllers 
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoService _produtoService;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoService produtoService)
        {
            _logger = logger;
            _produtoService = produtoService;
        }

        [HttpPost(template: "Adiciona")]
        public async Task<IActionResult> AdicionaProduto([FromBody] ProdutoDto produtoDto)
        {
            try
            {
                var result = await _produtoService.AdicionaProduto(produtoDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(1, $"Erro: {nameof(ProdutoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPost(template: "Remover")]
        public async Task<IActionResult> RemoverProduto([FromBody] int produtoId)
        {
            try
            {
                if (produtoId <= 0) { return BadRequest("Id do produto não deve ser menor que 1"); }

                var result = await _produtoService.RemoverProduto(produtoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(1, $"Erro: {nameof(ProdutoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetProduto([FromQuery] int produtoId)
        {
            if (produtoId <= 0) { return BadRequest("Id do produto não deve ser menor que 1"); }

            try
            {
                var result = await _produtoService.GetProduto(produtoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(1, $"Erro: {nameof(ProdutoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet(template: "ProcurarPorNome")]
        public async Task<IActionResult> GetProdutoPorNome([FromQuery] string nomeProduto)
        {
            if (string.IsNullOrWhiteSpace(nomeProduto)) { return BadRequest("Nome do produto inválido"); }

            try
            {
                var result = await _produtoService.GetProdutoPorNome(nomeProduto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(1, $"Erro: {nameof(ProdutoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet(template: "Listar")]
        public async Task<IActionResult> ListarProdutos()
        {
            try
            {
                var result = await _produtoService.ListarProdutos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(1, $"Erro: {nameof(ProdutoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }

        [HttpPost(template: "Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] ProdutoDto produto)
        {
            try
            {
                var result = await _produtoService.UpdateProduto(produto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(1, $"Erro: {nameof(ProdutoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }
    }
}

