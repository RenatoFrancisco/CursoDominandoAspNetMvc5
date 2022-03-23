using DevIO.Business.Models.Forncedores;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repositories
{
    public class ForncedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public async Task<Fornecedor> ObterFornecedoresEndereco(Guid id) =>
            await Db.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<Fornecedor> ObterFornecedoresProdutosEndereco(Guid id) =>
            await Db.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .Include(f => f.Produtos)
                .FirstOrDefaultAsync(f => f.Id == id);
    }
}