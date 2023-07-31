﻿using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Sdk.Extras.ConsoleApp;

public class CrearCliente
{
    private readonly IClienteProveedorService _clienteProveedorService;

    public CrearCliente(IClienteProveedorService clienteProveedorService)
    {
        _clienteProveedorService = clienteProveedorService;
    }

    public int Crear()
    {
        var cliente = new ClienteProveedor
        {
            Codigo = "PRUEBA",
            RazonSocial = "CLIENTE DE PRUEBAS",
            Rfc = "XAXX010101000",
            Tipo = TipoCliente.ClienteProveedor,
            UsoCfdi = UsoCfdi.S01,
            RegimenFiscal = RegimenFiscal._616
        };

        cliente.DatosExtra.Add(nameof(admClientes.CTEXTOEXTRA1), "Texto Extra 1");
        cliente.DatosExtra.Add(nameof(admClientes.CEMAIL1), "email@cliente.com");

        int clienteId = _clienteProveedorService.Crear(cliente);

        return clienteId;
    }

    public void Eliminar()
    {
        var codigoCliente = "PRUEBA";

        _clienteProveedorService.Eliminar(codigoCliente);
    }
}
