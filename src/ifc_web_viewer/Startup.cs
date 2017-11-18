using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ifc_web_viewer.Data;
using ifc_web_viewer.Models;
using ifc_web_viewer.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ifc_web_viewer
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
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //パスワード設定
                //数字必須
                options.Password.RequireDigit = false;
                //最低文字数
                options.Password.RequiredLength = 6;
                //記号必須
                options.Password.RequireNonAlphanumeric = false;
                //大文字必須
                options.Password.RequireUppercase = false;
                //小文字必須
                options.Password.RequireLowercase = false;
                //最低構成の文字種類
                options.Password.RequiredUniqueChars = 1;

                //ロックアウト設定
                //ロックアウト発生時のロックアウト時間
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //ロックアウトまでのログイン失敗可能回数
                options.Lockout.MaxFailedAccessAttempts = 5;
                //新規ユーザーにロックアウトを適用
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".mtl"] = "application/octet-stream";
            provider.Mappings[".obj"] = "application/octet-stream";

            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
