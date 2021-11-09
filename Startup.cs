using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notta.Data;
using Notta.Helpers;

namespace Notta
{
  /// <summary>
  /// the startup class 
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// the startup class constructor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    /// <summary>
    /// the configuration property
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// the configuration method for adding services to the project
    /// </summary>
    /// <param name="services"></param>
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      }); 

      // add swagger generator
      services.AddSwaggerGen(options =>
      {

        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Notta App API",
          Description = "A list of all the API-endpoints that makes up the Notta app project",
          Contact = new OpenApiContact
          {
            Name = "Abdullahi Muhammed",
            Email = "muhammedu9ted@yahoo.com",
          },
        });
        var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Notta.xml";
        options.IncludeXmlComments(xmlPath);
      });

      // add the Dbcontext service
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // add Cors
      services.AddCors();

      // add our repository patterns to the services for injection into our controllers
      services.AddScoped<IAuthRepository, AuthRepository>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IPostRepository, PostRepository>();
      services.AddScoped<ICommentRepository, CommentRepository>();

      // Auto-mapper configuration
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new MappingProfile());
      });

      IMapper mapper = mappingConfig.CreateMapper();

      services.AddSingleton(mapper);

      // Authentication by JWT Token
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                  ValidateIssuer = false,
                  ValidateAudience = false
                };
              });
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // adding swagger API to the request pipeline
      app.UseSwagger();

      // adding swagger API to the request pipeline
      app.UseSwaggerUI(options =>
          options.SwaggerEndpoint("/swagger/v1/swagger.json", "Notta API v1")
      );

      // use Cors
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
      app.UseAuthentication();
      app.UseMvc();
      app.Run(context =>
      {
        context.Response.Redirect("/swagger/index.html");
        return Task.CompletedTask;
      });
    }
  }
}
