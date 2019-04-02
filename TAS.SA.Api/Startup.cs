using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TAS.SA.Dominio;
using TAS.SA.Infra.EntityFramework.Config;
using TAS.SA.Infra.EntityFramework.Repositorio;

namespace TAS.SA.Api
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
            services.AddDbContext<ServicoAnuncioContexto>(opcao => opcao.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IAnuncioRepositorio, AnuncioRepositorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            AdiicionarDados(app);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void AdiicionarDados(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ServicoAnuncioContexto>();

                var listaDeAnuncios = new List<Anuncio>
                {
                    new Anuncio("Projeto A", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                    new Anuncio("Projeto B", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                    new Anuncio("Projeto C", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                    new Anuncio("Projeto D", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                    new Anuncio("Projeto E", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                    new Anuncio("Projeto F", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                };

                context.Anuncios.AddRange(listaDeAnuncios);
                context.SaveChanges();
            }
        }
    }
}
