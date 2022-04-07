using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EtlVendas.Data.Domain.Entities.Dw;

namespace EtlVendas.Data.Context
{
    public partial class VendasDwContext : DbContext
    {
        public VendasDwContext(DbContextOptions<VendasDwContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DmClientes> DmClientes { get; set; } = null!;
        public virtual DbSet<DmFornecedores> DmFornecedores { get; set; } = null!;
        public virtual DbSet<DmProdutos> DmProdutos { get; set; } = null!;
        public virtual DbSet<DmTempo> DmTempo { get; set; } = null!;
        public virtual DbSet<DmTiposVendas> DmTiposVendas { get; set; } = null!;
        public virtual DbSet<FtImpontualidade> FtImpontualidade { get; set; } = null!;
        public virtual DbSet<FtVendas> FtVendas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DW_VENDAS");

            modelBuilder.Entity<DmClientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("DM_CLIENTES_PK");

                entity.ToTable("DM_CLIENTES");

                entity.Property(e => e.IdCliente)
                    .HasPrecision(6)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_CLIENTE");

                entity.Property(e => e.CidadeCli)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CIDADE_CLI");

                entity.Property(e => e.NomeCliente)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("NOME_CLIENTE");

                entity.Property(e => e.UfCli)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("UF_CLI")
                    .IsFixedLength();
            });

            modelBuilder.Entity<DmFornecedores>(entity =>
            {
                entity.HasKey(e => e.IdForn)
                    .HasName("DM_FORNECEDORES_PK");

                entity.ToTable("DM_FORNECEDORES");

                entity.Property(e => e.IdForn)
                    .HasPrecision(6)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_FORN");

                entity.Property(e => e.NomForn)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("NOM_FORN");

                entity.Property(e => e.RegiaoForn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("REGIAO_FORN");
            });

            modelBuilder.Entity<DmProdutos>(entity =>
            {
                entity.HasKey(e => e.IdProd)
                    .HasName("DM_PRODUTOS_PK");

                entity.ToTable("DM_PRODUTOS");

                entity.Property(e => e.IdProd)
                    .HasPrecision(6)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_PROD");

                entity.Property(e => e.ClasseProd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLASSE_PROD");

                entity.Property(e => e.DscProd)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DSC_PROD");
            });

            modelBuilder.Entity<DmTempo>(entity =>
            {
                entity.HasKey(e => e.IdTempo)
                    .HasName("DM_TEMPO_PK");

                entity.ToTable("DM_TEMPO");

                entity.Property(e => e.IdTempo)
                    .HasPrecision(5)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TEMPO");

                entity.Property(e => e.NmMes)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NM_MES");

                entity.Property(e => e.NmMesano)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("NM_MESANO")
                    .IsFixedLength();

                entity.Property(e => e.NuAno)
                    .HasPrecision(4)
                    .HasColumnName("NU_ANO");

                entity.Property(e => e.NuAnomes)
                    .HasPrecision(7)
                    .HasColumnName("NU_ANOMES");

                entity.Property(e => e.NuDia)
                    .HasPrecision(2)
                    .HasColumnName("NU_DIA");

                entity.Property(e => e.NuMes)
                    .HasPrecision(2)
                    .HasColumnName("NU_MES");

                entity.Property(e => e.SgMes)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SG_MES")
                    .IsFixedLength();
            });

            modelBuilder.Entity<DmTiposVendas>(entity =>
            {
                entity.HasKey(e => e.IdTipoVenda)
                    .HasName("DM_TIPOS_VENDAS_PK");

                entity.ToTable("DM_TIPOS_VENDAS");

                entity.Property(e => e.IdTipoVenda)
                    .HasPrecision(3)
                    .HasColumnName("ID_TIPO_VENDA");

                entity.Property(e => e.DescTipoVenda)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESC_TIPO_VENDA");
            });

            modelBuilder.Entity<FtImpontualidade>(entity =>
            {
                entity.HasKey(e => new { e.IdTempo, e.IdCliente })
                    .HasName("FT_IMPONTUALIDADE_PK");

                entity.ToTable("FT_IMPONTUALIDADE");

                entity.Property(e => e.IdTempo)
                    .HasPrecision(5)
                    .HasColumnName("ID_TEMPO");

                entity.Property(e => e.IdCliente)
                    .HasPrecision(6)
                    .HasColumnName("ID_CLIENTE");

                entity.Property(e => e.ValorParcAtrasadas)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("VALOR_PARC_ATRASADAS");

                entity.Property(e => e.ValorParcTotal)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("VALOR_PARC_TOTAL");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.FtImpontualidade)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FT_IMP_DM_CLI_FK");

                entity.HasOne(d => d.IdTempoNavigation)
                    .WithMany(p => p.FtImpontualidade)
                    .HasForeignKey(d => d.IdTempo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FT_IMP_DM_TEM_FK");
            });

            modelBuilder.Entity<FtVendas>(entity =>
            {
                entity.HasKey(e => new { e.IdProd, e.IdTempo, e.IdTipoVenda, e.IdForn })
                    .HasName("FT_VENDAS_PK");

                entity.ToTable("FT_VENDAS");

                entity.Property(e => e.IdProd)
                    .HasPrecision(6)
                    .HasColumnName("ID_PROD");

                entity.Property(e => e.IdTempo)
                    .HasPrecision(5)
                    .HasColumnName("ID_TEMPO");

                entity.Property(e => e.IdTipoVenda)
                    .HasPrecision(3)
                    .HasColumnName("ID_TIPO_VENDA");

                entity.Property(e => e.IdForn)
                    .HasPrecision(6)
                    .HasColumnName("ID_FORN");

                entity.Property(e => e.ValorVenda)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("VALOR_VENDA");

                entity.HasOne(d => d.IdFornNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdForn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FT_VENDAS_DM_FORNECEDORES_FK");

                entity.HasOne(d => d.IdProdNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdProd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FT_VENDAS_DM_PRODUTOS_FK");

                entity.HasOne(d => d.IdTempoNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdTempo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FT_VENDAS_DM_TEMPO_FK");

                entity.HasOne(d => d.IdTipoVendaNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdTipoVenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FT_VENDAS_DM_TIPOS_VENDAS_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
