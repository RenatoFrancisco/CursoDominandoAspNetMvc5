using DevIO.Business.Core.Models;
using DevIO.Business.Models.Produtos;
using System.Collections.Generic;

namespace DevIO.Business.Models.Forncedores
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoForncedor TipoForncedor { get; set; }
        public Endereco Endereco { get; set; }

        // EF Relations
        public ICollection<Produto> Produtos { get; set; }
    }
}