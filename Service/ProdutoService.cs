using EstoqueApi.Model;
using EstoqueApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using EstoqueApi.Interface.Repository;
using EstoqueApi.Interface.Service;

namespace EstoqueApi.Service {
    public class ProdutoService : IProdutoService
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly IAuthService _authService;
        public ProdutoService(IProdutoRepository produtoRepository, IAuthService authService) {
            _produtoRepository = produtoRepository;
            _authService = authService;
        }

        public async Task<bool> AdicionaProduto(ProdutoDto produtoDto) {
            Produto produto = new Produto(produtoDto);

            ApplicationUser currentUser = await _authService.GetCurrentUser();
            produto.UserUltimoUpdate = currentUser.UserName;

            Produto result = await _produtoRepository.CreateProduto(produto);
            return true;
        }

        public async Task<bool> RemoverProduto(int produtoId) {

            await _produtoRepository.DeleteProdutoAsync(produtoId);
            return true;
        }

        public async Task<Produto> GetProduto(int produtoId) {
            Produto? produto = await _produtoRepository.GetProdutoById(produtoId);
            if (produto == null) throw new Exception("Não encontrou");

            return produto;
        }

        public async Task<Produto> GetProdutoPorNome(string nomeProduto) {
            Produto? produto = await _produtoRepository.GetProdutoPorNome(nomeProduto);
            if (produto == null) throw new Exception("Não encontrou");

            return produto;
        }
        
        public async Task<List<Produto>> ListarProdutos() {
            var produtos = await _produtoRepository.ListProdutos();

            return produtos;
        }

        public async Task<int> UpdateProduto(ProdutoDto produtoDto) {
            Produto produto = new Produto(produtoDto);

            ApplicationUser currentUser = await _authService.GetCurrentUser();
            produto.UserUltimoUpdate = currentUser.UserName;

            return await _produtoRepository.UpdateProduto(produto);
        }
        
    }
}
