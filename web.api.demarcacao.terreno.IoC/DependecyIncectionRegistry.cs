using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using patterns.strategy;
using System;
using web.api.demarcacao.terreno.CrossCutting.Core;
using web.api.demarcacao.terreno.Data;
using web.api.demarcacao.terreno.Data.Context;
using web.api.demarcacao.terreno.Data.Repository;
using web.api.demarcacao.terreno.Data.Repository.Common;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository.Common;
using web.api.demarcacao.terreno.Service.Application.Strategy;
using web.api.demarcacao.terreno.Service.Application.Strategy.Request;
using web.api.demarcacao.terreno.Service.Application.Strategy.Terreno;
using web.api.demarcacao.terreno.Service.Application.Strategy.Terreno.Request;

namespace web.api.demarcacao.terreno.IoC
{
    public static class DependecyIncectionRegistry
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterItensSetup();
            services.RegisterItensDataBase(configuration);
            services.RegisterStrategies();
            return services;
        }

        private static void RegisterItensSetup(this IServiceCollection services)
        {
            services.AddSingleton<IProxyGenerator, ProxyGenerator>();
            services.AddScoped<IAsyncValidatorInterceptor, ValidatorInterceptor>();
        }

        private static void RegisterStrategies(this IServiceCollection services)
        {
            services.AddScoppedStrategy<IStrategy<CadastraEmpreendimentoRequest, DefaultResponse>, CadastraEmpreendimentoStrategy>();
            services.AddScoppedStrategy<IStrategy<AtualizaEmpreendimentoRequest, DefaultResponse>, AtualizaEmpreendimentoStrategy>();
            services.AddScoppedStrategy<IStrategy<RetornaEmpreendimentoQuery, RetornarEmpreendimentoQueryResponse>, RetornaEmpreendimentoStrategy>();

            services.AddScoppedStrategy<IStrategy<CadastraTerrenoRequest, CadastraTerrenoResponse>, CadastraTerrenoStrategy>();
            services.AddScoppedStrategy<IStrategy<RetornaTerrenoQuery, RetornaTerrenoQueryResponse>, RetornaTerrenoStrategy>();
            services.AddScoppedStrategy<IStrategy<AtualizaTerrenoRequest, DefaultResponse>, AtualizaTerrenoStrategy>();
            services.AddScoppedStrategy<IStrategy<ExcluiTerrenoRequest, DefaultResponse>, ExcluiTerrenoStrategy>();

            services.AddScoped<IStrategy<AuthUserQuery, AuthUserQueryResponse>, AuthUserStrategy>();
        }

        private static void RegisterItensDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IDemarcacaoPostgressContext, DemarcacaoPostgressContext>(options =>
            {
                var connectionString = $"Server={Environment.GetEnvironmentVariable("hostDd")};" +
                                       $"Port={Environment.GetEnvironmentVariable("portDb")};" +
                                       $"User Id={Environment.GetEnvironmentVariable("userNameDb")};" +
                                       $"Password={Environment.GetEnvironmentVariable("passwordDb")};" +
                                       $"Database={Environment.GetEnvironmentVariable("databaseNameDb")};" +
                                       $"SSL Mode=Prefer;Trust Server Certificate=true";
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IDemarcacaoUnitOfWork, DemarcacaoUnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IEmpreendimentoRepository, EmpreendimentoRepository>();
            services.AddScoped<ITerrenoRepository, TerrenoRepository>();
            services.AddScoped<ICoordenadaRepository, CoordenadaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
