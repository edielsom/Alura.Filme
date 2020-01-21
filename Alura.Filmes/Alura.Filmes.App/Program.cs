using Alura.Filmes.App.Extensions;
using System;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {

            // Select na tabela actor

            using (var contexto = new AluraFilmeContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var ator in contexto.Atores)
                {
                    Console.WriteLine(ator);



                }
                Console.ReadLine();
            }
        }
    }
}