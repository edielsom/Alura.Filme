using Microsoft.EntityFrameworkCore;
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
    public class Aula_8
    {

        public static void JoinRelacionamentoUmParaUm()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());


                var cliente = contexto
                                .Clientes
                                .Include(c => c.EnderecoDeEntrega)// Relacionamento com a tabela enderço
                                .FirstOrDefault();


                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoDeEntrega.Logradouro}");

                var produto = contexto
                                .Produtos
                                .Include(p => p.Compras)
                                .Where(p => p.Id == 16)
                                .FirstOrDefault();

                Console.WriteLine($"\n***************************************************\n");
                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
                Console.WriteLine($"\n***************************************************");

                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item.Produto);
                }

                /*
                    Executaremos a aplicação com "Ctrl + F5". 
                    Poderemos ver que foi executado um SELECT em Produtos no banco de dados, 
                    buscando o produto com o Id passado no filtro Where(). 
                    Em seguida foi feito um segundo SELECT em Compras, 
                    filtrando pelo Id do produto passado no Entry() e pelo preço. 
                    Por fim, serão apresentados os produtos com preço acima de R$ 10,00.

                    Esse cenário é bastante utilizado quando queremos aplicar filtros em 
                    objetos relacionados da aplicação. Esta estratégia é conhecida como 
                    Carregamento Explícito, onde trazemos só o que nos interessa.

 *                 */
                // fazendo join e filtrando os dados da tabela join

                contexto.Entry(produto) // Faz uma pesquina na tabela produto
                            .Collection(p => p.Compras) // Vincula lista de compra na tabela produto
                            .Query() // Faz uma query
                            .Where(c => c.Preco > 10) // filtra a query com as informações da compra
                            .Load(); // carrega os dados da compra.


                Console.ReadLine();
            }
        }


        public static void JoinRelacionamentoMuitosParaMuitos()
        {
            using (var contexto2 = new LojaContext())
            {
                var promocao = contexto2
                              .Promocoes // Tabela Principal
                              .Include(p => p.Produtos) // faz um join entre a tabela promoções e produtos
                              .ThenInclude(pp => pp.Produto)  // Serve para fazer um join em outras tabelas.
                              .FirstOrDefault(); // traz o primiero ou o valor padrão.

                Console.WriteLine("\nMotrando os produtos da promoção...");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
                Console.ReadLine();

            }
        }
        public static void Aula_08_SelectConsulta()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima Total 2017";
                promocao.DataInicio = new DateTime(2017, 1, 1);
                promocao.DataTerminio = new DateTime(2017, 1, 31);

                var produtos = contexto
                .Produtos
                .Where(p => p.Categoria == "Bebidas")
                .ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluirProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
            }// no final do using tira o contexto da memória.


            using (var contexto2 = new LojaContext())
            {
                var promocao = contexto2.Promocoes.FirstOrDefault();
                Console.WriteLine("\nMotrando os produtos da promoção...");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
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
