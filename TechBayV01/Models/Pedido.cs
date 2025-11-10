namespace TechBayV01.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
        public int ProdutoId { get; set; }
        public decimal ValorTotal { get; set; }
        public string StatusPedido { get; set; }

        public Produto? Produto { get; set; }
    }
}
