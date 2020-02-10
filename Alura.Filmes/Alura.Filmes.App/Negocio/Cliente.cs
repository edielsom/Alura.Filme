using Alura.Filmes.App.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Filmes.App
{
    public class Cliente : Pessoa
    {
        public override string ToString()
        {
            return $"Pessoa ({Id}): {PrimeiroNome} {UltimoNome} - {Ativo}";
        }
    }
}
