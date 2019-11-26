using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using STT.WebApi.APIClient.Logic;
using STT.WebApi.APIClient.Models;
using STT.WebApi.Contract.Interfaces;
using STT.WebApi.Contract.Logic;
using STT.WebApi.Data.Logic;
using Swashbuckle.AspNetCore.Swagger;

namespace SST.WebApi.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", info: new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "agustin.nanni97@gmail.com",
                        Name = "Agustín Nanni"
                    },
                    Version = "v1",
                    Title = "Santex Football Api"
                }); 
            });
            services.AddSingleton<IContractUOW>(new ContractUOW(new FootBallApiWebClient(
                new WebApiConfiguration()
                {
                    Token = Configuration["ApiToken"],
                    URL = Configuration["ApiURL"]
                }),
                new FootballUOW(new FootballDBContext(
                    DBContextOptionsBuilder.GetOptions(Configuration["MySqlConn"])
                    )))
                );
            services.AddCors(c =>
            {
                c.AddPolicy("AllowMyOrigin",
                builder => builder.WithOrigins("*"));
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

            app.UseCors("AllowMyOrigin");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", name: "Santex Football API");
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
