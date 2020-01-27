using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    public class FilmeAtorConfiguration : IEntityTypeConfiguration<FilmeAtor>
    {
        public void Configure(EntityTypeBuilder<FilmeAtor> builder)
        {
            // Mapeamento da Tabela
            builder.ToTable("film_actor");

            // Mapeamento das chaves primárias compostas.
            builder.Property<int>("film_id");
            builder.Property<int>("actor_id");

            builder.Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getDate()")
                .IsRequired();

            // Criação da chave primária composta
            // As duas colunas só existem no mundo relacional e não no mundo orientado do objeto.
            builder.HasKey("film_id", "actor_id");

            // Cria uma expressão lambda para informar qual é a propriedade que representa a instância do filme.
            builder
                .HasOne(fa => fa.Filme) // Propriedade que representa a instância do filme.
                .WithMany(f => f.Atores) // Propriedade que informa o relacionamento MUITOS.
                .HasForeignKey("film_id"); // Propriedade que informa o nome da chave estrangeira.  

            // Cria uma expressão lambda para informar qual é a propriedade que representa a instância do ator.
            builder
                .HasOne(fa => fa.Ator) // Propriedade que representa a instância do ator.
                .WithMany(f => f.Filmografia) // Propriedade que informa o relacionamento MUITOS.
                .HasForeignKey("actor_id"); // Propriedade que informa o nome da chave estrangeira.
        }
    }
}
