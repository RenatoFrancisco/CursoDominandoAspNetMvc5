using DevIO.Business.Models.Fornecedores;
using DevIO.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext db) : base(db) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId) => await ObterPorId(fornecedorId);
    }
}