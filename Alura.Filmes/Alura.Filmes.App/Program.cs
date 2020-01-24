using Alura.Filmes.App.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // LerPropriedadeShadowProperty();
            //ListarDezAtoresModificados();

            using (var conteto = new AluraFilmeContexto())
            {
                conteto.LogSQLToConsole();

                var filmes = conteto.Filmes;

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