using System;
using System.Collections.Generic;

namespace EtlVendas.Data.Domain.Entities.Dw
{
    public partial class DmFornecedores
    {
        public DmFornecedores()
        {
            FtVendas = new HashSet<FtVendas>();
        }

        public int IdForn { get; set; }
        public string NomForn { get; set; } = null!;
        public string RegiaoForn { get; set; } = null!;

        public virtual ICollection<FtVendas> FtVendas { get; set; }
    }
}
