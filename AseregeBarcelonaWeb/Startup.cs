using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;

namespace AseregeBarcelonaWeb
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
            
        public void ConfigureServices(IServiceCollection services)
		{
            services.AddControllersWithViews();   //añadir controlador de vistas         
            services.AddRazorPages(); //añadir controlador de paginas 
            services.AddMvc(); //añadir el Modelo - Vista - Controlador (APIs)
			services.AddControllers(); //añadir controladores adicionales
            services.AddServerSideBlazor(); //añadir el servidor de blazor

			services.AddScoped<AseregeBarcelonaWeb.Pages.Index>();

			services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
            });

            services.AddMudServices();
        }
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");//página de error interno (Ejemplo: error 500)				
				app.UseHsts(); //Añade soporte para seguridad estricta
			}
			
            app.UseHttpsRedirection(); //Añade redireccion de http a https
            app.UseCors("AllowAll"); //evitar la politica de CORS
            app.UseStaticFiles(); //Permite el uso de recursos web

			app.UseRouting(); //Añade el enrutador de paginas
           
            app.UseEndpoints(endpoints =>
			{
                endpoints.MapControllers(); //Asigna un controlador a un mapping
                endpoints.MapBlazorHub(); //Abre un puerto de signal R para blazor
				endpoints.MapFallbackToPage("/_Host"); //Pagina donde se injectara el codigo web o blazor
			});
		}
	}
}
