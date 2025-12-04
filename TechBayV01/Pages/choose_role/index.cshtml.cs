using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace TechBayV01.Pages.choose_role
{
    public class choose_roleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public choose_roleModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [BindProperty]
        public string SelectedRole { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Chegamos aqui!");
            var user = await _userManager.GetUserAsync(User);
            Console.WriteLine("User: " + user);
            Console.WriteLine("SelectedRole: " + SelectedRole);
            if (user == null)
                return RedirectToPage("/Account/Login");

            // Garante que a role existe
            // if (!await _roleManager.RoleExistsAsync(SelectedRole))
            // await _roleManager.CreateAsync(new IdentityRole(SelectedRole));

            // Adiciona o usuário à role
            var result = await _userManager.AddToRoleAsync(user, SelectedRole);
            Console.WriteLine("result: " + result);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Erro ao adicionar role: {error.Code} - {error.Description}");
                }
                Console.WriteLine("Vai redirecionar...");
                return RedirectToPage("/vendedor/Index"); // volta pra página com erro
            } else
            {
                Console.WriteLine("O usuário foi adicionado à role.");
            }

            // Redireciona conforme a role escolhida
            if (SelectedRole == "Comprador")
            {
                Console.WriteLine("Sucesso ao associar o comprador à role.");
                return RedirectToPage("/comprador/Index");
            }
            else if (SelectedRole == "Vendedor")
            {
                Console.WriteLine("Sucesso ao associar vendedor à role.");
                return RedirectToPage("/vendedor/Index");
            }
            else
            {
                Console.WriteLine("Erro ao salvar a role.");
                return RedirectToPage("/vendedor/Index");
            }
        }
    }
}
