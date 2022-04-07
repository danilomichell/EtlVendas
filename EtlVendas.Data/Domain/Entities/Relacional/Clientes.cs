namespace EtlVendas.Data.Domain.Entities.Relacional;

public class Clientes
{
    public Clientes()
    {
        Pedidos = new HashSet<Pedidos>();
    }

    public int CodCli { get; set; }
    public decimal LimCredito { get; set; }
    public decimal SldDevedor { get; set; }
    public string NomCli { get; set; } = null!;
    public string? Fones { get; set; }

    public virtual ICollection<Pedidos> Pedidos { get; set; }
}