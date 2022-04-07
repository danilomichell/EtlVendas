namespace EtlVendas.Data.Domain.Entities.Dw;

public class FtImpontualidade
{
    public int IdTempo { get; set; }
    public int IdCliente { get; set; }
    public decimal ValorParcAtrasadas { get; set; }
    public decimal ValorParcTotal { get; set; }

    public virtual DmClientes IdClienteNavigation { get; set; } = null!;
    public virtual DmTempo IdTempoNavigation { get; set; } = null!;
}