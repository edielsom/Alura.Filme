
using Microsoft.EntityFrameworkCore;

namespace Alura.Filmes.App
{
    public class AluraFilmeContexto: DbContext
    {

        public DbSet<Ator> Atores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=PROJETO\SQL2014;Initial Catalog=Alura.Filme;User ID=sa;
                                        Password=estadao;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                        ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
