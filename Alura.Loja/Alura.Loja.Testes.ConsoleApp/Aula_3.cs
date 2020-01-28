using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Aula_3
    {
        /************************* AULA 03 ***********************************/
        #region /************************* AULA 03 ***********************************/
        public Aula_3()
        {
            using (var contexto = new LojaContext())
            {

                var produto = contexto.Produtos.ToList();

                foreach (var p in produto)
                {
                    Console.WriteLine(p);
                }

                Console.WriteLine("======= FAZ A ALTERAÇÃO NO OBJETO==========");

                var p1 = produto.First();
                p1.Nome = "testeASDFSADF";
                //contexto.SaveChanges();
                //produto = contexto.Produtos.ToList();
                //foreach (var p in produto)
                //{
                //    Console.WriteLine(p);
                //}


                Console.WriteLine("======= MOSTRA AS MUDANÇAS NOS OBEJTOS ==========");

                //Método para rastrear as mundaças no entity
                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    //State  registra o estado da entidade, e caso ela tenha sido alterada, o SaveChanges() terá que agir.Mostraremos o estado
                    Console.WriteLine(e.State);
                }
                Console.ReadLine();
            }
        }
        private static void GravarUsandoEntity()
        {
            Produto p1 = new Produto();
            p1.Nome = "Harry Potter e a Ordem da Fênix";
            p1.Categoria = "Livros";
            p1.PrecoUnitario = 19.89;

            Produto p2 = new Produto();
            p2.Nome = "Senhor dos Anéis 1";
            p2.Categoria = "Livros";
            p2.PrecoUnitario = 19.89;

            Produto p3 = new Produto();
            p3.Nome = "O Monge e o Executivo";
            p3.Categoria = "Livros";
            p3.PrecoUnitario = 19.89;

            using (var contexto = new LojaContext())
            {
                // O método AddRange insere mais de objeto para ser gravado.
                contexto.AddRange(p1, p2, p3);
                contexto.SaveChanges();
            }


        }
        #endregion
    }
}
