using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyAPIOData.API.Models;

namespace UdemyAPIOData.API
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConStr"]);
            });
            services.AddOData();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Bu builder ?zeriden odata ya hangi entityler ?zerinde sorgulama yapaca??n? g?nderiyoruz.
            var builder = new ODataConventionModelBuilder();

            //CategoriesController
            //[entity set Name] Controller

            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Product>("Products");

            /*ACT?ONS*/

            //../odata/categories(1)/totalproductprice
            builder.EntityType<Category>().Action
                ("TotalProductPrice").Returns<int>();
            //../odata/categories/totalproductprice
            builder.EntityType<Category>().Collection.Action
                ("TotalProductPrice2").Returns<int>();

            //odata/categories/totalproductprice

            builder.EntityType<Category>().Collection.Action
                ("TotalProductPriceWithParameter").Returns<int>
                ().Parameter<int>("categoryId");

            var actionTotal = builder.EntityType<Category>().Collection
                .Action("Total").Returns<int>();
            actionTotal.Parameter<int>("a");
            actionTotal.Parameter<int>("b");
            actionTotal.Parameter<int>("c");


            builder.EntityType<Product>().Collection.Action("LoginUser")
                .Returns<string>().Parameter<Login>("UserLogin");

            /*FUNCTIONS*/

            builder.EntityType<Category>().Collection.Function("CategoryCount").Returns<int>();

            //parametre alan
            var MultiplyFuntion = builder.EntityType<Product>()
                .Collection.Function("MultiplyFunction").Returns<int>();
            MultiplyFuntion.Parameter<int>("a1");
            MultiplyFuntion.Parameter<int>("a2");
            MultiplyFuntion.Parameter<int>("a3");


            builder.EntityType<Product>().Function("KdvHesapla").Returns<double>().Parameter<double>("KDV");

            builder.Function("GetKdv").Returns<int>();         
            
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            

            app.UseEndpoints(endpoints =>
            {
             
                endpoints.Select().Expand().OrderBy().MaxTop(null).Count().Filter(); // query option
                endpoints.MapODataRoute
                ("odata", "odata", builder.GetEdmModel());
                endpoints.MapControllers();
            });
        }
    }
}
