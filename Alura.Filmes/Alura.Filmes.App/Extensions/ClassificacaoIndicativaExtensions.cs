using Alura.Filmes.App.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Filmes.App.Extensions
{
    /*  Criando classe de extesão.
     *  O método de extensão é feito por meio de uma classe estática, 
     *  cujo nome é formado pela denominação da classe que pretendemos estender, seguido do sufixo "extensions".
     */
    public static class ClassificacaoIndicativaExtensions
    {
        /* Declaração de Mapa através de Dictionary, onde:
         * o primeiro parâmetro é a chave (key)
         * e o segundo valor é o valor (value)
         */

        public static Dictionary<string, ClassificacaoIndicativa> mapa = new Dictionary<string, ClassificacaoIndicativa>()
        {
                 { "G", ClassificacaoIndicativa.Livre },
                { "PG", ClassificacaoIndicativa.MaioresQue10 },
                { "PG-13", ClassificacaoIndicativa.MaioresQue13 },
                { "R", ClassificacaoIndicativa.MaioresQue14 },
                { "NC-17", ClassificacaoIndicativa.MaioresQue18 }
        };

        // Quando a Classe é de extesão e estático, o método também deverá ser estático.
        public static string ParaString(this ClassificacaoIndicativa valor)
        {
            /*utilizaremos um mapa, contendo as correspondências entre os valores.
                O tipo de dado utilizado no C# para mapas é o Dictionary.*/

            /* A condição é que a segunda parte (Value) seja igual ao argumento (valor)
                que está sendo passado no método. E retornaremos o primeiro parâmetro Key.
            */
            return mapa.First(c => c.Value == valor).Key;
        }


        // Quando a Classe é de extesão e estático, o método também deverá ser estático.
        public static ClassificacaoIndicativa ParaValor(this string texto)
        {
            /*utilizaremos um mapa, contendo as correspondências entre os valores.
                O tipo de dado utilizado no C# para mapas é o Dictionary.*/

            /* A condição é que a primeira parte (Value) seja igual ao argumento (texto)
                que está sendo passado no método. E retornaremos o primeiro parâmetro Value.
            */
            return mapa.First(c => c.Key == texto).Value;
        }
    }
}
