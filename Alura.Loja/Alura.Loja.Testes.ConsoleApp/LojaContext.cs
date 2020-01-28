using Alura.Loja.Testes.ConsoleApp.Migrations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class LojaContext : DbContext
    {

        // Propriedade que cria vinculo com banco de dados.
        // DbSet<Produto> --> Nome classe mapeada 
        // Produtos --> Nome da tabela no banco de dados.
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public LojaContext()
        {

        }
        public LojaContext(DbContextOptions<LojaContext> options):base(options)
        {

        }
        // Método para efetuar a configuração do modelo.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string Conexao = @"Data Source=PROJETO\SQL2014;Initial Catalog=LojaDbCurso;User ID=sa;Password=estadao;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // Verifica se o banco de já está configurado.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Conexao);
            }
        }

        /*  Método é executado no evento de criação do modelo. 
            Podemos configurar informado que a entidade PromocaoProduto tem a chave composta, 
            com a composição de ProdutoId e PromocaoId. A configuração ficará da seguinte maneira.
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Chave primária composta.
            modelBuilder
                .Entity<PromocaoProduto>()
                .HasKey(pp => new { pp.ProdutoId, pp.PromocaoId });


            modelBuilder
           .Entity<Endereco>()
           .ToTable("Enderecos");// Define o nome da tabela

            modelBuilder
                .Entity<Endereco>()
                .Property<int>("ClienteId"); // Cria uma propriedade compartilhada se definição na classe.

            modelBuilder
                .Entity<Endereco>()
                .HasKey("ClienteId");
        }

    }
}