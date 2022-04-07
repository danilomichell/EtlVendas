namespace EtlVendas.Data.Domain.Entities.Relacional;

public class NotasFiscais
{
    public NotasFiscais()
    {
        ItensDeNota = new HashSet<ItensDeNota>();
    }

    public int NumNota { get; set; }
    public int CodForn { get; set; }
    public decimal ValNota { get; set; }
    public decimal PerIcms { get; set; }
    public decimal PerIpi { get; set; }
    public decimal PerFrete { get; set; }
    public decimal ValTotal { get; set; }
    public DateTime DatNota { get; set; }
    public DateTime DatVenc { get; set; }
    public string StaNota { get; set; } = null!;

    public virtual Fornecedores CodFornNavigation { get; set; } = null!;
    public virtual ICollection<ItensDeNota> ItensDeNota { get; set; }
}