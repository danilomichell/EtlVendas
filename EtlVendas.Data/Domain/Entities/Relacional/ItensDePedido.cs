namespace EtlVendas.Data.Domain.Entities.Relacional;

public class ItensDePedido
{
    public int NumPed { get; set; }
    public int CodProd { get; set; }
    public int QtdPed { get; set; }
    public decimal PrecoPro { get; set; }

    public virtual Produtos CodProdNavigation { get; set; } = null!;
    public virtual Pedidos NumPedNavigation { get; set; } = null!;
}