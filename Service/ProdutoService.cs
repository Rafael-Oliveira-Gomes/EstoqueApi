using EstoqueApi.Model;
using EstoqueApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace EstoqueApi.Service {
    public class ProdutoService {

        private readonly ProdutoRepository _produtoRepository;
        private readonly AuthService _authService;
        public ProdutoService(ProdutoRepository produtoRepository, AuthService authService) {
            _produtoRepository = produtoRepository;
            _authService = authService;
        }

        public async Task<bool> AdicionaProduto(ProdutoDto produtoDto) {
            var produto = new Produto(produtoDto);

            var currentUser = await _authService.GetCurrentUser();
            produto.UserUltimoUpdate = currentUser.UserName;

            var result = await _produtoRepository.CreateProduto(produto);
            return true;
        }

        public async Task<bool> RemoverProduto(int produtoId) {

            await _produtoRepository.DeleteProdutoAsync(produtoId);
            return true;
        }

        public async Task<Produto> GetProduto(int produtoId) {
            var produto = await _produtoRepository.GetProdutoById(produtoId);
            if (produto == null) throw new Exception("Não encontrou");

            return produto;
        }

        public async Task<Produto> GetProdutoPorNome(string nomeProduto) {
            var produto = await _produtoRepository.GetProdutoPorNome(nomeProduto);
            if (produto == null) throw new Exception("Não encontrou");

            return produto;
        }
        
        public async Task<List<Produto>> ListarProdutos() {
            var produtos = await _produtoRepository.ListProdutos();

            return produtos;
        }

        public async Task<int> UpdateProduto(ProdutoDto produtoDto) {
            var produto = new Produto(produtoDto);

            var currentUser = await _authService.GetCurrentUser();
            produto.UserUltimoUpdate = currentUser.UserName;

            return await _produtoRepository.UpdateProduto(produto);
        }
        
    }
}
