namespace EtlVendas.Data.Domain.Entities.Dw;

public class FtVendas
{
    public int IdProd { get; set; }
    public int IdTempo { get; set; }
    public int IdTipoVenda { get; set; }
    public int IdForn { get; set; }
    public decimal ValorVenda { get; set; }

    public virtual DmFornecedores IdFornNavigation { get; set; } = null!;
    public virtual DmProdutos IdProdNavigation { get; set; } = null!;
    public virtual DmTempo IdTempoNavigation { get; set; } = null!;
    public virtual DmTiposVendas IdTipoVendaNavigation { get; set; } = null!;
}