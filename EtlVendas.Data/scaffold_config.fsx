#r "nuget: Newtonsoft.Json"
open Newtonsoft.Json.Linq
open System
open System.IO

// aqui ficam as tabelas que vão gerar classes no projeto
let tabelas =
    [
        "DW_Vendas.FT_LOCACOES"
        "DW_Vendas.DM_ARTISTA"
        "DW_Vendas.DM_GRAVADORA"
        "DW_Vendas.DM_SOCIO"
        "DW_Vendas.DM_TEMPO"
        "DW_Vendas.DM_TITULO"
        "Vendas.GRAVADORAS"
        "Vendas.COPIAS"
        "Vendas.ARTISTAS"
        "Vendas.ITENS_LOCACOES"
        "Vendas.LOCACOES"
        "Vendas.SOCIOS"
        "Vendas.TIPOS_SOCIOS"
        "Vendas.TITULOS"
    ]

let caminho_appsettings = "EtlVendas.Processamento/appsettings.json"
let projeto_do_contexto = "EtlVendas.Data"
let nome_do_contexto = "VendasContext"
let diretorio_do_contexto = "Context"
let diretorio_das_entidades = "..\EtlVendas.Data\Domain\Entities\Relacional"
let projeto_das_entidades = "EtlVendas.Data"
let caminho_string_conexao = "$.ConnectionStrings.VendasContext" 
let driver_banco_de_dados = "Oracle.EntityFrameworkCore"

// Comandos do terminal
let restore = "dotnet restore"

let run str = 
    System.Diagnostics.Process.Start("CMD.exe","/C " + str).WaitForExit()

let addRef ref = "dotnet add " + projeto_do_contexto + " reference " + ref

let scaffold_str connection_string table_list =
    let table_str = table_list |> List.map(fun table -> " -t " + table) |> String.concat ""
    [ 
        "dotnet ef dbcontext scaffold \"" + connection_string + "\""
        "Oracle.EntityFrameworkCore"
        "-v"
        "-f"
        "--context-dir " + diretorio_do_contexto
        "-c " + nome_do_contexto
        "-o " + diretorio_das_entidades
        "--no-onconfiguring"
        "--no-pluralize"
        "--project " + projeto_do_contexto
        //table_str
    ] |> String.concat " "

let addPackage pkg = "dotnet add " + projeto_do_contexto + " package " + pkg
//

let scaffold() =
    let appSettings = JToken.Parse(File.ReadAllText(caminho_appsettings))
    let conexao = appSettings.SelectToken(caminho_string_conexao).Value<string>()
    run <| scaffold_str conexao tabelas
    run <| addRef projeto_das_entidades

let install() =
    run <| addPackage "Microsoft.EntityFrameworkCore.Design"
    run <| addPackage "Microsoft.EntityFrameworkCore.Tools"
    run <| addPackage driver_banco_de_dados
    run restore
    printf "Pacotes instalados, gerar scaffold? (y/n) "
    let resposta = Console.ReadLine()
    if resposta = "y" then 
        scaffold()
    else
        ()

let rec main () =
    printf "Deseja instalar os pacotes para geracao automatica do scaffold? (y/n) "
    let resposta = String.map (Char.ToLower) (Console.ReadLine())

    match resposta with
    | "y" -> install()
    | "n" -> scaffold()
    | _   -> main()

main()
