namespace EtlVendas.Data.Domain.Entities.Dw;

public class DmProdutos
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