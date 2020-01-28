using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp.Migrations
{
    public class PromocaoProduto
    {
        // Classe de relacionamento Muitos para Muitos, 
        //  onde 1 produto pode ter várias promoções e 1 promoção do ter vários produtos.
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int PromocaoId { get; set; }
        public Promocao Promocao { get; set; }

        
    }
}
