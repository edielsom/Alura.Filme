using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Aula_6
    {

        // RELACIONAMENTO DE MUITOS PARA MUITOS.
        public static void Aula_06()
        {

            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 4.23, Unidade = "Gramas" };


            var promocaoDePessoa = new Promocao();
            promocaoDePessoa.Descricao = "Festival de Férias";
            promocaoDePessoa.DataInicio = DateTime.Now;
            promocaoDePessoa.DataTerminio = DateTime.Now.AddMonths(3);
            promocaoDePessoa.IncluirProduto(p1);
            promocaoDePessoa.IncluirProduto(p2);
            promocaoDePessoa.IncluirProduto(p3);

            using (var contexto = new LojaContext())
            {
                // Procedimento para pegar o log de consulta do Entity Framework.
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                //contexto.Promocoes.Add(promocaoDePessoa);

                var promocao = contexto.Promocoes.Find(1);
                contexto.Promocoes.Remove(promocao);
                Console.WriteLine("***********************************\n\n");
                ExibeEntries(contexto.ChangeTracker.Entries());
                Console.WriteLine("***********************************\n\n");
                contexto.SaveChanges();
                Console.WriteLine("***********************************\n\n");
                ExibeEntries(contexto.ChangeTracker.Entries());


                Console.ReadLine();
            }


        }

       

        // Imprime os logs do Entity Frameword no banco de dados.
        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }

    }
}
