using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TechBayV01.Data;
using TechBayV01.Models;


namespace TechBayV01.Pages.vendedor
{
    public class IndexModel : PageModel
    {
        // recebe o dbcontext via injeção de dependência e guarda para usar nos métodos da página
        private readonly DbTechBayV01DbContext _context;

        public IndexModel(DbTechBayV01DbContext context)
            {
            _context = context;
        }
        public IList<Produto> Produtos { get; set; } = default!;
        public async Task OnGetAsync()
        {
            Produtos = await _context.Produto.ToListAsync();
        }

        [BindProperty]
        public Models.Produto Produto { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var json = JsonSerializer.Serialize(Produto);
            Console.WriteLine("[DEBUG PRODUTO] " + json);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Define a data de cadastro no momento da criação
                Produto.DataCadastro = DateTime.Now;
                Produto.DataModificacao = DateTime.Now;

                _context.Produto.Add(Produto);
                await _context.SaveChangesAsync();

                // Define mensagem de sucesso
                TempData["SuccessMessage"] = $"Produto '{Produto.Nome}' criado com sucesso!";

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o produto. Tente novamente.");
                return Page();
            }

        }
    }
}
