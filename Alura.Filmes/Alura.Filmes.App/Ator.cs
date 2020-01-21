using System.ComponentModel.DataAnnotations.Schema;

namespace Alura.Filmes.App
{
    [Table("actor")]
    public class Ator
    {
        [Column("actor_id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string PrimeiroNome { get; set; }
        [Column("last_name")]
        public string SegundoNome { get; set; }


        // Quando é instânciado um objeto e não especificamos qual o  atributo, é chamado o método ToString()
        public override string ToString()
        {
            return $"Ator ({Id}) : {PrimeiroNome} {SegundoNome}";
        }
    }
}