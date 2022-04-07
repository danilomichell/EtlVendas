cls
title Teste BAT Scaffold
@echo off
dotnet ef
@REM Checando se o comando anterior rodou sem erros.
set erros= %errorlevel%
IF %erros%==0 (
	dotnet fsi EtlVendas.Data/scaffold_config.fsx
)

IF NOT %erros%==0 (
	:DotTool
    set /p dotTool= Voce nao possui a ferramenta de scaffold instalada globalmente, deseja instalar? [y/n]: 
	IF "%dotTool%"=="y" (
		dotnet tool install --global dotnet-ef
		echo Ferramenta Instalada, pressione uma tecla para instalar os pacotes...
		pause >nul
		goto Install
	)
	IF "%dotTool%"=="" goto DotTool
	
	pause
)