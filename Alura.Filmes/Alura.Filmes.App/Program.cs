using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;


namespace Alura.Filmes.App
{
    class Program
    {

        static void Main(string[] args)
        {
            /* MÉTODOS DO CURSO PARTE I
             
                LerPropriedadeShadowProperty();
                ListarDezAtoresModificados();
                LendoFilmeCadastrado();
                ImprimidoDadosTabelasRelacionamento();
                RelacionamentoMuitosParaMuitos();
                IdiomasFalados();
                LendoIdiomaFilme();
            */

            /* MÉTODOS DO CURSO PARTE II 
                CriandoChaveUnique();
                 TrabalhandoComEnumerador();
                HerancaEntity();
                UsandoSqlManualmente();
                UsandoStoredProcedures();

             */

            // Executar comandos de insert, delete e update.
            ExecutarStoredProceduresInsertDeleteUpdate();

        }
        private static void ExecutarStoredProceduresInsertDeleteUpdate()
        {
            // Executar comandos de insert, delete e update.
            using (var contexto = new AluraFilmeContexto())
            {

                var sql = "INSERT INTO language (name) VALUES ('Teste 1'), ('Teste 2'), ('Teste 3')";
                var registros = contexto.Database.ExecuteSqlCommand(sql);
                System.Console.WriteLine($"O total de registros afetados é {registros}.");

                var deleteSql = "DELETE FROM language WHERE name LIKE 'Teste%'";
                registros = contexto.Database.ExecuteSqlCommand(deleteSql);
                System.Console.WriteLine($"O total de registros afetados é {registros}.");

            }
            Console.ReadKey();
        }

        private static void UsandoStoredProcedures()
        {
            using (var contexto = new AluraFilmeContexto())
            {

                contexto.LogSQLToConsole();

                // Executando Stored Procedure

                var categ = "Action"; //36

                var paramCateg = new SqlParameter("category_name", categ);

                var paramTotal = new SqlParameter
                {
                    ParameterName = "@total_actors",
                    Size = 4,
                    Direction = System.Data.ParameterDirection.Output
                };

                contexto.Database
                    .ExecuteSqlCommand("total_actors_from_given_category @category_name, @total_actors OUT", paramCateg, paramTotal);

                System.Console.WriteLine($"O total de atores na categoria {categ} é de {paramTotal.Value}.");


            }
            Console.ReadKey();
        }

        private static void UsandoSqlManualmente()
        {
            using (var contexto = new AluraFilmeContexto())
            {

                contexto.LogSQLToConsole();

                // Consulta via Entity Framework
                /* var atoresMaisAtuantes = contexto.Atores
                                            .Include( a => a.Filmografia)
                                            .OrderByDescending(a => a.Filmografia.Count())
                                            .Take(5);
                */


                // Assumindo o controle sql
                var sql = @"select a.* from actor a
                            inner join top5_most_starred_actors filmes on filmes.actor_id = a.actor_id";

                var atoresMaisAtuantes = contexto.Atores
                                      .FromSql(sql);

                Console.WriteLine("Atores.: ");
                foreach (var ator in atoresMaisAtuantes)
                {
                    Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.SegundoNome} atuou em {ator.Filmografia.Count} filmes");

                }


            }
            Console.ReadKey();
        }

        private static void HerancaEntity()
        {
            using (var contexto = new AluraFilmeContexto())
            {

                contexto.LogSQLToConsole();

                Console.WriteLine("Cliente.: ");
                foreach (var cliente in contexto.Clientes)
                {
                    Console.WriteLine(cliente);
                }

                Console.WriteLine("Funcionário.: ");
                foreach (var funcionario in contexto.Funcionarios)
                {
                    Console.WriteLine(funcionario);
                }
            }
            Console.ReadLine();
        }

        public static void TrabalhandoComEnumerador()
        {
            using (var contexto = new AluraFilmeContexto())
            {

                contexto.LogSQLToConsole();

                var filme = new Filme
                {
                    Titulo = "Senhor dos Anéis",
                    Duracao = 120,
                    AnoLancamento = "2000",
                    Classificacao = ClassificacaoIndicativa.MaioresQue14,
                    IdiomaFalado = contexto.Idiomas.First()
                };

                contexto.Filmes.Add(filme);
                contexto.SaveChanges();

                var filmeInserido = contexto.Filmes.First(f => f.Titulo == "Senhor dos Anéis");
                Console.WriteLine(filmeInserido.Classificacao);
            }
            Console.ReadLine();
        }

        public static void CriandoChaveUnique()
        {
            using (var contexto = new AluraFilmeContexto())
            {

                contexto.LogSQLToConsole();

                var ator1 = new Ator { PrimeiroNome = "Emma", SegundoNome = "Watson" };
                var ator2 = new Ator { PrimeiroNome = "Emma", SegundoNome = "Watson" };
                contexto.Atores.AddRange(ator1, ator2);
                contexto.SaveChanges();

                var emmaWatson = contexto.Atores
                    .Where(a => a.PrimeiroNome == "Emma" && a.SegundoNome == "Watson");

                Console.WriteLine($"Total de atores encontrados: {emmaWatson.Count()}.");

            }
            Console.ReadLine();
        }

        #region EXERCÍCIOS DO CURSO DE ENTITY FRAMEWORK PARTE I 
        public static void LendoIdiomaFilme()
        {
            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();

                var idiomas = contexto.Idiomas
                    .Include(i => i.FilmesFalados);

                foreach (var idioma in idiomas)
                {
                    Console.WriteLine(idioma);

                    foreach (var filme in idioma.FilmesFalados)
                    {
                        Console.WriteLine(filme);
                    }
                    Console.WriteLine("\n");
                }
                Console.ReadLine();
            }
        }

        public static void IdiomasFalados()
        {
            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();


                foreach (var idioma in contexto.Idiomas)
                {
                    Console.WriteLine(idioma);
                }

                Console.ReadLine();
            }
        }

        public static void RelacionamentoMuitosParaMuitos()
        {
            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto.Filmes
                    .Include(f => f.Atores)  // Faz um join com a tabela Atores
                    .ThenInclude(fa => fa.Ator) // Faz um join em nível mais baixo com a tabela filme
                    .First();

                Console.WriteLine(filme);
                Console.WriteLine("Elenco");

                foreach (var ator in filme.Atores)
                {
                    Console.WriteLine(ator.Ator);
                }
                Console.ReadLine();
            }
        }

        public static void ImprimidoDadosTabelasRelacionamento()
        {
            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();



                foreach (var item in contexto.Elenco)
                {
                    var entidade = contexto.Entry(item);
                    var filmId = entidade.Property("film_id").CurrentValue; // Mapea uma ShadowProperty
                    var actorId = entidade.Property("actor_id").CurrentValue;// Mapea uma ShadowProperty
                    var lastUpd = entidade.Property("last_update").CurrentValue;// Mapea uma ShadowProperty

                    Console.WriteLine($"Filme {filmId}, Ator {actorId}, LastUpdate: {lastUpd}");
                }
                Console.ReadLine();
            }
        }

        public static void LendoFilmeCadastrado()
        {
            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();

                var filmes = contexto.Filmes;

                foreach (var filme in filmes)
                {
                    Console.WriteLine(filme);
                }
                Console.ReadLine();
            }
        }

        public static void ListarDezAtoresModificados()
        {
            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();

                // Listar os 10 atores modificados recentementes
                var atores = contexto.Atores       // "EF" é uma instância estática do entity framework, onde pode-se usar vários métodos estáticos.
                            .OrderByDescending(ator => EF.Property<DateTime>(ator, "last_update"))
                            .Take(10);

                foreach (var ator in atores)
                {
                    Console.WriteLine(ator + " - " +
                            contexto.Entry(ator).Property("last_update").CurrentValue);

                }

                Console.ReadLine();

            }
        }

        public static void LerPropriedadeShadowProperty()
        {
            // Select na tabela actor

            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();

                var ator = new Ator()
                {
                    PrimeiroNome = "Tom",
                    SegundoNome = "Hanks"
                };



                // Informar uma coluna que existe no banco de dados mas não está declarada na classe
                // Shadow Property
                //contexto.Entry(ator).Property("last_update").CurrentValue = DateTime.Now;

                contexto.Atores.Add(ator);

                contexto.SaveChanges();

                Console.ReadLine();
            }
        }
        #endregion

    }
}
