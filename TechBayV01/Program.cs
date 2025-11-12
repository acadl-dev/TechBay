using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechBayV01.Data;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// CONFIGURACAO DO BANCO DE DADOS
// ============================================================================
// Obt�m a connection string do appsettings.json
// Se n�o encontrar, lan�a uma exce��o para evitar erro em runtime
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configura o Entity Framework Core para usar SQL Server
// DbTechBayV01DbContext � o contexto que gerencia as entidades do banco
builder.Services.AddDbContext<DbTechBayV01DbContext>(options =>
    options.UseSqlServer(connectionString));

// Adiciona p�gina especial para exibir erros de migration em desenvolvimento
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ============================================================================
// CONFIGURA��O DO SISTEMA DE AUTENTICA��O (ASP.NET IDENTITY)
// ============================================================================
// Configura o Identity para gerenciar usu�rios, senhas, login, etc
// RequireConfirmedAccount = true: usu�rio PRECISA confirmar email antes de fazer login
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DbTechBayV01DbContext>(); // Armazena dados de usu�rios no banco configurado

// ============================================================================
// CONFIGURA��O DAS RAZOR PAGES
// ============================================================================
builder.Services.AddRazorPages();

// IMPORTANTE: Configura pol�tica de seguran�a global
// Todas as p�ginas da aplica��o exigem usu�rio autenticado (logado)
// Exce��o: p�ginas do Identity (Login, Register, etc) s�o liberadas automaticamente
builder.Services.AddRazorPages(options => {
    // Cria uma pol�tica que exige usu�rio autenticado
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    // Aplica a pol�tica a TODAS as p�ginas da raiz "/"
    options.Conventions.AuthorizeFolder("/");
});

// Configura o comportamento do cookie de autentica��o
// Define para onde redirecionar usu�rios n�o autenticados
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Identity/Account/Login";   // P�gina de login padr�o
});

// ============================================================================
// BUILD DA APLICA��O
// ============================================================================
var app = builder.Build();

// ============================================================================
// CONFIGURA��O DO PIPELINE HTTP (MIDDLEWARE)
// ============================================================================
// Em desenvolvimento: mostra p�gina detalhada de erros de migration
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
// Em produ��o: redireciona para p�gina de erro gen�rica
else
{
    app.UseExceptionHandler("/Error");
}

// Permite servir arquivos est�ticos (CSS, JavaScript, imagens, etc)
app.UseStaticFiles();

// Ativa o sistema de roteamento
app.UseRouting();

// ORDEM CR�TICA: Authentication ANTES de Authorization
// Authentication: identifica QUEM � o usu�rio
app.UseAuthentication();

// Authorization: verifica se o usu�rio TEM PERMISS�O para acessar o recurso
app.UseAuthorization();

// Mapeia as Razor Pages para as rotas correspondentes
app.MapRazorPages();

// ============================================================================
// ROTA PERSONALIZADA PARA A RAIZ "/"
// ============================================================================
// Quando usu�rio acessa "/" (raiz), redireciona para "/choose_role"
// NOTA: S� chega aqui se estiver autenticado (devido � pol�tica global)
app.MapGet("/", context => {
    context.Response.Redirect("/choose_role");
    return Task.CompletedTask;
});


// INICIALIZAR AS ROLES COMPRADOR E VENDEDOR
async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Vendedor", "Comprador" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}


// ============================================================================
// INICIA A APLICA��O
// ============================================================================
app.Run();