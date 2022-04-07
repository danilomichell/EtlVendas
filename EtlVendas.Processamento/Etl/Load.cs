using System.Data;
using System.Diagnostics;
using EtlVendas.Data.Context;
using EtlVendas.Data.Domain.Entities.Dw;
using Microsoft.EntityFrameworkCore;

namespace EtlVendas.Processamento.Etl;

public class Load
{
    public Load(Transform transform, VendasDwContext context)
    {
        CarregarDmTempo(transform.DmTempo, context);
        CarregarDmCliente(transform.DmClientes, context);
        CarregarDmFornecedores(transform.DmFornecedores, context);
        CarregarDmProdutos(transform.DmProdutos, context);
        CarregarDmTiposVendas(transform.DmTiposVendas, context);
        CarregaFtVendas(transform.FtVendas, context);
    }

    public void CarregarDmTempo(List<DmTempo> tempos, VendasDwContext context)
    {
        Console.WriteLine("Iniciando cargda dos tempos");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in tempos)
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            if (command.Connection!.State != ConnectionState.Open) command.Connection.Open();
            var tempoExist = context.DmTempo.FirstOrDefault(x => x.IdTempo == item.IdTempo);
            var cmd = tempoExist != null ? $@"UPDATE DW_VENDAS.DM_TEMPO 
                                                    SET NU_DIA = '{item.NuDia}',
                                                        NU_MES = '{item.NuMes}',
                                                        NU_ANO = '{item.NuAno}',
                                                        NU_ANOMES = '{item.NuAnomes}',
                                                        SG_MES = '{item.SgMes}',
                                                        NM_MES = '{item.NmMes}',
                                                        NM_MESANO = '{item.NmMesano}'
                                                    WHERE ID_TEMPO = {item.NmMesano}" : $@"INSERT INTO DW_VENDAS.DM_TEMPO
                                                    (ID_TEMPO, NU_DIA, NU_MES, NU_ANO,NU_ANOMES,SG_MES,NM_MES,NM_MESANO)
                                                    VALUES
                                                    ({item.IdTempo}, '{item.NuDia}', '{item.NuMes}', '{item.NuAno}','{item.NuAnomes}','{item.SgMes}','{item.NmMes}','{item.NmMesano}')";
            command.CommandText = cmd;
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        sw.Stop();
        Console.WriteLine("Finalizando carga dos tempos" +
                          $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
    }

    public void CarregarDmCliente(List<DmClientes> clientes, VendasDwContext context)
    {
        Console.WriteLine("Iniciando cargda dos clientes");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in clientes)
        {
            var itemExist = context.DmClientes.FirstOrDefault(x => x.IdCliente == item.IdCliente);
            if (itemExist != null)
            {
                itemExist.NomeCliente = item.NomeCliente;
                itemExist.CidadeCli = item.CidadeCli;
                itemExist.UfCli = item.UfCli;
            }
            else
            {
                context.DmClientes.Add(item);
            }
        }

        context.SaveChanges();
        sw.Stop();
        Console.WriteLine("Finalizando carga dos clientes" +
                          $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
    }

    public void CarregarDmFornecedores(List<DmFornecedores> fornecedores, VendasDwContext context)
    {
        Console.WriteLine("Iniciando cargda dos fornecedores");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in fornecedores)
        {
            var itemExist = context.DmFornecedores.FirstOrDefault(x => x.IdForn == item.IdForn);
            if (itemExist != null)
            {
                itemExist.NomForn = item.NomForn;
                itemExist.RegiaoForn = item.RegiaoForn;
            }
            else
            {
                context.DmFornecedores.Add(item);
            }
        }

        context.SaveChanges();
        sw.Stop();
        Console.WriteLine("Finalizando carga dos fornecedores" +
                          $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
    }

    public void CarregarDmProdutos(List<DmProdutos> produtos, VendasDwContext context)
    {
        Console.WriteLine("Iniciando cargda dos produtos");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in produtos)
        {
            var itemExist = context.DmProdutos.FirstOrDefault(x => x.IdProd == item.IdProd);
            if (itemExist != null)
            {
                itemExist.ClasseProd = item.ClasseProd;
                itemExist.DscProd = item.DscProd;
                context.DmProdutos.Update(itemExist);
            }
            else
            {
                context.DmProdutos.Add(item);
            }
        }

        context.SaveChanges();
        sw.Stop();
        Console.WriteLine("Finalizando carga dos produtos" +
                          $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
    }

    public void CarregarDmTiposVendas(List<DmTiposVendas> tpVendas, VendasDwContext context)
    {
        Console.WriteLine("Iniciando cargda dos tipos de vendas");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in tpVendas)
        {
            var itemExist = context.DmTiposVendas.FirstOrDefault(x => x.IdTipoVenda == item.IdTipoVenda);
            if (itemExist != null)
            {
                itemExist.DescTipoVenda = item.DescTipoVenda;
                context.DmTiposVendas.Update(itemExist);
            }
            else
            {
                context.DmTiposVendas.Add(item);
            }
        }

        context.SaveChanges();
        sw.Stop();
        Console.WriteLine("Finalizando carga dos tipos de vendas" +
                          $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
    }

    public void CarregaFtVendas(List<FtVendas> ftVendas, VendasDwContext context)
    {
        var sw = new Stopwatch();
        sw.Start();
        var valores = context.FtVendas.ToList();
        if (valores.Count != 0)
        {
            context.RemoveRange(valores);
            context.SaveChanges();
        }
        context.FtVendas.AddRange(ftVendas);
        context.SaveChanges();
        sw.Stop();
        Console.WriteLine("Finalizando carga das locações" +
                          $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
    }
}