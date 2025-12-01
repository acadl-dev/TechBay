using Microsoft.AspNetCore.Identity;

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
        public DateTime DataModificacao { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public Pedido? Pedido { get; set; }

        public string? VendedorId { get; set; } // Armazena o ID do vendedor, usado em filtros e queries, cria um campo na tabela
        public IdentityUser? Vendedor { get; set; } // Permite acessar dados do vendedor e usar Include, não cria campo na tabela
    }
}
