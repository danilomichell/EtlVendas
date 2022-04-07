using EtlVendas.Data.Domain.Entities.Relacional;
using Microsoft.EntityFrameworkCore;

namespace EtlVendas.Data.Context;

public class VendasContext : DbContext
{
    public VendasContext(DbContextOptions<VendasContext> options)
           : base(options)
    {
    }

    public virtual DbSet<Clientes> Clientes { get; set; } = null!;
    public virtual DbSet<Fornecedores> Fornecedores { get; set; } = null!;
    public virtual DbSet<ItensDeNota> ItensDeNota { get; set; } = null!;
    public virtual DbSet<ItensDePedido> ItensDePedido { get; set; } = null!;
    public virtual DbSet<NotasFiscais> NotasFiscais { get; set; } = null!;
    public virtual DbSet<Parcelas> Parcelas { get; set; } = null!;
    public virtual DbSet<Pedidos> Pedidos { get; set; } = null!;
    public virtual DbSet<Produtos> Produtos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("VENDAS");

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.CodCli)
                .HasName("CLI_PK");

            entity.ToTable("CLIENTES");

            entity.HasIndex(e => e.NomCli, "CLI_IDX_NOM_CLI");

            entity.Property(e => e.CodCli)
                .HasPrecision(5)
                .ValueGeneratedNever()
                .HasColumnName("COD_CLI");

            entity.Property(e => e.Fones)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("FONES");

            entity.Property(e => e.LimCredito)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("LIM_CREDITO");

            entity.Property(e => e.NomCli)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM_CLI");

            entity.Property(e => e.SldDevedor)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("SLD_DEVEDOR");
        });

        modelBuilder.Entity<Fornecedores>(entity =>
        {
            entity.HasKey(e => e.CodForn)
                .HasName("FOR_PK");

            entity.ToTable("FORNECEDORES");

            entity.HasIndex(e => e.NomForn, "FOR_IDX_NOM_FORN");

            entity.Property(e => e.CodForn)
                .HasPrecision(3)
                .HasColumnName("COD_FORN");

            entity.Property(e => e.NomForn)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM_FORN");

            entity.Property(e => e.SldCredor)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("SLD_CREDOR");

            entity.Property(e => e.UfForn)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("UF_FORN")
                .IsFixedLength();
        });

        modelBuilder.Entity<ItensDeNota>(entity =>
        {
            entity.HasKey(e => new { e.NumNota, e.CodProd })
                .HasName("ITN_PK");

            entity.ToTable("ITENS_DE_NOTA");

            entity.HasIndex(e => e.CodProd, "ITN_IDX_COD_PROD");

            entity.HasIndex(e => e.NumNota, "ITN_IDX_NUM_NOTA");

            entity.Property(e => e.NumNota)
                .HasPrecision(5)
                .HasColumnName("NUM_NOTA");

            entity.Property(e => e.CodProd)
                .HasPrecision(5)
                .HasColumnName("COD_PROD");

            entity.Property(e => e.PrecoPro)
                .HasColumnType("NUMBER(6,2)")
                .HasColumnName("PRECO_PRO");

            entity.Property(e => e.QtdPed)
                .HasPrecision(4)
                .HasColumnName("QTD_PED");

            entity.HasOne(d => d.CodProdNavigation)
                .WithMany(p => p.ItensDeNota)
                .HasForeignKey(d => d.CodProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ITN_FK_PRO");

            entity.HasOne(d => d.NumNotaNavigation)
                .WithMany(p => p.ItensDeNota)
                .HasForeignKey(d => d.NumNota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ITN_FK_NOT");
        });

        modelBuilder.Entity<ItensDePedido>(entity =>
        {
            entity.HasKey(e => new { e.NumPed, e.CodProd })
                .HasName("ITP_PK");

            entity.ToTable("ITENS_DE_PEDIDO");

            entity.HasIndex(e => e.CodProd, "ITP_IDX_COD_PROD");

            entity.HasIndex(e => e.NumPed, "ITP_IDX_NUM_PED");

            entity.Property(e => e.NumPed)
                .HasPrecision(5)
                .HasColumnName("NUM_PED");

            entity.Property(e => e.CodProd)
                .HasPrecision(5)
                .HasColumnName("COD_PROD");

            entity.Property(e => e.PrecoPro)
                .HasColumnType("NUMBER(6,2)")
                .HasColumnName("PRECO_PRO");

            entity.Property(e => e.QtdPed)
                .HasPrecision(4)
                .HasColumnName("QTD_PED");

            entity.HasOne(d => d.CodProdNavigation)
                .WithMany(p => p.ItensDePedido)
                .HasForeignKey(d => d.CodProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ITP_FK_PRO");

            entity.HasOne(d => d.NumPedNavigation)
                .WithMany(p => p.ItensDePedido)
                .HasForeignKey(d => d.NumPed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ITP_FK_PED");
        });

        modelBuilder.Entity<NotasFiscais>(entity =>
        {
            entity.HasKey(e => e.NumNota)
                .HasName("NOT_PK");

            entity.ToTable("NOTAS_FISCAIS");

            entity.HasIndex(e => e.CodForn, "NOT_IDX_COD_FORN");

            entity.Property(e => e.NumNota)
                .HasPrecision(5)
                .ValueGeneratedNever()
                .HasColumnName("NUM_NOTA");

            entity.Property(e => e.CodForn)
                .HasPrecision(3)
                .HasColumnName("COD_FORN");

            entity.Property(e => e.DatNota)
                .HasColumnType("DATE")
                .HasColumnName("DAT_NOTA");

            entity.Property(e => e.DatVenc)
                .HasColumnType("DATE")
                .HasColumnName("DAT_VENC");

            entity.Property(e => e.PerFrete)
                .HasColumnType("NUMBER(4,1)")
                .HasColumnName("PER_FRETE");

            entity.Property(e => e.PerIcms)
                .HasColumnType("NUMBER(4,1)")
                .HasColumnName("PER_ICMS");

            entity.Property(e => e.PerIpi)
                .HasColumnType("NUMBER(4,1)")
                .HasColumnName("PER_IPI");

            entity.Property(e => e.StaNota)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("STA_NOTA")
                .IsFixedLength();

            entity.Property(e => e.ValNota)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("VAL_NOTA");

            entity.Property(e => e.ValTotal)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("VAL_TOTAL");

            entity.HasOne(d => d.CodFornNavigation)
                .WithMany(p => p.NotasFiscais)
                .HasForeignKey(d => d.CodForn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("NOT_FK_FOR");
        });

        modelBuilder.Entity<Parcelas>(entity =>
        {
            entity.HasKey(e => new { e.NumPed, e.DatVenc })
                .HasName("PAR_PK");

            entity.ToTable("PARCELAS");

            entity.HasIndex(e => e.NumPed, "PAR_IDX_NUM_PED");

            entity.Property(e => e.NumPed)
                .HasPrecision(5)
                .HasColumnName("NUM_PED");

            entity.Property(e => e.DatVenc)
                .HasColumnType("DATE")
                .HasColumnName("DAT_VENC");

            entity.Property(e => e.ParcPaga)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PARC_PAGA")
                .IsFixedLength();

            entity.Property(e => e.ValParc)
                .HasColumnType("NUMBER(8,2)")
                .HasColumnName("VAL_PARC");

            entity.HasOne(d => d.NumPedNavigation)
                .WithMany(p => p.Parcelas)
                .HasForeignKey(d => d.NumPed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PAR_FK_PED");
        });

        modelBuilder.Entity<Pedidos>(entity =>
        {
            entity.HasKey(e => e.NumPed)
                .HasName("PED_PK");

            entity.ToTable("PEDIDOS");

            entity.HasIndex(e => e.CodCli, "PED_IDX_COD_CLI");

            entity.Property(e => e.NumPed)
                .HasPrecision(5)
                .ValueGeneratedNever()
                .HasColumnName("NUM_PED");

            entity.Property(e => e.CodCli)
                .HasPrecision(5)
                .HasColumnName("COD_CLI");

            entity.Property(e => e.DatPed)
                .HasColumnType("DATE")
                .HasColumnName("DAT_PED");

            entity.Property(e => e.SldDevedor)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("SLD_DEVEDOR");

            entity.Property(e => e.StaPedido)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("STA_PEDIDO")
                .IsFixedLength();

            entity.Property(e => e.ValAPrazo)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("VAL_A_PRAZO");

            entity.Property(e => e.ValAVista)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("VAL_A_VISTA");

            entity.Property(e => e.ValPed)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("VAL_PED");

            entity.HasOne(d => d.CodCliNavigation)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CodCli)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PED_FK_CLI");
        });

        modelBuilder.Entity<Produtos>(entity =>
        {
            entity.HasKey(e => e.CodProd)
                .HasName("PRO_PK");

            entity.ToTable("PRODUTOS");

            entity.HasIndex(e => e.DscProd, "PRO_IDX_DSC_PROD");

            entity.Property(e => e.CodProd)
                .HasPrecision(5)
                .ValueGeneratedNever()
                .HasColumnName("COD_PROD");

            entity.Property(e => e.CodForn)
                .HasPrecision(3)
                .HasColumnName("COD_FORN");

            entity.Property(e => e.DscProd)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("DSC_PROD");

            entity.Property(e => e.PerParc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PER_PARC")
                .IsFixedLength();

            entity.Property(e => e.PrecoPro)
                .HasColumnType("NUMBER(6,2)")
                .HasColumnName("PRECO_PRO");

            entity.Property(e => e.QtdEstoque)
                .HasPrecision(4)
                .HasColumnName("QTD_ESTOQUE");

            entity.HasOne(d => d.CodFornNavigation)
                .WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CodForn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRO_FK_FOR");
        });
    }
}