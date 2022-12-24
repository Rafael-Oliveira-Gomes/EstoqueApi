namespace EstoqueApi.Model {
    public class Produto {
        public Produto() {
        }

        public Produto(ProdutoDto dto) {
            Id = dto.Id;
            Nome = dto.Nome;
            QtdQuebrado = dto.QtdQuebrado;
            QtdFuncional = dto.QtdFuncional;
            Quantidade = dto.QtdFuncional + dto.QtdQuebrado;            
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set;}
        public string? UserUltimoUpdate { get; set; }
        public int QtdQuebrado { get; set; }
        public int QtdFuncional { get; set; }
    }

    public class ProdutoDto {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdQuebrado { get; set; }
        public int QtdFuncional { get; set; }
    }
}
