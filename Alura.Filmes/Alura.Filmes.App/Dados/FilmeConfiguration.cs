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
                .Property(f => f.Classificacao)
                .HasColumnName("rating")
                .HasColumnType("varchar(10)");

            builder
                .Property<DateTime>("last_update")  // Nome da Coluna.
                .HasColumnType("datetime")          // Tipo do campo.
                .HasDefaultValueSql("getdate()");   // Executa um Sql padrão

            // Cria as chaves estrangeiras.
            builder.Property<int>("language_id");
            builder.Property<int?>("original_language_id");//// "?" É chamado de "nullable types"

            builder
                .HasOne(f => f.IdiomaFalado)    // Um filme possui um idioma.
                .WithMany(i => i.FilmesFalados) // Um mesmo idioma é falando em vários filmes.
                .HasForeignKey("language_id");  // Chave estrangeira representada pela shadow property "language_id".

            builder
                .HasOne(f => f.IdiomaOriginal)          // Um filme possui um idioma.
                .WithMany(i => i.FilmesOriginais)       // Um mesmo idioma é falando em vários filmes.
                .HasForeignKey("original_language_id"); // Chave estrangeira representada pela shadow property "language_id".
        }
    }
}
