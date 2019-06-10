using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iDokladSync;
using iDokladSync.Bank;
using iDokladSync.Invoices;
using iDokladSync.Shoptet;
using IdokladSdk.ApiModels;
using ImportHebloMVC.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImportHebloMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton(IDokladConnector.Connect(Configuration.GetSection("IDOKLAD_CLIENTID").Value,
                Configuration.GetSection("IDOKLAD_CLIENT_SECRET").Value));
            services.AddSingleton(new FioSdkCsharp.ApiExplorer(Configuration.GetSection("FIO_API_KEY").Value));
            services.AddSingleton<IBankImporter, FioBankImporter>();
            services.AddSingleton<IInvoiceImporter, InvoiceImporter>();
            services.AddSingleton<IInvoiceParser, ShoptetInvoiceParser>();
            services.AddSingleton<IInvoiceSourceFactory, ShoptetInvoiceSourceFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Invoices}/{action=Index}/{id?}");
            });

            AutoMapper.Mapper.Initialize(c =>
            {
                c.CreateMap<IssuedInvoice, IssuedInvoiceUpdate>();
            });
        }
    }
}
