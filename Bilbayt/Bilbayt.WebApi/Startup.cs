using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bilbayt.Model.Settings;
using Bilbayt.WebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      services
        .AddCustomMVC(Configuration)
        .EnableJWTAuthentication(Configuration)
        .AddAppSettingValues(Configuration)
        .AddSwagger();

      var container = new ContainerBuilder();
      container.Populate(services);
      container.RegisterModule(new ApplicationModule());

      return new AutofacServiceProvider(container.Build());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bilbayt.WebApi v1"));

      app.UseHttpsRedirection();

      app.UseCors("CorsPolicy");
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
      });
    }
  }

  public static class CustomExtensionMethods
  {
    public static object JwtBearerDefaults { get; private set; }

    public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddMvc(options =>
      {
        options.Filters.Add(typeof(HttpGlobalExceptionFilter));
      }).AddControllersAsServices();

      var allowedOrigins = new List<string>();
      var allowOrigins = configuration["AllowedOrigins"].Split(",");

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
                  builder => builder.WithOrigins(allowOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
      });

      return services;
    }

    public static IServiceCollection AddAppSettingValues(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<BilbaytDatabaseSettings>
          (configuration.GetSection(nameof(BilbaytDatabaseSettings)));

      services.Configure<TokenSettings>
          (configuration.GetSection(nameof(TokenSettings)));

      services.AddSingleton<IBilbaytDatabaseSettings>(sp =>
          sp.GetRequiredService<IOptions<BilbaytDatabaseSettings>>().Value);

      services.AddSingleton<ITokenSettings>(sp =>
        sp.GetRequiredService<IOptions<TokenSettings>>().Value);

      return services;
    }

    public static IServiceCollection EnableJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["TokenSettings:Issuer"],
            ValidAudiences = new List<string>
              {
                          "angularApp"
              },

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenSettings:Key"])),
            ClockSkew = TimeSpan.Zero
          };
        });

      return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(options =>
      {
        //options.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Bilbayt - Web API",
          Version = "v1",
          Description = "The web service for Bilbayt Test",
          TermsOfService = new Uri("https://example.com/terms")
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
          Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
      });

      return services;

    }

  }
}
