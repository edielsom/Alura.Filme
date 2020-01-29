using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Alura.Filmes.App.Negocio
{
    public class Idioma  
    {
       

        public int Id { get; set; }
        public string Nome { get; set; }

        // A propriedade de um para muitos na classe principal sempre será uma 
        // coleção de classe com a classe de navegação.
        public IList<Filme> FilmesFalados { get; set; } 
        public IList<Filme> FilmesOriginais { get; set; }

        public Idioma()
        {
            FilmesFalados = new List<Filme>();
            FilmesOriginais = new List<Filme>();
        }
        public override string ToString()
        {
            return $"Idioma ({Id}) : {Nome}";
        }
    }
}
