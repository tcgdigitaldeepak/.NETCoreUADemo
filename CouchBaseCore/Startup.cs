using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using Swashbuckle;
//using Swashbuckle.Swagger;
//using Swashbuckle.SwaggerUi;

namespace CouchBaseCore
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

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection("JwtToken").GetSection("JwtIssuer").Value,
                        //ValidAudience = Configuration.GetSection("JwtToken").GetSection("JwtIssuer").Value,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JwtToken").GetSection("JwtKey").Value)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            services.AddMvc();

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Info { Title = "Gift Wishlist API", Version = "v1" });
           });

            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri("http://127.0.0.1:8091/") }
            }, new PasswordAuthenticator("Administrator", "971185"));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gigt Wishlist API"));

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                ClusterHelper.Close();
            });
        }
    }
}
