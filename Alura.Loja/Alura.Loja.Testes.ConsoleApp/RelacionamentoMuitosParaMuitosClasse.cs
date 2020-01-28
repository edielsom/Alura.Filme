using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class RelacionamentoMuitosParaMuitosClasse
    {
        /* 
         *  CONSULTA EFETUANDO JOIN ENTRE AS TABELAS.
                var cliente = contexto.Clientes
                .Include(c => c.Contas)
                .ThenInclude(cc => cc.Conta)
                .FirstOrDefault();
         */

    }
    public class Conta
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public double Saldo { get; set; }
        public IList<ContaCliente> Clientes { get; set; }
    }

    public class Clientes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public IList<ContaCliente> Contas { get; set; }
    }

    public class ContaCliente
    {
        public int IdConta { get; set; }
        public int IdCliente { get; set; }
        public Conta Conta { get; set; }
        public Clientes Cliente { get; set; }
    }


}
/*  Para saber mais: sobrecarga de Include
 * 
 * O método Include possui uma segunda sobrecarga, 
 * que permite informarmos como argumento de entrada uma string com o 
 * nome da propriedade de navegação a ser incluída no join. 
 * A vantagem dessa abordagem é que não precisamos usar outros 
 * métodos ThenInclude para continuar a navegação em outras entidades. 
 * Por exemplo, para o exemplo Cliente x Conta, poderíamos fazer:
 * 
 * var lista = contexto.Clientes.Include("Contas.Conta");
 * 
 * A desvantagem é que se o nome da propriedade mudar, 
 * teremos que lembrar todos os lugares onde fizemos isso, 
 * porque não teremos ajuda do compilador.

*/
