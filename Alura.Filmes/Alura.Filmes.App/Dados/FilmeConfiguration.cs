using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder
                .ToTable("film");

            builder
                .Property(f => f.Id)
                .HasColumnName("film_id");

            builder
                .Property(f => f.Titulo)
                .HasColumnName("title")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .Property(f => f.Descricao)
                .HasColumnName("description")
                .HasColumnType("text");

            builder
                .Property(f => f.Duracao)
                .HasColumnName("length")
                .HasColumnType("smallint")
                .IsRequired();

            builder
                .Property(f => f.AnoLancamento)
                .HasColumnName("release_year")
                .HasColumnType("varchar(4)");

            // A coluna que não existe na classe e existe no banco de dados são chamadas de
            // shadow properties e para configura-las segue os comandos abaixos.

            builder
                .Property<string>("rating")
                .HasColumnType("varchar(10)");

            builder
                .Property<DateTime>("last_update")// Nome da Coluna.
                .HasColumnType("datetime")// Tipo do campo.
                .HasDefaultValueSql("getdate()"); // Executa um Sql padrão
        }
    }
}
