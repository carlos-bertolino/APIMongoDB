using APIMongoDB.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APIMongoDB.Controllers.v2
{


    [ApiController]
    [ApiVersion("2.0")] // Vincula esta controller à V2 🚀
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Registra um novo pedido de compra no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de envio de JSON (Body):
        /// 
        ///     POST /api/pedidos
        ///     {
        ///        "clienteId": "64f1a2b3c4d5e6f7a8b9c0a1",
        ///        "itens": [
        ///           {
        ///              "produtoId": "64f1a2b3c4d5e6f7a8b9c0d1",
        ///              "quantidade": 2
        ///           },
        ///           {
        ///              "produtoId": "64f1a2b3c4d5e6f7a8b9c0d2",
        ///              "quantidade": 1
        ///           }
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="input">Objeto contendo o cliente e os itens do pedido.</param>
        /// <returns>O pedido gerado com valores calculados e ID gerado pelo MongoDB.</returns>
        /// <response code="201">Pedido finalizado e gerado com sucesso.</response>
        /// <response code="400">Se algum produto não possuir estoque suficiente ou for inválido.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] CriarPedidoInputModel input)
        {
            try
            {
                // O serviço processa a lógica de negócio, busca preços no banco de produtos, etc.
                Pedido novoPedido = await _pedidoService.CriarPedido(input);

                return CreatedAtAction(
                    nameof(ObterPorId),
                    new { id = novoPedido.Id },
                    novoPedido
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Recupera os detalhes de um pedido específico pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     GET /api/pedidos/64f1a2b3c4d5e6f7a8b9c0a1
        ///
        /// </remarks>
        /// <param name="id">O identificador único do pedido (ID gerado pelo MongoDB).</param>
        /// <returns>Os dados detalhados do pedido encontrado.</returns>
        /// <response code="200">Retorna o pedido solicitado com todos os seus itens e valor total calculado.</response>
        /// <response code="404">Se o ID informado não corresponder a nenhum pedido no banco de dados.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(string id)
        {
            var pedido = await _pedidoService.ObterPorId(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }
    }

}