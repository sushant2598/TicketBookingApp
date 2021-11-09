using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace APIGateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder();
            //builder.SetBasePath(environment.ContentRootPath).AddJsonFile("configuration.json", false).AddEnvironmentVariables();
            builder.SetBasePath(environment.ContentRootPath).AddJsonFile("configurationDocker.json", false).AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot(Configuration);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5U5h@n!1998_pr@n5hu"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = "UserWebApi",
                ValidateAudience = true,
                ValidAudience = "MovieTicketWebApi"
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var configuration = new OcelotPipelineConfiguration
            {
                AuthorizationMiddleware = async (ctx, next) =>
                {
                    if (this.Authorize(ctx))
                    {
                        await next.Invoke();
                    }
                    else
                    {
                        ctx.Items.SetError(new UnauthorizedError($"Fail to authorize"));
                    }
                }
            };

            app.UseRouting();

            app.UseOcelot(configuration).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
    {
                await context.Response.WriteAsync("Hello World!");
            });
            });
        }

        private bool Authorize(HttpContext ctx)
        {
            bool authorization = false;
            if (ctx.Items.DownstreamRoute().AuthenticationOptions.AuthenticationProviderKey == null)
            {
                return true;
            }
            else
            {
                Claim[] claims = ctx.User.Claims.ToArray<Claim>();
                Dictionary<string, string> required = ctx.Items.DownstreamRoute().RouteClaimsRequirement;
                string userrole;
                required.TryGetValue("UserRole", out userrole);
                var roles = userrole.Split(",");
                foreach (var role in roles)
                {
                    if (role == claims[1].Value)
                    {
                        authorization = true;
                        break;
                    }
                }
                return authorization;
            }
        }
    }
}
