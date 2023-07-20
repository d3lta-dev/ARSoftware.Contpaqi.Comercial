﻿using System.Collections.Generic;
using System.Linq;
using Ardalis.Specification.EntityFrameworkCore;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Repositories;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using ARSoftware.Contpaqi.Comercial.Sql.Specifications;

namespace ARSoftware.Contpaqi.Comercial.Sql.Repositories;

public sealed class ClienteProveedorSqlRepository : RepositoryBase<admClientes>, IClienteProveedorRepository<admClientes>
{
    private readonly ContpaqiComercialEmpresaDbContext _context;

    public ClienteProveedorSqlRepository(ContpaqiComercialEmpresaDbContext context) : base(context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public admClientes BuscarPorCodigo(string codigoCliente)
    {
        return _context.admClientes.WithSpecification(new ClientePorCodigoSpecification(codigoCliente)).SingleOrDefault();
    }

    /// <inheritdoc />
    public admClientes BuscarPorId(int idCliente)
    {
        return _context.admClientes.WithSpecification(new ClientePorIdSpecification(idCliente)).SingleOrDefault();
    }

    /// <inheritdoc />
    public IEnumerable<admClientes> TraerClientes()
    {
        return _context.admClientes.WithSpecification(new ClientesSpecification()).ToList();
    }

    /// <inheritdoc />
    public IEnumerable<admClientes> TraerPorTipo(TipoCliente tipoCliente)
    {
        return _context.admClientes.WithSpecification(new ClientesPorTipoSpecification(tipoCliente)).ToList();
    }

    /// <inheritdoc />
    public IEnumerable<admClientes> TraerProveedores()
    {
        return _context.admClientes.WithSpecification(new ProveedoresSpecification()).ToList();
    }

    /// <inheritdoc />
    public IEnumerable<admClientes> TraerTodo()
    {
        return _context.admClientes.ToList();
    }
}