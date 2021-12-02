using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shoalace.API.Filters;
using Shoalace.Domain.Handlers;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Interfaces.Services;
using Shoalace.Infra.Contexto;
using Shoalace.Infra.Repositories;
using Shoalace.Infra.Services;

namespace Shoalace.API
{
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
            //Habilitando que outras origens podem acessar a nossa API -> Cross-Origin Resource Sharing(Compartilhamento de recursos entre origens)
            services.AddCors();

            services.AddControllers();

            //Adicionando a connection string do nosso banco de dados e informando que as Migrations serão feitas no projeto chamado Infra
            //services.AddDbContext<ShoalaceContexto>(options => options.UseInMemoryDatabase("DataBase"));
            services.AddDbContext<ShoalaceContexto>(options => options .UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"), m => m.MigrationsAssembly("Shoalace.Infra")));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter)); //Adicionando o Filtro de Exception
                options.Filters.Add(typeof(ActionFilter)); //Filtro de entrada de dados para preencher ClienteId e AcessoId
            }).AddJsonOptions(options =>
            { //Configurando a nossa resposta de JSON para não conter valores nullos e o máximo de 30 interações
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.MaxDepth = 30;
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            //Repositories
            services.AddScoped<IAcessoRepository, AcessoRepository>();
            services.AddScoped<IErroRepository, ErroRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<IMensagemRepository, MensagemRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Handlers
            services.AddScoped<AcessoHandler>();
            services.AddScoped<EventoHandler>();
            services.AddScoped<GrupoHandler>();
            services.AddScoped<MensagemHandler>();
            services.AddScoped<UsuarioHandler>();

            services.AddScoped<IFileUpload, FileUpload>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shoalace.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shoalace.API v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
