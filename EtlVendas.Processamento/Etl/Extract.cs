using System.Diagnostics;
using EtlVendas.Data.Context;
using EtlVendas.Data.Domain.Entities.Relacional;
using Microsoft.EntityFrameworkCore;

namespace EtlVendas.Processamento.Etl;

public class Extract
{
    //public List<Locacoes> Locacoes { get; private set; } = new();
    public Extract(VendasContext context)
    {
        ExtrairTempo(context);
        ExtrairCliente(context);
        ExtrairFornecedores(context);
        ExtrairProdutos(context);
        ExtrairPedidos(context);
        //ExtrairLocacoes(context);
    }

    public List<DateTime> Tempo { get; private set; } = new();
    public List<Clientes> Clientes { get; private set; } = new();
    public List<Fornecedores> Fornecedores { get; private set; } = new();
    public List<Produtos> Produtos { get; private set; } = new();
    public List<Pedidos> Pedidos { get; private set; } = new();

    private void ExtrairTempo(VendasContext context)
    {
        Console.WriteLine("Iniciando extração do Tempo");
        var sw = new Stopwatch();
        sw.Start();
        Tempo = context.Pedidos.Select(x => x.DatPed).Distinct().ToList();
        sw.Stop();

        Console.WriteLine("Finalizando extração do Tempo" +
                          $" - Total extraido: {Tempo.Count}" +
                          $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void ExtrairCliente(VendasContext context)
    {
        Console.WriteLine("Iniciando extração dos clientes");
        var sw = new Stopwatch();
        sw.Start();

        Clientes = context.Clientes.Distinct().ToList();

        sw.Stop();
        Console.WriteLine("Finalizando extração dos clientes" +
                          $" - Total extraido: {Clientes.Count}" +
                          $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void ExtrairFornecedores(VendasContext context)
    {
        Console.WriteLine("Iniciando extração dos fornecedores");
        var sw = new Stopwatch();
        sw.Start();

        Fornecedores = context.Fornecedores.Distinct().ToList();
        sw.Stop();
        Console.WriteLine("Finalizando extração dos fornecedores" +
                          $" - Total extraido: {Fornecedores.Count}" +
                          $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void ExtrairProdutos(VendasContext context)
    {
        Console.WriteLine("Iniciando extração dos produtos");
        var sw = new Stopwatch();
        sw.Start();

        Produtos = context.Produtos.Distinct().ToList();

        sw.Stop();
        Console.WriteLine("Finalizando extração dos produtos" +
                          $" - Total extraido: {Produtos.Count}" +
                          $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void ExtrairPedidos(VendasContext context)
    {
        Console.WriteLine("Iniciando extração das pedidos");
        var sw = new Stopwatch();
        sw.Start();

        Pedidos = context.Pedidos.Include(x => x.CodCliNavigation)
            .Include(x => x.ItensDePedido)
            .ThenInclude(x => x.CodProdNavigation)
            .ThenInclude(x => x.CodFornNavigation)
            .Include(x=>x.Parcelas)
            .ToList();

        sw.Stop();
        Console.WriteLine("Finalizando extração das pedidos" +
                          $" - Total extraido: {Pedidos.Count}" +
                          $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");
    }
}