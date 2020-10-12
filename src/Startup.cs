using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VueJwt.Security;

namespace VueJwt
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
      services.AddAuthorization(cfg =>
      {
        var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
        defaultAuthorizationPolicyBuilder =
            defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
        cfg.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
      });

      services.AddAuthentication(cfg => {
        cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(cfg =>
      {
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidIssuer = TokenSecurity.ISSUER,
          ValidAudience = TokenSecurity.AUDIENCE,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSecurity.SIGNING_KEY)),
        };
      });

      services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}
