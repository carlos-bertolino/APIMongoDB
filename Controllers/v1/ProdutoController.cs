using APIMongoDB.Models;
using APIMongoDB.Services;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;



namespace APIMongoDB.Controllers.v1;

[ApiController]
[ApiVersion("1.0")] // Vincula esta controller à V1
[Route("api/v{version:apiVersion}/[controller]")]
//[Route("api/v1/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutosController(ProdutoService service)
    {
        _service = service;
    }


    // GET: api/produtos
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _service.Listar();

        return Ok(produtos);
    }


    // GET: api/produtos/{id}
    /// <summary>
    /// Obtém o produto pelo ID 
    /// </summary>
    /// <param name="id">O identificador único do produto.</param>
    /// <returns>Os detalhes do produto encontrado.</returns>
    /// <response code="200">Retorna o produto solicitado.</response>
    /// <response code="404">Se o produto não for encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Buscar(string id)
    {
        var produto = await _service.BuscarPorId(id);

        if (produto == null)
        {
            return NotFound(new
            {
                sucesso = false,
                mensagem = "Produto não encontrado."
            });
        }

        return Ok(produto);

    }


    // POST: api/produtos

    /// <summary>
    /// Permite cadastrar um produto novo.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição enviado no corpo do POST (Body):
    /// 
    ///     POST /api/produtos
    ///     {
    ///        "nome": "Smartphone 5G",
    ///        "preco": 2499.90,
    ///        "estoque": 50,
    ///        "descricao": "Aparelho celular com tela de 6.7 polegadas e 128GB"
    ///     }
    ///
    /// </remarks>
    /// <param name="produto">Os dados cadastrais do novo produto.</param>
    /// <returns>O produto cadastrado com seu ID gerado.</returns>
    /// <response code="201">Produto cadastrado com sucesso.</response>
    /// <response code="400">Se as informações enviadas no JSON forem inválidas.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor ao tentar salvar.</response>
    [HttpPost]
    public async Task<IActionResult> Inserir(
        Produto produto)
    {
        await _service.Inserir(produto);

        return CreatedAtAction(
            nameof(Buscar),
            new { id = produto.Id },
            produto
        );
    }


    // PUT: api/produtos/{id}

    [HttpPut("{id}")]
    public async Task<IActionResult> Alterar(
    string id,
    Produto produto)
    {
        var alterado = await _service.Alterar(id, produto);

        if (!alterado)
        {
            return NotFound(new
            {
                sucesso = false,
                mensagem = "Produto não encontrado."
            });
        }

        return Ok(new
        {
            sucesso = true,
            mensagem = "Produto alterado com sucesso."
        });
    }


    // DELETE: api/produtos/{id}

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(string id)
    {
        var excluido = await _service.Excluir(id);

        if (!excluido)
        {
            return NotFound(new
            {
                sucesso = false,
                mensagem = "Produto não encontrado."
            });
        }

        return Ok(new
        {
            sucesso = true,
            mensagem = "Produto excluído com sucesso."
        });
    }
}