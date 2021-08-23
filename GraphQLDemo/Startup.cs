using App.Data;
using App.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using GraphQL;
using GraphQL.Types;
using App.GraphQL;
using GraphQL.NewtonsoftJson;

namespace GraphQLDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var dbConnectionString = Configuration.GetConnectionString("DemoDBConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
            });

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(dbConnectionString);
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new App.GraphQL.DependencyInjectionInstaller()
            {
                option = optionsBuilder.Options
            });
            containerBuilder.RegisterType<GraphSchema>().As<ISchema>().SingleInstance();
            containerBuilder.RegisterType<DocumentWriter>().As<IDocumentWriter>().SingleInstance();

            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
