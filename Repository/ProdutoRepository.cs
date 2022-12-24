using EstoqueApi.Config.Context;
using EstoqueApi.Interface.Repository;
using EstoqueApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Repository {
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MySqlContext _context;

        public ProdutoRepository(MySqlContext context) {
            _context = context;
        }

        public async Task<List<Produto>> ListProdutos() {
            List<Produto> list = await _context.Produto.OrderBy(p => p.Nome).ToListAsync();
            return list;
        }

        public async Task<Produto> GetProdutoById(int produtoId) {
            Produto Produto = await _context.Produto.FirstOrDefaultAsync((p => p.Id == produtoId));
            return Produto;
        }

        public async Task<Produto> GetProdutoPorNome(string nomeProduto) {
            Produto Produto = await _context.Produto.Where(x => x.Nome == nomeProduto).FirstOrDefaultAsync();
            return Produto;
        }

        public async Task<Produto> CreateProduto(Produto produto) {
            var ret = await _context.Produto.AddAsync(produto);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdateProduto(Produto produto) {
            _context.Entry(produto).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProdutoAsync(int produtoId) {
            var item = await _context.Produto.FindAsync(produtoId);
            _context.Produto.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
