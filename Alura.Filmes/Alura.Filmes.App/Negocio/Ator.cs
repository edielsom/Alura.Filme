using Alura.Filmes.App.Negocio;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alura.Filmes.App
{
    public class Ator
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string SegundoNome { get; set; }
        public IList<FilmeAtor>  Filmografia { get; set; }

        public Ator()
        {
            Filmografia = new List<FilmeAtor>();
        }
        // Quando é instânciado um objeto e não especificamos qual o  atributo, é chamado o método ToString()
        public override string ToString()
        {
            return $"Ator ({Id}) : {PrimeiroNome} {SegundoNome}";
        }
    }
}