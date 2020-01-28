using Alura.Loja.Testes.ConsoleApp.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Promocao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTerminio { get; set; }
        public IList<PromocaoProduto> Produtos { get; set; }

        public Promocao()
        {
            this.Produtos = new List<PromocaoProduto>();
        }

        public void IncluirProduto(Produto produto)
        {
            this.Produtos.Add(new PromocaoProduto() { Produto = produto });
        }
    }
}