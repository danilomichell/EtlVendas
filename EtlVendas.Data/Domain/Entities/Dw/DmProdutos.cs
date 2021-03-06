using System;
using System.Collections.Generic;

namespace EtlVendas.Data.Domain.Entities.Dw
{
    public partial class DmProdutos
    {
        public DmProdutos()
        {
            FtVendas = new HashSet<FtVendas>();
        }

        public int IdProd { get; set; }
        public string DscProd { get; set; } = null!;
        public string ClasseProd { get; set; } = null!;

        public virtual ICollection<FtVendas> FtVendas { get; set; }
    }
}
