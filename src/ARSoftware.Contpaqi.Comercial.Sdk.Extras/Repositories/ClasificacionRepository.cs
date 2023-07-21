﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Repositories;
using ARSoftware.Contpaqi.Comercial.Sdk.Excepciones;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Constants;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace ARSoftware.Contpaqi.Comercial.Sdk.Extras.Repositories;

/// <summary>
///     Repositorio de SDK para consultar clasificaciones.
/// </summary>
/// <typeparam name="T">
///     El tipo de clasificación utilizado por el repositorio.
/// </typeparam>
public class ClasificacionRepository<T> : IClasificacionRepository<T> where T : class, new()
{
    private readonly IContpaqiSdk _sdk;

    public ClasificacionRepository(IContpaqiSdk sdk)
    {
        _sdk = sdk;
    }

    /// <inheritdoc />
    public T BuscarPorId(int idClasificacion)
    {
        return _sdk.fBuscaIdClasificacion(idClasificacion) == SdkResultConstants.Success ? LeerDatosClasificacionActual() : null;
    }

    /// <inheritdoc />
    public T BuscarPorTipoYNumero(TipoClasificacion tipo, NumeroClasificacion numero)
    {
        return _sdk.fBuscaClasificacion((int)tipo, (int)numero) == SdkResultConstants.Success ? LeerDatosClasificacionActual() : null;
    }

    /// <inheritdoc />
    public IEnumerable<T> TraerPorTipo(TipoClasificacion tipo)
    {
        yield return BuscarPorTipoYNumero(tipo, NumeroClasificacion.Uno);
        yield return BuscarPorTipoYNumero(tipo, NumeroClasificacion.Dos);
        yield return BuscarPorTipoYNumero(tipo, NumeroClasificacion.Tres);
        yield return BuscarPorTipoYNumero(tipo, NumeroClasificacion.Cuatro);
        yield return BuscarPorTipoYNumero(tipo, NumeroClasificacion.Cinco);
        yield return BuscarPorTipoYNumero(tipo, NumeroClasificacion.Seis);
    }

    /// <inheritdoc />
    public IEnumerable<T> TraerTodo()
    {
        _sdk.fPosPrimerClasificacion().ToResultadoSdk(_sdk).ThrowIfError();
        yield return LeerDatosClasificacionActual();

        while (_sdk.fPosSiguienteClasificacion() == SdkResultConstants.Success)
        {
            yield return LeerDatosClasificacionActual();
            if (_sdk.fPosEOFClasificacion() == 1) break;
        }
    }

    private T LeerDatosClasificacionActual()
    {
        var clasificacion = new T();

        LeerYAsignarDatos(clasificacion);

        return clasificacion;
    }

    private void LeerYAsignarDatos(T clasificacion)
    {
        Type sqlModelType = typeof(admClasificaciones);

        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(typeof(T)))
        {
            try
            {
                if (!sqlModelType.HasProperty(propertyDescriptor.Name)) continue;

                propertyDescriptor.SetValue(clasificacion,
                    _sdk.LeeDatoClasificacion(propertyDescriptor.Name).Trim().ConvertFromSdkValueString(propertyDescriptor.PropertyType));
            }
            catch (ContpaqiSdkException e)
            {
                throw new ContpaqiSdkInvalidOperationException(
                    $"Error al leer el dato {propertyDescriptor.Name} de tipo {propertyDescriptor.PropertyType}. Error: {e.MensajeErrorSdk}",
                    e);
            }
        }
    }
}