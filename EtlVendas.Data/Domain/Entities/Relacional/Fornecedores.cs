namespace EtlVendas.Data.Domain.Entities.Relacional;

public class Fornecedores
{
    public Fornecedores()
    {
        NotasFiscais = new HashSet<NotasFiscais>();
        Produtos = new HashSet<Produtos>();
    }

    public int CodForn { get; set; }
    public string UfForn { get; set; } = null!;
    public decimal SldCredor { get; set; }
    public string NomForn { get; set; } = null!;

    public virtual ICollection<NotasFiscais> NotasFiscais { get; set; }
    public virtual ICollection<Produtos> Produtos { get; set; }
}