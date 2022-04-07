namespace EtlVendas.Data.Domain.Entities.Dw;

public class DmClientes
{
    public DmClientes()
    {
        FtImpontualidade = new HashSet<FtImpontualidade>();
    }

    public int IdCliente { get; set; }
    public string NomeCliente { get; set; } = null!;
    public string CidadeCli { get; set; } = null!;
    public string UfCli { get; set; } = null!;

    public virtual ICollection<FtImpontualidade> FtImpontualidade { get; set; }
}