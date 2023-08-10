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

//Conex�o com sql
FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

//Criando o servi�o para pegar o caminho da pasta de imagens.
builder.Services.Configure<ConfigurationImagens>(pastaImagens);

//Caso queira alterar a pol�ticade senha do identity
builder.Services.Configure<IdentityOptions>(options => 
{
    //Padr�o de pol�tica de senha do identity
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

#region Tipo de escopos
//Criando o servi�o de inje��o de depend�ncia de Lanches e Categorias para que os controladores possam ter acessos aos dados de forma mais eficiente
//sem a necessidade de instaciar as classes.

//A tr�s tipos de inje��o de dep�ncia: Escopo(vida �til) dos servi�os. Esses escopos afetam como o servi�o �� resolvido e descartado pelo provedor de servi�os.

//1- Transiente (builder.Services.AddTransient<interface,reposit�rio> ou <etc,etc>): Uma nova inst�ncia do servi�o � criada cada vez que um servi�o � solicitado do provedor de servi�os.
//Se o servi�o for descart�vel, o escopo do servi�o monitorar� todas as inst�ncias do servi�o e destruir� todas as int�ncias do servi�o criadas nesse escopo
//quando o escopo do servi�o for descart�vel.

//2- Scoped (builder.Services.AddScoped<interface,reposit�rio> ou <etc,etc>): Uma nova inst�ncia do servi�o � criada em cada request. A cada requisi��o temos uma nova inst�ncia do servi�o. 
//Se o servi�o for descart�vel, ele ser� descartado quando o escopo do servi�o for descartado.

//3- Sungleton (builder.Services.AddSungleton<interface,reposit�rio> ou <etc,etc>): Apenas uma inst�ncia do servi�o � criada se ainda n�o estiver registrada como uma inst�ncia.
// Um objeto do servi�o � criado e fornecido para todas as requisi��es. Todas as requisi��es obt�m o mesmoi objeto.
#endregion

builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Servi�o de Seed inicial dos perfis e logins de usu�rio padr�es.
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

//Criando servico para injetar m�todo no controller, como n�o tem interface.
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

//Cria um  servi�o para gerar uma instancia do carrinho de compra.
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

//Adicionando midwares de cache e session
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

//Adicionando servi�o de p�gina��o do Nuget ReflectionIT.
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

//Permitir o acesso e tamb�m utilizado para servir arquivos est�ticos(como HTML, CSS, JavaScript, imagens, entre outros)
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
    //Mapeia rota de Area criada pelo pr�prio scaffolding.
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

//IServiceScopeFactory - Cria instancia dos servi�os no escopo
void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using(var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();

        //Iniciando usu�rios padr�es admin e member.
        //Cria usu�rios e atribui aos perfis criados
        service.SeedUsers();
        service.SeedRoles();
    }
}