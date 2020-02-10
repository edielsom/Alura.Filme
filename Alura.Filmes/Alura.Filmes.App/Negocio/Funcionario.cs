namespace Alura.Filmes.App.Negocio
{
    public class Funcionario : Pessoa
    {
        public string Login { get; set; }
        public string Senha { get; set; }


        public override string ToString()
        {
            return $"Funcionario ({Id}): {PrimeiroNome} {UltimoNome} - {Ativo}";
        }
    }
}
