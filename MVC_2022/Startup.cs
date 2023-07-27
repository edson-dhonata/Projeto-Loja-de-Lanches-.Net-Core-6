
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_2022.Areas.Admin.Services;
using MVC_2022.Context;
using MVC_2022.Models;
using MVC_2022.Repositories;
using MVC_2022.Repositories.Interfaces;
using MVC_2022.Services;
using ReflectionIT.Mvc.Paging;

namespace MVC_2022;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //Injetando DbContext
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        //Criando o serviço para pegar o caminho da pasta de imagens.
        services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens"));

        //Caso queira alterar a políticade senha do identity
        services.Configure<IdentityOptions>(options => { 
        
            //Padrão de política de senha do identity
            options.Password.RequireDigit= true;
            options.Password.RequireLowercase= true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase= true;
            options.Password.RequiredLength= 8;
            options.Password.RequiredUniqueChars = 1;
        });

        #region Tipo de escopos
        //Criando o serviço de injeção de dependência de Lanches e Categorias para que os controladores possam ter acessos aos dados de forma mais eficiente
        //sem a necessidade de instaciar as classes.

        //A três tipos de injeção de depência: Escopo(vida útil) dos serviços. Esses escopos afetam como o serviço ´é resolvido e descartado pelo provedor de serviços.

        //1- Transiente (services.AddTransient<interface,repositório> ou <etc,etc>): Uma nova instância do serviço é criada cada vez que um serviço é solicitado do provedor de serviços.
        //Se o serviço for descartável, o escopo do serviço monitorará todas as instâncias do serviço e destruirá todas as intâncias do serviço criadas nesse escopo
        //quando o escopo do serviço for descartável.

        //2- Scoped (services.AddScoped<interface,repositório> ou <etc,etc>): Uma nova instância do serviço é criada em cada request. A cada requisição temos uma nova instância do serviço. 
        //Se o serviço for descartável, ele será descartado quando o escopo do serviço for descartado.

        //3- Sungleton (services.AddSungleton<interface,repositório> ou <etc,etc>): Apenas uma instância do serviço é criada se ainda não estiver registrada como uma instância.
        // Um objeto do serviço é criado e fornecido para todas as requisições. Todas as requisições obtém o mesmoi objeto.
        #endregion

        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //Serviço de Seed inicial dos perfis e logins de usuário padrões.
        services.AddScoped<ISeedUserRoleInitial,SeedUserRoleInitial>();

        //Criando servico para injetar método no controller, como não tem interface.
        services.AddScoped<RelatorioVendasService>();

        //Politica para adicionar os perfis.
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
                });
        });

        //Cria um  serviço para gerar uma instancia do carrinho de compra.
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        //Adicionando midwares de cache e session
        services.AddMemoryCache();
        services.AddSession();

        services.AddControllersWithViews();

        //Adicionando serviço de páginação do Nuget ReflectionIT.
        services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
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

        //Usando session
        app.UseSession();

        app.UseRouting();

        //Iniciando usuários padrões admin e member.
        //Cria perfis
        seedUserRoleInitial.SeedRoles();
        //Cria usuários e atribui aos perfis criados
        seedUserRoleInitial.SeedUsers();

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
    }
}
