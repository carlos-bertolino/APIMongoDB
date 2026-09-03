
using APIMongoDB.Models;
using APIMongoDB.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;


namespace APIMongoDB.Controllers.v2;

[ApiController]
[ApiVersion("2.0")] // Vincula esta controller à V2 🚀
[Route("api/v{version:apiVersion}/[controller]")]
//[Route("/api/v2/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutosController(ProdutoService service)
    {
        _service = service;
    }


    // GET: api/produtos
    /// <summary>
    /// Lista de todos os produtos
    /// </summary>
    /// <returns>Listagem de todos os produtos cadastrados.</returns>
    /// <response code="200">Retorna a lista de  produtos cadastrados.</response>
    /// <response code="404">Se o produto não for encontrado.</response>
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
    /// <summary>
    /// Atualiza as informações de um produto existente pelo ID.
    /// </summary>
    /// <remarks>
    /// Exemplo de JSON para enviar no corpo da requisição (Body):
    /// 
    ///     PUT /api/produtos/64f1a2b3c4d5e6f7a8b9c0d1
    ///     {
    ///        "nome": "Teclado Mecânico RGB (Atualizado)",
    ///        "preco": 389.90,
    ///        "estoque": 25,
    ///        "descricao": "Teclado mecânico com switches novos e iluminação customizável."
    ///     }
    ///
    /// </remarks>
    /// <param name="id">O identificador único (GUID ou string) do produto que será alterado.</param>
    /// <param name="produto">O objeto contendo os novos dados do produto.</param>
    /// <returns>Um objeto indicando o status do sucesso da operação.</returns>
    /// <response code="200">Produto alterado com sucesso no banco de dados.</response>
    /// <response code="404">Se o ID fornecido não corresponder a nenhum produto cadastrado.</response>
    /// <response code="400">Se o formato dos dados enviados no JSON for inválido.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    /// <summary>
    /// Remove permanentemente um produto do sistema pelo ID.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     DELETE /api/produtos/64f1a2b3c4d5e6f7a8b9c0d1
    ///
    /// </remarks>
    /// <param name="id">O identificador único (GUID ou string) do produto que será excluído.</param>
    /// <returns>Um objeto indicando o status do sucesso da exclusão.</returns>
    /// <response code="200">Produto removido com sucesso do banco de dados.</response>
    /// <response code="404">Se o ID fornecido não corresponder a nenhum produto cadastrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
