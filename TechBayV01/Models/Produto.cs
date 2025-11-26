namespace TechBayV01.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoEstoque { get; set; }
        public string Status { get; set; }
        public float Quantidade { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataModificação { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public Pedido? Pedido { get; set; }
    }
}
