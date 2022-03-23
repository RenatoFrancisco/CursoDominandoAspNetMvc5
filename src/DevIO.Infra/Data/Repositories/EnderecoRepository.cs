using DevIO.Business.Models.Forncedores;
using System;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId) => await ObterPorId(fornecedorId);
    }
}