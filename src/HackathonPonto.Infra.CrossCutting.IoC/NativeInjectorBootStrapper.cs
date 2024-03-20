using Microsoft.Extensions.DependencyInjection;
using GenericPack.Mediator;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using HackathonPonto.Infra.Data.Context;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.Services;
using HackathonPonto.Infra.CrossCutting.Bus;
using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Infra.Data.Repository;
using HackathonPonto.Application.AutoMapper;
using HackathonPonto.Domain.Commands.FuncionarioCommands;
using MediatR;
using GenericPack.Messaging;

namespace HackathonPonto.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Setting DBContexts
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Connectionstring")));

            services.AddScoped<AppDbContext>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddScoped<IDbConnection, Npgsql.NpgsqlConnection>();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application            
            services.AddScoped<IFuncionarioApp, FuncionarioApp>();            


            // Infra - Data           
            services.AddScoped<IOcupacaoRepository, OcupacaoRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();


            // AutoMapper Settings
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(InputModelToDomainMappingProfile));

            // Domain - Commands
            services.AddScoped<IRequestHandler<FuncionarioCreateCommand, CommandResult>, FuncionarioCommandHandler>();
            services.AddScoped<IRequestHandler<FuncionarioUpdateCommand, CommandResult>, FuncionarioCommandHandler>();
            services.AddScoped<IRequestHandler<FuncionarioDeleteCommand, CommandResult>, FuncionarioCommandHandler>();            


            // Domain - Events
            //services.AddScoped<INotificationHandler<AndamentoCreateEvent>, AndamentoEventHandler>();

            //Infra - Services
            //services.AddScoped<IProxyProducao, ProducaoService>();

            //Gateway de Pagamento
            //services.AddHttpClient<IProxyProducao, ProducaoService>(
            //client =>
            //{
            //    // Set the base address of the named client.
            //    //var host = configuration.GetSection("ProxyProducao").Value;

            //    client.BaseAddress = new Uri("https://localhost:7035/");
            //});
        }
    }
}
