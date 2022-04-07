using EtlVendas.Processamento;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = Configurations.Inject();
Console.Write("1 - Exlude\n" +
              "2 - Processo Etl\n" +
              "Escolhe a operação desejada: ");
var opt = Convert.ToInt32(Console.ReadLine());
serviceProvider.GetRequiredService<IProcessoEtl>().Processar(opt);

var retorno = opt == 1 ? "Excluido com sucesso" : "Processado com sucesso";

Console.WriteLine(retorno);