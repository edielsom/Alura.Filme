
using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Filmes.App
{
    public class AluraFilmeContexto: DbContext
    {

        public DbSet<Ator> Atores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
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

            // Importar as configurações da classe ator.
            modelBuilder.ApplyConfiguration(new AtorConfiguration());

            // Importar as configurações da classe filme.
            modelBuilder.ApplyConfiguration(new FilmeConfiguration());

        }
    }
}
