using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PostCardAppV2.Data;
using Westwind.AspNetCore.LiveReload;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using PostCardAppV2.Backend.CronJobs;
using PostCardAppV2.Backend;

namespace PostCardAppV2
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
            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "This field is required.");
            });

            services.AddDbContext<PostCardAppContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PostCardAppContext")));
            services.AddAutoMapper(typeof(Startup));

            services.AddLiveReload();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddSingleton<IEmailService, Email>();
            services.AddCronJob<QuotesRecycleJob>(c =>
            {
                c.CronExpression = @"*/1 * * * *";
                c.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseLiveReload();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Quotes}/{action=Index}/{id?}");
            });
        }
    }
}
