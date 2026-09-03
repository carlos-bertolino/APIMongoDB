namespace APIMongoDB.Models
{
    // Interfaces/IPedidoService.cs
    public interface IPedidoService
    {
        Task<Pedido> CriarPedido(CriarPedidoInputModel input);
        Task<Pedido?> ObterPorId(string id);
    }

    

}
