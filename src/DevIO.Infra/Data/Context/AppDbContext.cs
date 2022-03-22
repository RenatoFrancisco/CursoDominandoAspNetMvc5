using DevIO.Business.Models.Forncedores;
using DevIO.Business.Models.Produtos;
using System.Data.Entity;

namespace DevIO.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}