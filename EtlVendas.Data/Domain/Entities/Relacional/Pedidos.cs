namespace EtlVendas.Data.Domain.Entities.Relacional;

public class Pedidos
{
    public Pedidos()
    {
        ItensDePedido = new HashSet<ItensDePedido>();
        Parcelas = new HashSet<Parcelas>();
    }

    public int NumPed { get; set; }
    public int CodCli { get; set; }
    public DateTime DatPed { get; set; }
    public string StaPedido { get; set; } = null!;
    public decimal ValPed { get; set; }
    public decimal ValAVista { get; set; }
    public decimal ValAPrazo { get; set; }
    public decimal SldDevedor { get; set; }

    public virtual Clientes CodCliNavigation { get; set; } = null!;
    public virtual ICollection<ItensDePedido> ItensDePedido { get; set; }
    public virtual ICollection<Parcelas> Parcelas { get; set; }
}