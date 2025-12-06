using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MidasBlazor.Models;

namespace MidasBlazor.Data
{
    // Essa classe é o "coração" do EF(migração) Core:, pq conecta as Models ao banco
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Cada DbSet vira uma tabela no banco
        public DbSet<ProjetoViewModel> Projecoes { get; set; } = null!;

        // É aqui o OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define nomes de tabelas para serem alimentadas e chamadas. Também defini o relacionamentos e pk e fk
            modelBuilder.Entity<ProjetoViewModel>().ToTable("Projecoes");
            modelBuilder.Entity<ProjetoViewModel>().HasKey(p => p.IdProjecao);

            

            // decimal
            modelBuilder.Entity<ProjetoViewModel>()
                .Property(p => p.ValorPrevisto)
                .HasColumnType("decimal(18,2)");


            base.OnModelCreating(modelBuilder);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings
                .Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
