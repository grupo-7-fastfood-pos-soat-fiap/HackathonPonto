
using System.ComponentModel.DataAnnotations;
using GenericPack.Data;
using GenericPack.Domain;
using GenericPack.Mediator;
using GenericPack.Messaging;
using HackathonPonto.Domain.Models;
using HackathonPonto.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace HackathonPonto.Infra.Data.Context
{
    public sealed class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
      
        public DbSet<Funcionario>? Funcionarios { get; set; }
        public DbSet<Ocupacao>? Ocupacoes { get; set; }
        public DbSet<Ponto>? Pontos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediatorHandler mediatorHandler) :base(options)
        {
             _mediatorHandler = mediatorHandler;

            //https://learn.microsoft.com/en-us/ef/core/querying/tracking
             ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

             //https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.autodetectchangesenabled?view=efcore-7.0
             ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            //https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            //Configura mapeamento
            modelBuilder.ApplyConfiguration(new FuncionariosMap());
            modelBuilder.ApplyConfiguration(new OcupacoesMap());
            modelBuilder.ApplyConfiguration(new PontosMap());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var success = await SaveChangesAsync() > 0;

            return success;
        }

    }

    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
