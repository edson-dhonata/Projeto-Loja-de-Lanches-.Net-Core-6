using FastReport.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_2022.Areas.Admin.Services;
using MVC_2022.Context;
using MVC_2022.Models;
using MVC_2022.Repositories;
using MVC_2022.Repositories.Interfaces;
using MVC_2022.Services;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);

var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var pastaImagens = builder.Configuration.GetSection("ConfigurationPastaImagens");

//Injetando DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conectionString));

//Conexão com sql
FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

//Criando o serviço para pegar o caminho da pasta de imagens.
builder.Services.Configure<ConfigurationImagens>(pastaImagens);

//Caso queira alterar a políticade senha do identity
builder.Services.Configure<IdentityOptions>(options => 
{
    //Padrão de política de senha do identity
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

#region Tipo de escopos
//Criando o serviço de injeção de dependência de Lanches e Categorias para que os controladores possam ter acessos aos dados de forma mais eficiente
//sem a necessidade de instaciar as classes.

//A três tipos de injeção de depência: Escopo(vida útil) dos serviços. Esses escopos afetam como o serviço ´é resolvido e descartado pelo provedor de serviços.

//1- Transiente (builder.Services.AddTransient<interface,repositório> ou <etc,etc>): Uma nova instância do serviço é criada cada vez que um serviço é solicitado do provedor de serviços.
//Se o serviço for descartável, o escopo do serviço monitorará todas as instâncias do serviço e destruirá todas as intâncias do serviço criadas nesse escopo
//quando o escopo do serviço for descartável.

//2- Scoped (builder.Services.AddScoped<interface,repositório> ou <etc,etc>): Uma nova instância do serviço é criada em cada request. A cada requisição temos uma nova instância do serviço. 
//Se o serviço for descartável, ele será descartado quando o escopo do serviço for descartado.

//3- Sungleton (builder.Services.AddSungleton<interface,repositório> ou <etc,etc>): Apenas uma instância do serviço é criada se ainda não estiver registrada como uma instância.
// Um objeto do serviço é criado e fornecido para todas as requisições. Todas as requisições obtém o mesmoi objeto.
#endregion

builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Serviço de Seed inicial dos perfis e logins de usuário padrões.
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

//Criando servico para injetar método no controller, como não tem interface.
builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<GraficoVendasService>();
builder.Services.AddScoped<RelatorioLanchesServices>();

//Politica para adicionar os perfis.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        politica =>
        {
            politica.RequireRole("Admin");
        });
});

//Cria um  serviço para gerar uma instancia do carrinho de compra.
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

//Adicionando midwares de cache e session
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

//Adicionando serviço de páginação do Nuget ReflectionIT.
builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. 
    app.UseHsts();
}
app.UseHttpsRedirection();

//Permitir o acesso e também utilizado para servir arquivos estáticos(como HTML, CSS, JavaScript, imagens, entre outros)
//diretamente do sistema de arquivos do servidor web ao cliente,
//sem a necessidade de processamento adicional ou processamento do servidor.
app.UseStaticFiles();
app.UseFastReport();

//Usando session
app.UseSession();

app.UseRouting();

CriarPerfisUsuarios(app);

app.UseAuthentication();
app.UseAuthorization();

//Define rotas para meus controllers:
app.UseEndpoints(endpoints =>
{
    //Mapeia rota de Area criada pelo próprio scaffolding.
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

    //Rota para listar lanche pela categoria
    endpoints.MapControllerRoute(
        name: "categoriaFiltro",
        pattern: "Lanche/{action}/{categoria?}",
        defaults: new { Controller = "Lanche", action = "List" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

//IServiceScopeFactory - Cria instancia dos serviços no escopo
void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using(var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();

        //Iniciando usuários padrões admin e member.
        //Cria usuários e atribui aos perfis criados
        service.SeedUsers();
        service.SeedRoles();
    }
}