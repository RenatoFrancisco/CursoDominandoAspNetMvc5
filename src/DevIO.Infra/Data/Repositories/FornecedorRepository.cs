using DevIO.Business.Models.Fornecedores;
using DevIO.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppDbContext db) : base(db) { }

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