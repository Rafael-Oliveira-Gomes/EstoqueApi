using EstoqueApi.Model;

namespace EstoqueApi.Interface.Service;

public interface IProdutoService
{
    Task<bool> AdicionaProduto(ProdutoDto produtoDto);
    Task<bool> RemoverProduto(int produtoId);
    Task<Produto> GetProduto(int produtoId);
    Task<Produto> GetProdutoPorNome(string nomeProduto);
    Task<List<Produto>> ListarProdutos();
    Task<int> UpdateProduto(ProdutoDto produtoDto);
}