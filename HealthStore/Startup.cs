using AutoMapper;
using FluentValidation.AspNetCore;
using HealthStore.BL.Interfaces.Products;
using HealthStore.BL.Interfaces.Users;
using HealthStore.BL.Services.Products;
using HealthStore.BL.Services.Users;
using HealthStore.DL.Interfaces.Products;
using HealthStore.DL.Interfaces.Users;
using HealthStore.DL.Repositories.Products;
using HealthStore.DL.Repositories.Users;
using HealthStore.Models.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace HealthStore
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
            services.Configure<MongoDbConfiguration>(Configuration.GetSection(nameof(MongoDbConfiguration)));
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

            services.AddSingleton<IPlanService, PlanService>();
            services.AddSingleton<IPlanRepository, PlanRepository>();

            services.AddSingleton<IDietService, DietService>();
            services.AddSingleton<IDietRepository, DietRepository>();

            services.AddSingleton<ISupplementService, SupplementService>();
            services.AddSingleton<ISupplementRepository, SupplementRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton(Log.Logger);
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); 
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthStore API V1");
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
