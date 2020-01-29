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
        public short Duracao { get; set; }
        public string Classificacao { get; set; }

        // A propriedade de um para muitos na classe principal sempre será uma 
        // coleção de classe com a classe de navegação.
        public IList<FilmeAtor> Atores { get; set; }
        public Idioma IdiomaFalado { get; set; }// Propriedade de navegação de referência.
        public Idioma IdiomaOriginal { get; set; }// Propriedade de navegação de referência.


        /****************** OBSERVAÇÃO *****************************/
        /*
         *  Quando a duas propriedades de navegação em uma classe, o Entity não realiza as relações de forma automática.
         *  Portanto, tem de assumir a configuração dos dois elementos.
         *  
         *  As duas propriedades que criamos (IdiomaFalado, IdiomaOriginal) são de navegação, e como cada uma está apontando 
         *  apenas para uma instância de idioma, são chamadas de propriedades de navegação de referência.
         */
        public Filme()
        {
            Atores = new List<FilmeAtor>();
        }
        public override string ToString()
        {
            return $@"Id ({Id}) - Título {Titulo} - Ano de Lançamento - {AnoLancamento}";
        }
    }
}
