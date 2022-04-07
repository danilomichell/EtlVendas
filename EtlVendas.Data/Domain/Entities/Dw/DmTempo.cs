namespace EtlVendas.Data.Domain.Entities.Dw;

public class DmTempo
{
    public DmTempo()
    {
        FtImpontualidade = new HashSet<FtImpontualidade>();
        FtVendas = new HashSet<FtVendas>();
    }

    public int IdTempo { get; set; }
    public int NuAno { get; set; }
    public int NuMes { get; set; }
    public int NuAnomes { get; set; }
    public string SgMes { get; set; } = null!;
    public string NmMesano { get; set; } = null!;
    public string NmMes { get; set; } = null!;
    public int NuDia { get; set; }

    public virtual ICollection<FtImpontualidade> FtImpontualidade { get; set; }
    public virtual ICollection<FtVendas> FtVendas { get; set; }
}