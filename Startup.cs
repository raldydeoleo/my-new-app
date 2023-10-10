using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using my_new_app.Models;
using ServiceStack.Configuration;
using System;

namespace my_new_app
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

            services.AddControllersWithViews();

            /*try
            {
                //Configurando la clase para manejar las config. en el archivo appSettings.json
                var appSettingsSection = Configuration.GetSection("AppSettings");
                services.Configure<AppSettings>(appSettingsSection);
                var appSettings = appSettingsSection.Get<AppSettings>();
                
                //Configurando el contexto EF para los Datos Maestros
                //services.AddDbContext<MaestrosDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"), SqlServerOptions => SqlServerOptions.CommandTimeout(appSettings.CommandTimeout)));
                //Configurando el contexto EF para Box Track Label
                //services.AddEntityFrameworkSqlServer().AddDbContext<SpcDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("boxTrackConnection"), SqlServerOptions => SqlServerOptions.CommandTimeout(appSettings.CommandTimeout)));
                //Obteniendo la clave secreta para cifrar los tokens desde appSettings.json
                // var key = Encoding.UTF8.GetBytes(appSettings.Secret);
               
                //Configurando la ruta de nuestra single page application (angular)
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });

                //Permitiendo conexiones desde otros origenes
                services.AddCors(action => {
                    action.AddPolicy("MyCorsPolicy", builder =>
                    {
                        builder //.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(x => true)
                        .Build();
                    });
                });
              
                
               // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               //     .AddJsonOptions(ConfigureJson);

                
            }
            catch (Exception ex)
            {
                throw ; //, ex.Message);
            }*/

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            //INICIA LA APLICACION FRONTEND en el folder ClientApp automaticamente al lanzar la aplicacion backend .NET Core.
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
