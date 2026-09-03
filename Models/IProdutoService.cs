namespace APIMongoDB.Models
{
    // Interfaces/IProdutoService.cs (Assumindo que você já tenha algo parecido)
    public interface IProdutoService
    {
        Task<Produto?> ObterPorId(string id);
        Task<bool> AtualizarEstoque(string id, int quantidadeSubtrair);
    }
}
