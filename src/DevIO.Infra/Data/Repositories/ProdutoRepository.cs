using DevIO.Business.Models.Produtos;
using DevIO.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext db) : base(db) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid id) =>
            await Db.Produtos.AsNoTracking()
                .Include(f => f.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores() =>
            await Db.Produtos.AsNoTracking()
                .Include(f => f.Fornecedor)
                .OrderBy(p => p.Nome).ToListAsync();

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId) => 
            await Buscar(p => p.FornecedorId == fornecedorId);
    }
}