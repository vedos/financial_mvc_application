using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.DbCtx;
using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Models;
using System.Collections.Generic;

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
                FeePercent = 0.3m  
            };

            var partner2 = new Partner
            {
                Name = "Partner2",
                FeePercent = 0.2m
            };

            var partner3 = new Partner
            {
                Name = "Partner3",
                FeePercent = 0.3m
            };

            var partner4 = new Partner
            {
                Name = "Partner4",
                FeePercent = 0.2m
            };

            var partner5 = new Partner
            {
                Name = "Partner5",
                FeePercent = 0.5m
            };

            var partner6 = new Partner
            {
                Name = "Partner6",
                FeePercent = 0.3m
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
                FeePercent = 0.25m
            };

            var partner10 = new Partner
            {
                Name = "Partner10",
                FeePercent = 0.5m
            };

            var random = new Random();
            var list = new List<Partner>() {partner1,partner2,partner3,partner4,partner5, partner6, partner7, partner8, partner9, partner10 };

            var randomList = new List<Partner>();
            foreach (Partner item in list) 
            {
                int index = random.Next(list.Count);

                item.PartnerParent = list[index];

                if (isUsed(item, list[index]))
                    item.PartnerParent = list[index];
                else
                    item.PartnerParent = null;

                randomList.Add(item);
            }


            context.Partners.AddRange(randomList);

            context.SaveChanges();
        }

        public static bool isUsed(Partner partner, Partner newParent) 
        {
            if (newParent == null) 
                return true;

            if (partner == newParent)
                return false;

            

            return isUsed(partner, newParent.PartnerParent);
        }
    }
}
