using E_commerc3D.Areas.AdminAreas.Models;
using E_commerc3D.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;


namespace E_commerc3D
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddAuthentication()
          .AddGoogle(options =>
          {
              IConfigurationSection googleAuthNSection =
                  Configuration.GetSection("Authentication:Google");
              options.ClientId = "24640833825-jairush5krtemvhegmu80gn0grfaon2m.apps.googleusercontent.com";
              options.ClientSecret = "DU9wqTNTDrMMnSkScfhPpmjv";
              options.SaveTokens = true;

              /*     options.Events.OnCreatingTicket = ctx =>
                     {
                         List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();

                         tokens.Add(new AuthenticationToken()
                         {

                             Name = "TicketCreated",
                             Value = DateTime.UtcNow.ToString()
                         });

                         ctx.Properties.StoreTokens(tokens);

                         return Task.CompletedTask;
                     };
              */
          });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditProductsPolicy",
                    policy => policy.RequireClaim("Edit Products")
                    );
                options.AddPolicy("DeleteProductsPolicy",
                    policy => policy.RequireClaim("Delete Products")
                    );
                options.AddPolicy("CreateProductsPolicy",
                    policy => policy.RequireClaim("Create Products")
                    );
                options.AddPolicy("EditOrderPolicy",
                    policy => policy.RequireClaim("Edit Order")
                    );
                options.AddPolicy("DeleteOrderPolicy",
                    policy => policy.RequireClaim("Delete Order")
                    );
                options.AddPolicy("EditCategoriesPolicy",
                    policy => policy.RequireClaim("Edit Categories")
                    );
                options.AddPolicy("DeleteCategoriesPolicy",
                    policy => policy.RequireClaim("Delete Categories")
                    );

            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            //KETU LEJOHET THIRJJA E addHTTPCONTEXTACCSEORE PER DEPENDECY INJECTION
            services.AddHttpContextAccessor();
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("ElevatedRights", policy =>
                //policy.RequireRole("Admin", "User"));
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Dashboard}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }

}