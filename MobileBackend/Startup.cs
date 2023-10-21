using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MobileBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobileBackend
{
    public class CheckToken
    {
        private readonly RequestDelegate _next;
        public CheckToken(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Session.GetString("Token");
            var header = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token) && (context.Request.Path.Value != "/Home/Login" || !context.Request.Path.Value.Contains("AddDocumentApp")) && string.IsNullOrEmpty(header))
            {
                context.Response.Redirect("/Home/Login");
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            await _next(context);
        }
    }
    public static class TestMiddlewareExtensions
    {
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckToken>();
        }
    }
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
                options.UseNpgsql(
                    Configuration.GetConnectionString("ConnStr")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();
            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })


            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    ,ValidateAudience = false
                    ,ValidateIssuer = false
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSession( options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddSingleton<UniqueCode>();
            services.AddSingleton<CustomIDataProtection>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseHttpsRedirection();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
           // app.UseTestMiddleware();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                else
                {
                    var t = context.Request.Query["token"].ToString();
                    if (!string.IsNullOrEmpty(t))
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + t);
                    }
                    else {
                        var header = context.Request.Headers["Authorization"];
                        //if (string.IsNullOrEmpty(header)) {
                        //    if (!context.Request.Path.Value.Contains("Login"))
                        //    {
                        //        context.Response.Redirect("/Home/Login");
                        //    }
                        //}
                       
                         
                        
                    }
                }
                await next();
            });
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
           

            app.UseStatusCodePages(async context => {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                // you may also check requests path to do this only for specific methods       
                // && request.Path.Value.StartsWith("/specificPath")

                {
                    response.Redirect("/Home/Login");
                }
            });
            //RotativaConfiguration.Setup("/wwwroot","rotativa");

          

        }
    }
}
