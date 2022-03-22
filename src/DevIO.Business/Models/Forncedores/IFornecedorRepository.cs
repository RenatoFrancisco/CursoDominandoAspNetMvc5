using DevIO.Business.Core.Data;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Forncedores
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedoresEndereco(Guid id);
        Task<Fornecedor> ObterFornecedoresProdutosEndereco(Guid id);
    }
}