using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program_Aula02
    {
        //static void Main(string[] args)
        //{
        //    //GravarUsandoAdoNet();
        //    //GravarUsandoEntity();
        //    //RecuperarProduto();
        //    //ExcluirProduto();
        //    //RecuperarProduto();
        //    AtualizaProduto();
        //    Console.ReadLine();
        //}

        private static void AtualizaProduto()
        {
            // Inclui o produto no banco de dados.
            GravarUsandoEntity();
            RecuperarProduto();

            using (var repo = new ProdutoDAOEntity())
            {
                Produto primeiro = repo.Produtos().First();
                primeiro.Nome = "Produto editado";
                repo.Atualizar(primeiro);
            }
            // Lista todos os produtos cadastrado.
            RecuperarProduto();
        }

        public static void ExcluirProduto()
        {
            // Acessa dos dados 
            using (var repo = new ProdutoDAOEntity())
            {
                // Faz um select na tabela produto e reporta uma lista de produto.
                IList<Produto> produto = repo.Produtos();

                // faz um laço de repetição e remove os produtos
                foreach (var item in produto)
                {
                    repo.Remover(item);
                }
            }
        }

        private static void RecuperarProduto()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                // Faz um select na tabela produto e reporta uma lista de produto.
                IList<Produto> produtos = repo.Produtos();
                Console.WriteLine("foram encontrados {0} produto(s)", produtos.Count);

                // faz um laço de repetição e remove os produtos
                foreach (var item in produtos)
                {
                    Console.WriteLine(item.Nome);
                }
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p1 = new Produto();
            p1.Nome = "Harry Potter e a Ordem da Fênix";
            p1.Categoria = "Livros";
            p1.PrecoUnitario = 19.89;
            //p1.Unidade = "6";

            Produto p2 = new Produto();
            p2.Nome = "Senhor dos Anéis 1";
            p2.Categoria = "Livros";
            p2.PrecoUnitario = 19.89;
            //p2.Unidade = "19";

            Produto p3 = new Produto();
            p3.Nome = "O Monge e o Executivo";
            p3.Categoria = "Livros";
            p3.PrecoUnitario = 19.89;
            //p3.Unidade = "10";

            using (var contexto = new ProdutoDAOEntity())
            {
                // O método AddRange insere mais de objeto para ser gravado.
                contexto.Adicionar(p1);
                //contexto.Add(p);
             }
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
    }
}
