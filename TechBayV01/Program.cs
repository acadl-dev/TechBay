using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechBayV01.Data;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// CONFIGURACAO DO BANCO DE DADOS
// ============================================================================
// Obtém a connection string do appsettings.json
// Se não encontrar, lança uma exceção para evitar erro em runtime
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configura o Entity Framework Core para usar SQL Server
// DbTechBayV01DbContext é o contexto que gerencia as entidades do banco
builder.Services.AddDbContext<DbTechBayV01DbContext>(options =>
    options.UseSqlServer(connectionString));

// Adiciona página especial para exibir erros de migration em desenvolvimento
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ============================================================================
// CONFIGURAÇÃO
// DO SISTEMA DE AUTENTICAÇÃO (ASP.NET IDENTITY)
// ============================================================================
// Configura o Identity para gerenciar usuários, senhas, login, etc
// RequireConfirmedAccount = true: usuário PRECISA confirmar email antes de fazer login
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Importante para habilitar o uso de Roles
    .AddEntityFrameworkStores<DbTechBayV01DbContext>(); // Armazena dados de usuários no banco configurado

// ============================================================================
// CONFIGURAÇÃO DAS RAZOR PAGES
// ============================================================================
builder.Services.AddRazorPages();

// IMPORTANTE: Configura política de segurança global
// Todas as páginas da aplicação exigem usuário autenticado (logado)
// Exceção: páginas do Identity (Login, Register, etc) são liberadas automaticamente
builder.Services.AddRazorPages(options => {
    // Cria uma política que exige usuário autenticado
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    // Aplica a política a TODAS as páginas da raiz "/"
    options.Conventions.AuthorizeFolder("/");
});

// Configura o comportamento do cookie de autenticação
// Define para onde redirecionar usuários não autenticados
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Identity/Account/Login";   // Página de login padrão
});

// ============================================================================
// BUILD DA APLICAÇÃO
// ============================================================================
var app = builder.Build();

// ============================================================================
// CONFIGURAÇÃO DO PIPELINE HTTP (MIDDLEWARE)
// ============================================================================
// Em desenvolvimento: mostra página detalhada de erros de migration
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
// Em produção: redireciona para página de erro genérica
else
{
    app.UseExceptionHandler("/Error");
}

// Permite servir arquivos est�ticos (CSS, JavaScript, imagens, etc)
app.UseStaticFiles();

// Ativa o sistema de roteamento
app.UseRouting();

// ORDEM CR�TICA: Authentication ANTES de Authorization
// Authentication: identifica QUEM é o usuário
app.UseAuthentication();

// Authorization: verifica se o usuário TEM PERMISS�O para acessar o recurso
app.UseAuthorization();

// Mapeia as Razor Pages para as rotas correspondentes
app.MapRazorPages();

await CriarRolesPadraoAsync(app);

// ============================================================================
// ROTA PERSONALIZADA PARA A RAIZ "/"
// ============================================================================
// Quando usuário acessa "/" (raiz), redireciona para "/choose_role"
// NOTA: Só chega aqui se estiver autenticado (devido á política global)
//app.MapGet("/", context => {
//    context.Response.Redirect("/choose_role");
//    return Task.CompletedTask;
//});



// ============================================================================
// INICIA A APLICAÇÃO
// ============================================================================
app.Run();

// CRIA AS ROLES ATRAVÉS DO IDENTITY, APÓS RODAR A PRIMEIRA VEZ PODE SER RETIRADO
async Task CriarRolesPadraoAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Comprador", "Vendedor" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}