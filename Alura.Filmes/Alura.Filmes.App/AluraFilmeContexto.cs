using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Filmes.App
{
    public class AluraFilmeContexto : DbContext
    {

        public DbSet<Ator> Atores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<FilmeAtor> Elenco { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        //public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             string banco = "AluraFilme";
           //string banco = "AluraFilmeTST";
            string connectionString = $@"Data Source=PROJETO\SQL2014;Initial Catalog={banco};User ID=sa;
                                        Password=estadao;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                        ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString);
        }

        // Método que representa o evento de criação do modelo que irá representar o 
        // mapeamento entre o "mundo dos objetos" e o "mundo relacional".
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Importar as configurações da classe Ator.
            modelBuilder.ApplyConfiguration(new AtorConfiguration());

            // Importar as configurações da classe Filme.
            modelBuilder.ApplyConfiguration(new FilmeConfiguration());

            // Importar as configurações da classe FilmeAtor.
            modelBuilder.ApplyConfiguration(new FilmeAtorConfiguration());

            // Importar as configurações da classe Idiomas.
            modelBuilder.ApplyConfiguration(new IdiomaConfiguration());

            // Importar as configurações da classe Pessoas.
            //modelBuilder.ApplyConfiguration(new PessoaConfiguration());

            // Importar as configurações da classe Cliente.
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());

            // Importar as configurações da classe Funcionário.
            modelBuilder.ApplyConfiguration(new FuncionarioConfiguration());

        }
    }
}
