namespace EstoqueApi.Model {
    public class Produto {
        public Produto() {
        }

        public Produto(ProdutoDto dto) {
            Id = dto.Id;
            Nome = dto.Nome;
            Quantidade = dto.Quantidade;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set;}
        public string? UserUltimoUpdate { get; set; }
    }

    public class ProdutoDto {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }
}
