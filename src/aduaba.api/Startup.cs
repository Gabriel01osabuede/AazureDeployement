using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aduaba.api.Interface;
using aduaba.api.Services;
using aduaba.data.AppDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace aduaba.api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "aduaba.api", Version = "v1" });
            });

            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("MyAduabaDb"),
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("aduaba.data");
                }
            ));

            //Registering AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IProductInterface, ProductService>();
            services.AddScoped<ICategoryInterface, CategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "aduaba.api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MigrateDatabaseContext(svp);
        }

        public void MigrateDatabaseContext(IServiceProvider svp)
        {
            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }
    }
}
