namespace EtlVendas.Data.Domain.Entities.Relacional;

public class Produtos
{
    public Produtos()
    {
        ItensDeNota = new HashSet<ItensDeNota>();
        ItensDePedido = new HashSet<ItensDePedido>();
    }

    public int CodProd { get; set; }
    public int QtdEstoque { get; set; }
    public string PerParc { get; set; } = null!;
    public decimal PrecoPro { get; set; }
    public int CodForn { get; set; }
    public string DscProd { get; set; } = null!;

    public virtual Fornecedores CodFornNavigation { get; set; } = null!;
    public virtual ICollection<ItensDeNota> ItensDeNota { get; set; }
    public virtual ICollection<ItensDePedido> ItensDePedido { get; set; }
}