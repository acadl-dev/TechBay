using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechBayV01.Data;

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
        public void OnGet()
        {
        }

        [BindProperty]
        public Models.Produto Produto { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
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
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o cliente. Tente novamente.");
                return Page();
            }

        }
    }
}
