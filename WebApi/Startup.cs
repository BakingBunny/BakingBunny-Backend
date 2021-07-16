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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repository;
using WebApi.Models;
using System.Configuration;
using WebApi.Settings;
using WebApi.Services;

namespace WebApi
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
            services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IMailService, MailService>();
            services.AddTransient<IMailService, MailService>();
            services.AddDbContext<BakingbunnyContext>();
            string dfd = Configuration.GetConnectionString("localBakingBunnyDB");
#if (DEBUG)
            services.AddDbContext<BakingbunnyContext>(options =>
                //options.UseMySql(Configuration.GetConnectionString("localBakingBunnyDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql")));
                options.UseMySql(Configuration.GetConnectionString("localBakingBunnyDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql")));
#else
            services.AddDbContext<BakingbunnyContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("prodBakingBunnyDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql")));
#endif
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Baking Bunny API", Version = "v1" });
                //c.ResolveConflictingActions(s => s.First());
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                configBuilder.AddUserSecrets<UserSecretConfig>();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Baking Bunny API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
