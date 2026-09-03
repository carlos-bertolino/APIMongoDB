using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIMongoDB.Models
{
    public class Pedido
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string ClienteId { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        public List<PedidoItem> Itens { get; set; } = new();

        public decimal ValorTotal { get; set; }
    }

    public class PedidoItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string ProdutoId { get; set; }

        public required string NomeProduto { get; set; } // Histórico do nome no momento da compra

        public decimal PrecoUnitario { get; set; }

        public int Quantidade { get; set; }

        public decimal Subtotal => PrecoUnitario * Quantidade;
    }
}
