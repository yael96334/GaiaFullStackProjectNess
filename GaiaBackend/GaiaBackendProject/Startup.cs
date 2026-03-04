using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using CalculationProject.Models;

using CalculationProject.Repositories;
using CalculationProject.Services;

namespace WebApplication1
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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()    
                           .AllowAnyMethod()    
                           .AllowAnyHeader());  
            });
            services.AddScoped<ICalculationRepository, CalculationRepository>();
            services.AddScoped<ICalculatorService, CalculatorService>();
          
            var builder2 = new ConfigurationBuilder()
       .AddConfiguration(Configuration)
       .AddJsonFile("operations.json", optional: false, reloadOnChange: true);

            var operationsConfig = builder2.Build();

            services.Configure<OperationConfig>(operationsConfig);


            services.AddSingleton<OperationEngineService>();
            services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseMvc();
            app.UseCors("AllowAll");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Calculator}/{action=GetNum}/{id?}");
            });
        }
    }
}
