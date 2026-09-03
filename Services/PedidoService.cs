using APIMongoDB.Models;
using MongoDB.Driver;

namespace APIMongoDB.Services
{
    

    public class PedidoService : IPedidoService
    {
        private readonly IMongoCollection<Pedido> _pedidosCollection;
        private readonly IProdutoService _produtoService; // Usado para buscar preços e atualizar estoque

        public PedidoService(IMongoDatabase database, IProdutoService produtoService)
        {
            // Obtém a coleção do MongoDB para pedidos
            _pedidosCollection = database.GetCollection<Pedido>("Pedidos");
            _produtoService = produtoService;
        }

        public async Task<Pedido> CriarPedido(CriarPedidoInputModel input)
        {
            var novoPedido = new Pedido
            {
                ClienteId = input.ClienteId,
                DataPedido = DateTime.UtcNow,
                Itens = new List<PedidoItem>()
            };

            decimal valorTotalPedido = 0;

            foreach (var itemInput in input.Itens)
            {
                // 1. Busca o produto atualizado no banco
                var produto = await _produtoService.ObterPorId(itemInput.ProdutoId);
                if (produto == null)
                {
                    throw new Exception($"Produto com ID {itemInput.ProdutoId} não foi encontrado.");
                }

                // 2. Validação de estoque
                if (produto.Estoque < itemInput.Quantidade)
                {
                    throw new Exception($"Estoque insuficiente para o produto '{produto.Nome}'. Disponível: {produto.Estoque}. Solicitado: {itemInput.Quantidade}.");
                }

                // 3. Monta o item com o preço real e calcula o subtotal
                var pedidoItem = new PedidoItem
                {
                    ProdutoId = itemInput.ProdutoId,
                    NomeProduto = produto.Nome,
                    PrecoUnitario = produto.Preco, // Garante o preço real do banco
                    Quantidade = itemInput.Quantidade
                };

                valorTotalPedido += pedidoItem.Subtotal;
                novoPedido.Itens.Add(pedidoItem);

                // 4. Atualiza/Abate o estoque do produto
                await _produtoService.AtualizarEstoque(itemInput.ProdutoId, itemInput.Quantidade);
            }

            novoPedido.ValorTotal = valorTotalPedido;

            // 5. Salva o pedido finalizado no MongoDB
            await _pedidosCollection.InsertOneAsync(novoPedido);

            return novoPedido;
        }

        public async Task<Pedido?> ObterPorId(string id)
        {
            return await _pedidosCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
    }


}
