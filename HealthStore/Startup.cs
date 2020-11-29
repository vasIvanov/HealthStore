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
using HealthStore.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

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
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.Configure<MongoDbConfiguration>(Configuration.GetSection(nameof(MongoDbConfiguration)));

            var mongoSettings = Configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongoSettings.ConnectionString, mongoSettings.DatabaseName)
                .AddSignInManager()
                .AddDefaultTokenProviders();

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
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); 
            services.AddHealthChecks();
            services.AddSwaggerGen(x =>
            {
                var sercurity = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0] }
                };

                OpenApiSecurityScheme secutiryDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Authorization token"
                };

                x.AddSecurityDefinition("jwt", secutiryDefinition);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {secutiryDefinition, new string[] {} }
                });

            });
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
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
