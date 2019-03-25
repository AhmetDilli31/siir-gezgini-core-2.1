using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SairGezgini.Core;
using SairGezgini.Core.Configuration;
using SiirGezgini.Business;
using SiirGezgini.Business.Interfaces;
using SiirGezgini.Repository;

namespace SairGezgini.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddAuthentication();
            services.AddMvc();

            services.AddSingleton(Configuration);
          
            InitDependecy(services);

        }

        private void InitDependecy(IServiceCollection services)
        {
            // bus
            services.AddTransient<IPoetBusiness, PoetBusiness>();
            services.AddTransient<IPoetOfPoemsBusiness, PoetOfPoemsBusiness>();
            services.AddTransient<IPoemBusiness, PoemBusiness>();
            services.AddTransient<ISearchBusiness, SearchBusiness>();

            //service
            services.AddTransient<IPoetServices, PoetServices>();
            services.AddTransient<IPoemService, PoemService>();

            //conf
            services.AddTransient<IConfigurationManager, ConfigurationManager>();

            services.BuildServiceProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "anasayfa",
                    template: "anasayfa",
                    defaults: new { controller = "Home", action = "Index" });

                route.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            IoCResolver.Init(app.ApplicationServices);

            app.UseMvc();

            app.UseStaticFiles();
            app.UseStatusCodePages();
        }
    }
}
