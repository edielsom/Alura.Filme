using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Filmes.App.Negocio
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string AnoLancamento { get; set; }
        public Int16 Duracao { get; set; }

        public override string ToString()
        {
            return $@"Id ({Id}) - Título {Titulo} - Ano de Lançamento - {AnoLancamento}";
        }
    }
}
