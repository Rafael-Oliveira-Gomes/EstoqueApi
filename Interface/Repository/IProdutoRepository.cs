using EstoqueApi.Model;

namespace EstoqueApi.Interface.Repository;

public interface IProdutoRepository
{
    Task<List<Produto>> ListProdutos();
    Task<Produto> GetProdutoById(int produtoId);
    Task<Produto> GetProdutoPorNome(string nomeProduto);
    Task<Produto> CreateProduto(Produto produto);
    Task<int> UpdateProduto(Produto produto);
    Task<bool> DeleteProdutoAsync(int produtoId);
}