namespace EtlVendas.Data.Domain.Entities.Relacional;

public class Parcelas
{
    public int NumPed { get; set; }
    public DateTime DatVenc { get; set; }
    public decimal ValParc { get; set; }
    public string ParcPaga { get; set; } = null!;

    public virtual Pedidos NumPedNavigation { get; set; } = null!;
}