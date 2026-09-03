
//Modelo de Entrada para a API (DTO / InputModel)
namespace APIMongoDB.Models
{

    public class CriarPedidoInputModel
    {
        /// <summary>
        /// Identificador único do cliente que está realizando a compra.
        /// </summary>
        /// <example>64f1a2b3c4d5e6f7a8b9c0a1</example>
        public required string ClienteId { get; set; }

        /// <summary>
        /// Lista de produtos incluídos no pedido.
        /// </summary>
        public required List<CriarPedidoItemInputModel> Itens { get; set; }
    }

    public class CriarPedidoItemInputModel
    {
        /// <summary>
        /// ID do produto cadastrado no banco.
        /// </summary>
        /// <example>64f1a2b3c4d5e6f7a8b9c0d1</example>
        public required string ProdutoId { get; set; }

        /// <summary>
        /// Quantidade de itens desejada.
        /// </summary>
        /// <example>2</example>
        public int Quantidade { get; set; }
    }
}