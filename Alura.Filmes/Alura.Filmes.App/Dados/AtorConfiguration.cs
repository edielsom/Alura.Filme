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

            /*  A coluna que não existe na classe e existe no banco de dados são chamadas de
               shadow properties e para configura-las segue os comandos abaixos.
            */

            builder
                .Property<DateTime>("last_update")  // Nome da Coluna.
                .HasColumnType("datetime")          // Tipo do campo.
                .HasDefaultValueSql("getdate()");   // Executa um Sql padrão

            /* CRIANDO ÍNDICE NA COLUNAL SEGUNDO NOME. */
            builder
                .HasIndex(i => i.SegundoNome)       // Cria o índice na coluna
                .HasName("idx_actor_last_name");    // Informa o nome do índice.


            /*  Por convenção se houver uma chave estrangeira, também será criado um índice.*/

            /*  Outra convenção é no nome conferido ao índice, ele será sempre formado seguindo o seguinte padrão: 
               prefixo "IX", underscore (_), nome do tipo da classe dependente, 
               e nome da coluna onde ele será colocado.
            */
            /* O índice não pode ser criado por anotação, somente pelo modo fluent. */

            /* CRIANDO "UNIQUE CONSTRAINT */
            builder
                .HasAlternateKey(a => new { a.PrimeiroNome, a.SegundoNome }); // Cria Unique nas colunas primeiro e segundo nome.

            /* No Entity Framework, a restrição Unique é chamada de Alternate Keys.
               Como se fosse a chame primiária, só que alternativa.
               Ao criar uma Alternate Key, ela será nomeada respeitando o sufixo AK, 
               seguido do nome do tipo e, em seguida, o nome de todas as propriedades 
               utilizadas naquela restrição, com o underscore (_).
             */

        }
    }
}
