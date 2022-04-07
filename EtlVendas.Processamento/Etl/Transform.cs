using System.Diagnostics;
using EtlVendas.Data.Domain.Entities.Dw;
using EtlVendas.Data.Domain.Entities.Relacional;

namespace EtlVendas.Processamento.Etl;

public class Transform
{
    //public List<FtLocacoes> FtLocacoes { get; private set; } = new();

    public Transform(Extract extracao)
    {
        TransformarTempo(extracao.Tempo);
        TransformarClientes(extracao.Clientes);
        TransformarFornecedores(extracao.Fornecedores);
        TransformarProdutos(extracao.Produtos);
        TransformarTiposVendas(extracao.Pedidos);
        TransformarFtVendas(extracao.Pedidos);
        TransformarFtInadimplencia(extracao.Pedidos);
    }

    public List<DmTempo> DmTempo { get; } = new();
    public List<DmClientes> DmClientes { get; } = new();
    public List<DmFornecedores> DmFornecedores { get; } = new();

    public List<DmProdutos> DmProdutos { get; } = new();
    public List<DmTiposVendas> DmTiposVendas { get; } = new();
    public List<FtVendas> FtVendas { get; } = new();
    public List<FtImpontualidade> FtImpontualidade { get; } = new();

    private void TransformarTempo(List<DateTime> tempo)
    {
        Console.WriteLine("Iniciando transformação do tempo");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in tempo)
            DmTempo.Add(new DmTempo
            {
                IdTempo = Convert.ToInt16(item.Year.ToString()[2..] + item.Month),
                NmMes = NomeMes(item.Month),
                NmMesano = item.Month + "/" + item.Year,
                NuAno = item.Year,
                NuDia = item.Day,
                NuAnomes = item.Year * 100 + item.Month,
                NuMes = item.Month,
                SgMes = NomeMes(item.Month)[..3]
            });

        sw.Stop();

        Console.WriteLine("Finalizando transformação do tempo" +
                          $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void TransformarClientes(List<Clientes> clientes)
    {
        Console.WriteLine("Iniciando transformação dos clientes");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in clientes)
            DmClientes.Add(new DmClientes
            {
                IdCliente = item.CodCli,
                NomeCliente = item.NomCli,
                CidadeCli = "Aracaju",
                UfCli = "SE"
            });

        sw.Stop();

        Console.WriteLine("Finalizando transformação dos clientes" +
                          $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void TransformarFornecedores(List<Fornecedores> fornecedores)
    {
        Console.WriteLine("Iniciando transformação dos fornecedores");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in fornecedores)
            DmFornecedores.Add(new DmFornecedores
            {
                IdForn = item.CodForn,
                NomForn = item.NomForn,
                RegiaoForn = "Nordeste"
            });

        sw.Stop();

        Console.WriteLine("Finalizando transformação dos fornecedores" +
                          $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void TransformarProdutos(List<Produtos> produtos)
    {
        Console.WriteLine("Iniciando transformação dos produtos");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in produtos)
            DmProdutos.Add(new DmProdutos
            {
                IdProd = item.CodProd,
                ClasseProd = ClasseProduto(item.PrecoPro),
                DscProd = item.DscProd
            });

        sw.Stop();

        Console.WriteLine("Finalizando transformação dos produtos" +
                          $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void TransformarTiposVendas(List<Pedidos> pedidos)
    {
        Console.WriteLine("Iniciando transformação dos tipos de vendas");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var item in pedidos)
            DmTiposVendas.Add(new DmTiposVendas
            {
                IdTipoVenda = item.NumPed,
                DescTipoVenda = item.ValAPrazo == 0 ? "A vista" : "Parcelado"
            });
        sw.Stop();

        Console.WriteLine("Finalizando transformação dos tipos de vendas" +
                          $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void TransformarFtVendas(List<Pedidos> pedidos)
    {
        Console.WriteLine("Iniciando transformação das vendas");
        var sw = new Stopwatch();
        sw.Start();

        foreach (var pedido in pedidos)
            foreach (var item in pedido.ItensDePedido)
                FtVendas.Add(new FtVendas
                {
                    IdProd = item.CodProd,
                    IdTipoVenda = pedido.NumPed,
                    IdForn = item.CodProdNavigation.CodForn,
                    IdTempo = Convert.ToInt16(pedido.DatPed.Year.ToString()[2..] + pedido.DatPed.Month),
                    ValorVenda = pedido.ItensDePedido.Sum(x => x.PrecoPro)
                });

        sw.Stop();

        Console.WriteLine("Finalizando transformação das vendas" +
                          $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private void TransformarFtInadimplencia(List<Pedidos> pedidos)
    {
        Console.WriteLine("Iniciando transformação das vendas");
        var sw = new Stopwatch();
        sw.Start();
        foreach (var pedido in pedidos)
            foreach (var item in pedido.Parcelas.Where(x => x.ParcPaga.Equals("F")))
                FtImpontualidade.Add(new FtImpontualidade()
                {
                    IdTempo = Convert.ToInt16(pedido.DatPed.Year.ToString()[2..] + pedido.DatPed.Month),
                    ValorParcAtrasadas = item.ValParc,
                    IdCliente = pedido.CodCli,
                    ValorParcTotal = item.ValParc
                });

        sw.Stop();

        Console.WriteLine("Finalizando transformação das vendas" +
                          $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
    }

    private static string NomeMes(int mes)
    {
        return mes switch
        {
            1 => "Janeiro",
            2 => "Fevereiro",
            3 => "Março",
            4 => "Abril",
            5 => "Maio",
            6 => "Junho",
            7 => "Julho",
            8 => "Agosto",
            9 => "Setembro",
            10 => "Outubro",
            11 => "Novembro",
            _ => "Dezembro"
        };
    }

    private static string ClasseProduto(decimal preco)
    {
        return preco switch
        {
            <= 200 => "Populares",
            <= 1000 => "Média",
            _ => "Alta linha"
        };
    }
}