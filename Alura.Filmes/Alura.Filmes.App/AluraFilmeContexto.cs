
using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Filmes.App
{
    public class AluraFilmeContexto: DbContext
    {

        public DbSet<Ator> Atores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=PROJETO\SQL2014;Initial Catalog=AluraFilme;User ID=sa;
                                        Password=estadao;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                        ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString);
        }

        // Método que representa o evento de criação do modelo que irá representar o 
        // mapeamento entre o "mundo dos objetos" e o "mundo relacional".
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ator>()
               .ToTable("actor");

            modelBuilder.Entity<Ator>()
              .Property(a => a.Id)
               .HasColumnName("actor_id");

            modelBuilder.Entity<Ator>()
                .Property(a => a.PrimeiroNome)
                .HasColumnName("first_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            modelBuilder.Entity<Ator>()
                .Property(a => a.SegundoNome)
                .HasColumnName("last_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            // A coluna que não existe na classe e existe no banco de dados são chamadas de
            // shadow properties e para configura-las segue os comandos abaixos.
            modelBuilder.Entity<Ator>()
                .Property<DateTime>("last_update") // Nome da Coluna.
                .HasColumnType("datetime")// Tipo do campo.
                .HasDefaultValueSql("getdate()"); // Executa um Sql padrão

        }
    }
}
