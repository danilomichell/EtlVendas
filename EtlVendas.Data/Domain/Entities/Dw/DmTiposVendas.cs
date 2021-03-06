using System;
using System.Collections.Generic;

namespace EtlVendas.Data.Domain.Entities.Dw
{
    public partial class DmTiposVendas
    {
        public DmTiposVendas()
        {
            FtVendas = new HashSet<FtVendas>();
        }

        public int IdTipoVenda { get; set; }
        public string DescTipoVenda { get; set; } = null!;

        public virtual ICollection<FtVendas> FtVendas { get; set; }
    }
}
