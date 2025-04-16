using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Context
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options) : base(options)
        {}

        public DbSet<Tarefa> Tarefas {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var convert = new EnumToStringConverter<StatusTarefa>();
            modelBuilder.Entity<Tarefa>().Property(t => t.Status).HasConversion(convert);
        }
    }
}