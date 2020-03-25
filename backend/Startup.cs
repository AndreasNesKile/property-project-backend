using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string _corsOrigin = "allowSpecificOrigin";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Sets the authentication scheme for the application, JwtBearer is used here.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Authority is the issuer of the token
                // Audience is the intended recipient of the token (this application)

                options.Authority = Configuration.GetSection("AuthApi").GetSection("Domain").Value;
                options.Audience = Configuration.GetSection("AuthApi").GetSection("Identifier").Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Sets the role claim to the specified claim in the token. 
                    // Used to authorized users. 
                    RoleClaimType = "https://property.com/roles"
                };
            });

            // Adds Policy-based authorization.
            // Adds policies requiring the user to have a role claim with a valid value
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireBuyerRole", policy => policy.RequireClaim("https://property.com/roles", "Buyer"));
                options.AddPolicy("RequireAgentRole", policy => policy.RequireClaim("https://property.com/roles", "Agent"));
            });

            services.AddDbContext<PropertyDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddAutoMapper(typeof(PropertyRepository).Assembly, typeof(AccountRepository).Assembly);
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy(_corsOrigin, builder =>
                {
                    /*
                     * Replace line below with 
                     * "builder.WithOrigins('https://example.com', 'https://example2.com')"
                     * if only specific origins are to be allowed
                     */
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature.Error;

                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }));

            }
            app.UseHttpsRedirection();
          
            app.UseRouting();

            app.UseCors(_corsOrigin);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
