﻿using ARSoftware.Contpaqi.Comercial.Sdk.Extras;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sdk.Extras.ConsoleApp;
using Sdk.Extras.ConsoleApp.Catalogos;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(collection =>
    {
        collection.AddContpaqiComercialSdkServices();
        collection.AddSingleton<ConexionSdk>()
            .AddSingleton<EmpresaSdk>()
            .AddSingleton<ClienteSdk>()
            .AddSingleton<ProductoSdk>()
            .AddSingleton<ConceptoSdk>()
            .AddSingleton<DocumentoSdk>();
    })
    .ConfigureLogging(builder => { builder.AddSimpleConsole(options => { options.SingleLine = true; }); })
    .Build();

await host.StartAsync();

var conexionSdk = host.Services.GetRequiredService<ConexionSdk>();

try
{
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Iniciando Programa");

    // Iniciar conexion
    var sdk = host.Services.GetRequiredService<IContpaqiSdk>();

    if (sdk is FacturaElectronicaSdkExtended || sdk is AdminpaqSdkExtended)
        conexionSdk.IniciarConexion();
    else if (sdk is ComercialSdkExtended)
        conexionSdk.IniciarConexionConParametros("SUPERVISOR", "");

    // Abrir empresa
    var empresaSdk = host.Services.GetRequiredService<EmpresaSdk>();
    List<Empresa> empresas = empresaSdk.BuscarEmpresas();
    //empresaSdk.LogEmpresas(empresas);
    Empresa empresaSeleccionada = empresas.First(e => e.CNOMBREEMPRESA == "UNIVERSIDAD ROBOTICA ESPAÑOLA SA DE CV");
    conexionSdk.AbrirEmpresa(empresaSeleccionada);

    // Pruebas con SDK
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    // Cerrar empresa
    conexionSdk.CerrarEmpresa();

    // Terminar conexion
    conexionSdk.TerminarConexion();
}

await host.StopAsync();
