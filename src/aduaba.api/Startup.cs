using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aduaba.api.Interface;
using aduaba.api.Services;
using aduaba.api.AppDbContext;
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
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using Microsoft.AspNetCore.Identity;
using aduaba.api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                Configuration.GetConnectionString("MyAduabaWebDb")
            ));


            //Registering AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserInterface, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductInterface, ProductService>();
            services.AddScoped<ICategoryInterface, CategoryService>();
            services.AddScoped<IWishListInterface, WishListService>();
            services.AddScoped<IAddressInterface, AddressService >();

            //Configuration from AppSettings
            services.Configure<JWT>(Configuration.GetSection("JWT"));
            //User Manager Service
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IUserInterface, UserService>();
            //Adding Athentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });


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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "aduaba.api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MigrateDatabaseContext(svp);
            CreateDefaultRoleAndUserAsync(svp).GetAwaiter().GetResult();
        }

        public void MigrateDatabaseContext(IServiceProvider svp)
        {
            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }

        public async Task CreateDefaultRoleAndUserAsync(IServiceProvider svp)
        {
            string[] roles = new string[] {
                "Administrator","Moderator","User"
            };
            var FirstName = "Andrey";
            var LastName = "Gabriel";
            var userEmail = "AndyGabriel@gmail.com";
            var userPassword = "Osabu100.";

            var roleManager = svp.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            var userManager = svp.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                user = new ApplicationUser
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = userEmail,
                    UserName = userEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "+2349087675652",
                    PhoneNumberConfirmed = true
                };

                await userManager.CreateAsync(user, userPassword);
                await userManager.AddToRolesAsync(user, roles);

            }
        }
    }
}
