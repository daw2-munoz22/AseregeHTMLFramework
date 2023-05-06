using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MudBlazor;
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
            services.AddControllersWithViews();   //a�adir controlador de vistas         
            services.AddRazorPages(); //a�adir controlador de paginas 
            services.AddMvc(); //a�adir el Modelo - Vista - Controlador (APIs)
			services.AddControllers(); //a�adir controladores adicionales
            services.AddServerSideBlazor(); //a�adir el servidor de blazor			

            //services.AddScoped<AseregeBarcelonaWeb.Pages.Index>();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "AseregeBarcelonaWebAPI", Version = "v1" });});

            services.AddCors(options =>
            {                
                options.AddPolicy("AllowAll", builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
            });

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });            
        }
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "AseregeBarcelonaWebAPI"); });
           
            if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");//p�gina de error interno (Ejemplo: error 500)				
				app.UseHsts(); //A�ade soporte para seguridad estricta
			}
			
            app.UseHttpsRedirection(); //A�ade redireccion de http a https
            app.UseCors("AllowAll"); //evitar la politica de CORS
            app.UseStaticFiles(); //Permite el uso de recursos web

			app.UseRouting(); //A�ade el enrutador de paginas
           
            app.UseEndpoints(endpoints =>
			{
                endpoints.MapControllers(); //Asigna un controlador a un mapping
                endpoints.MapBlazorHub(); //Abre un puerto de signal R para blazor
				endpoints.MapFallbackToPage("/_Host"); //Pagina donde se injectara el codigo web o blazor
			});
		}
	}
}
