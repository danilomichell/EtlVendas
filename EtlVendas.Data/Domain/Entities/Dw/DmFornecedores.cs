namespace EtlVendas.Data.Domain.Entities.Dw;

public class DmFornecedores
{
    public int IdForn { get; set; }
    public string NomForn { get; set; } = null!;
    public string RegiaoForn { get; set; } = null!;

    public virtual FtVendas FtVendas { get; set; } = null!;
}