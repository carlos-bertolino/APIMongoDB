using APIMongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace APIMongoDB.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IMongoCollection<Produto> _produtos;

        public ProdutoService(IConfiguration configuration)
        {
            var connectionString =
                configuration["MongoDB:ConnectionString"];

            var databaseName =
                configuration["MongoDB:DatabaseName"];

            var collectionName =
                configuration["MongoDB:CollectionName"];

            var client = new MongoClient(connectionString);

            var database = client.GetDatabase(databaseName);

            _produtos = database.GetCollection<Produto>(collectionName);
        }


        // ==========================
        // LISTAR
        // ==========================

        public async Task<List<Produto>> Listar()
        {
            return await _produtos
                .Find(_ => true)
                .ToListAsync();
        }


        // ==========================
        // BUSCAR POR ID
        // ==========================

        public async Task<Produto?> BuscarPorId(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return null;

            return await _produtos
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }


        // ==========================
        // INSERIR
        // ==========================

        public async Task Inserir(Produto produto)
        {
            await _produtos.InsertOneAsync(produto);
        }


        // ==========================
        // ALTERAR
        // ==========================

        public async Task<bool> Alterar(
    string id,
    Produto produto)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return false;

            var filtro = Builders<Produto>
                .Filter
                .Eq(x => x.Id, id);

            var alteracao = Builders<Produto>
                .Update
                .Set(x => x.Nome, produto.Nome)
                .Set(x => x.Preco, produto.Preco)
                .Set(x => x.Estoque, produto.Estoque);

            var resultado = await _produtos
                .UpdateOneAsync(filtro, alteracao);

            return resultado.MatchedCount > 0;
        }


        // ==========================
        // EXCLUIR
        // ==========================

        public async Task<bool> Excluir(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return false;

            var resultado = await _produtos
                .DeleteOneAsync(x => x.Id == id);

            return resultado.DeletedCount > 0;
        }



        // O restante do seu código atual continua igual aqui dentro...

        public async Task<Produto?> ObterPorId(string id)
        {
            return null;
        }

        public async Task<bool> AtualizarEstoque(string id, int quantidadeSubtrair)
        {
            return false;
        }
    }
}
