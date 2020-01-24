using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{

    public class AtorConfiguration : IEntityTypeConfiguration<Ator>
    {
        public void Configure(EntityTypeBuilder<Ator> builder)
        {
            builder
               .ToTable("actor");

            builder
              .Property(a => a.Id)
               .HasColumnName("actor_id");

            builder
                .Property(a => a.PrimeiroNome)
                .HasColumnName("first_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder
                .Property(a => a.SegundoNome)
                .HasColumnName("last_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            // A coluna que não existe na classe e existe no banco de dados são chamadas de
            // shadow properties e para configura-las segue os comandos abaixos.
            builder
                .Property<DateTime>("last_update") // Nome da Coluna.
                .HasColumnType("datetime")// Tipo do campo.
                .HasDefaultValueSql("getdate()"); // Executa um Sql padrão
        }
    }
}
