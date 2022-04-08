using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoneWebShop.Business;
using PhoneWebShop.Business.Diagnostics;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = PhoneWebShop.Domain.Interfaces.ILogger;

namespace PhoneWebShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions()
                .Configure<AppSettings>(
                    options => Configuration.GetSection(nameof(AppSettings).ToLower()).Bind(options))
                .Configure<JwtSettings>(
                    options => Configuration.GetSection(nameof(JwtSettings).ToLower()).Bind(options))
                .Configure<CacheSettings>(
                    options => Configuration.GetSection(nameof(CacheSettings).ToLower()).Bind(options));

            services.AddDbContext<DataContext>(x =>
                x.UseSqlServer(Configuration["AppSettings:ConnectionString"]), ServiceLifetime.Scoped);

            services.AddControllers();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>
                    (TokenOptions.DefaultProvider);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.IncludeErrorDetails = true;
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["jwtSettings:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration["jwtSettings:SignKey"])),
                        ValidAudience = Configuration["jwtSettings:Audience"],
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            services.AddScoped<IPhoneService, PhoneService>()
                .AddScoped<IBrandService, SqlBrandService>()
                .AddScoped<ITimeProvider, TimeProvider>()
                .AddScoped(typeof(IRepository<>), typeof(EFRepository<>))
                .AddScoped<ILogger, FileLogger>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IOrderService, OrderService>()
                .AddSingleton<IMemoryCache, MemoryCache>()
                .AddSingleton<ICaching, MemoryCaching>();

            services.AddProblemDetails();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneWebShop.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddConfiguration(Configuration)
                .SetBasePath(env.ContentRootPath);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneWebShop.Api v1"));
            }

            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Configuration = configBuilder.Build();
        }
    }
}
