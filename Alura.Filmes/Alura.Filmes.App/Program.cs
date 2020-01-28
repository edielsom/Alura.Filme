using Alura.Filmes.App.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Alura.Filmes.App.Negocio;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // LerPropriedadeShadowProperty();
            //ListarDezAtoresModificados();
            //LendoFilmeCadastrado();
            //ImprimidoDadosTabelasRelacionamento();
            //RelacionamentoMuitosParaMuitos();

            // IdiomasFalados();

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

        private static void IdiomasFalados()
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

        private static void RelacionamentoMuitosParaMuitos()
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

        private static void ImprimidoDadosTabelasRelacionamento()
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

        private static void LendoFilmeCadastrado()
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

        private static void ListarDezAtoresModificados()
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

        private static void LerPropriedadeShadowProperty()
        {
            // Select na tabela actor

            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();

                var ator = new Ator();
                ator.PrimeiroNome = "Tom";
                ator.SegundoNome = "Hanks";


                // Informar uma coluna que existe no banco de dados mas não está declarada na classe
                // Shadow Property
                //contexto.Entry(ator).Property("last_update").CurrentValue = DateTime.Now;

                contexto.Atores.Add(ator);

                contexto.SaveChanges();

                Console.ReadLine();
            }
        }
    }
}