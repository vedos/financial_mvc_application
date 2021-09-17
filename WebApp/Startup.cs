using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.DbCtx;
using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Models;

namespace WebApp
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
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("FinancialDatabase"));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            var context = serviceProvider.GetService<DataContext>();
            AddTestData(context);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void AddTestData(DataContext context)
        {
            //Add Test Data

            var partner1 = new Partner
            {
                Name = "Partner1",
                FeePercent = 0.1m  
            };

            var partner2 = new Partner
            {
                Name = "Partner2",
                FeePercent = 0.2m,
                PartnerParentId = 1
            };

            var partner3 = new Partner
            {
                Name = "Partner3",
                FeePercent = 0.3m,
                PartnerParentId = 2
            };

            var partner4 = new Partner
            {
                Name = "Partner4",
                FeePercent = 0.4m
            };

            var partner5 = new Partner
            {
                Name = "Partner5",
                FeePercent = 0.5m,
                PartnerParentId = 2
            };

            var partner6 = new Partner
            {
                Name = "Partner6",
                FeePercent = 0.3m,
                PartnerParentId = 5
            };

            var partner7 = new Partner
            {
                Name = "Partner7",
                FeePercent = 0.1m
            };

            var partner8 = new Partner
            {
                Name = "Partner8",
                FeePercent = 0.3m
            };

            var partner9 = new Partner
            {
                Name = "Partner9",
                FeePercent = 0.25m,
                PartnerParentId = 8
            };

            var partner10 = new Partner
            {
                Name = "Partner10",
                FeePercent = 0.5m
            };

            context.Partners.Add(partner1);
            context.Partners.Add(partner2);
            context.Partners.Add(partner3);
            context.Partners.Add(partner4);
            context.Partners.Add(partner5);
            context.Partners.Add(partner6);
            context.Partners.Add(partner7);
            context.Partners.Add(partner8);
            context.Partners.Add(partner9);
            context.Partners.Add(partner10);

            context.FinancialItems.Add(new FinancialItem {
                Amount = 0,
                Date = DateTime.Now,
                Partner = partner1,
                PartnerId = partner1.Id
            });

            context.SaveChanges();
        }
    }
}
