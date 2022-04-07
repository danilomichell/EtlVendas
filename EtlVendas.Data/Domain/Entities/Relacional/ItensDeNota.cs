namespace EtlVendas.Data.Domain.Entities.Relacional;

public class ItensDeNota
{
    public int NumNota { get; set; }
    public int CodProd { get; set; }
    public int QtdPed { get; set; }
    public decimal PrecoPro { get; set; }

    public virtual Produtos CodProdNavigation { get; set; } = null!;
    public virtual NotasFiscais NumNotaNavigation { get; set; } = null!;
}