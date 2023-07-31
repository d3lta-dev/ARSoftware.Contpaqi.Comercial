﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

public class FormaPago
{
    /// <summary>
    ///     01 - Efectivo
    /// </summary>
    public static readonly FormaPago _01 = new("01", "Efectivo");

    /// <summary>
    ///     02 - Cheque nominativo
    /// </summary>
    public static readonly FormaPago _02 = new("02", "Cheque nominativo");

    /// <summary>
    ///     03 - Transferencia electrónica de fondos
    /// </summary>
    public static readonly FormaPago _03 = new("03", "Transferencia electrónica de fondos");

    /// <summary>
    ///     04 - Tarjeta de crédito
    /// </summary>
    public static readonly FormaPago _04 = new("04", "Tarjeta de crédito");

    /// <summary>
    ///     05 - Monedero electrónico
    /// </summary>
    public static readonly FormaPago _05 = new("05", "Monedero electrónico");

    /// <summary>
    ///     06 - Dinero electrónico
    /// </summary>
    public static readonly FormaPago _06 = new("06", "Dinero electrónico");

    /// <summary>
    ///     08 - Vales de despensa
    /// </summary>
    public static readonly FormaPago _08 = new("08", "Vales de despensa");

    /// <summary>
    ///     12 - Dación en pago
    /// </summary>
    public static readonly FormaPago _12 = new("12", "Dación en pago");

    /// <summary>
    ///     13 - Pago por subrogación
    /// </summary>
    public static readonly FormaPago _13 = new("13", "Pago por subrogación");

    /// <summary>
    ///     14 - Pago por consignación
    /// </summary>
    public static readonly FormaPago _14 = new("14", "Pago por consignación");

    /// <summary>
    ///     15 - Condonación
    /// </summary>
    public static readonly FormaPago _15 = new("15", "Condonación");

    /// <summary>
    ///     17 - Compensación
    /// </summary>
    public static readonly FormaPago _17 = new("17", "Compensación");

    /// <summary>
    ///     23 - Novación
    /// </summary>
    public static readonly FormaPago _23 = new("23", "Novación");

    /// <summary>
    ///     24 - Confusión
    /// </summary>
    public static readonly FormaPago _24 = new("24", "Confusión");

    /// <summary>
    ///     25 - Remisión de deuda
    /// </summary>
    public static readonly FormaPago _25 = new("25", "Remisión de deuda");

    /// <summary>
    ///     26 - Prescripción o caducidad
    /// </summary>
    public static readonly FormaPago _26 = new("26", "Prescripción o caducidad");

    /// <summary>
    ///     27 - A satisfacción del acreedor
    /// </summary>
    public static readonly FormaPago _27 = new("27", "A satisfacción del acreedor");

    /// <summary>
    ///     28 - Tarjeta de débito
    /// </summary>
    public static readonly FormaPago _28 = new("28", "Tarjeta de débito");

    /// <summary>
    ///     29 - Tarjeta de servicios
    /// </summary>
    public static readonly FormaPago _29 = new("29", "Tarjeta de servicios");

    /// <summary>
    ///     30 - Aplicación de anticipos
    /// </summary>
    public static readonly FormaPago _30 = new("30", "Aplicación de anticipos");

    /// <summary>
    ///     31 - Intermediario pagos
    /// </summary>
    public static readonly FormaPago _31 = new("31", "Intermediario pagos");

    /// <summary>
    ///     99 - Por definir
    /// </summary>
    public static readonly FormaPago _99 = new("99", "Por definir");

    public FormaPago(string clave, string descripcion)
    {
        Clave = clave;
        Descripcion = descripcion;
    }

    public string Clave { get; }
    public string Descripcion { get; }

    public static FormaPago? FromClave(string clave)
    {
        return ToList().SingleOrDefault(r => r.Clave == clave);
    }

    public static FormaPago? FromDescripcion(string descripcion)
    {
        return ToList().SingleOrDefault(r => string.Equals(r.Descripcion, descripcion, StringComparison.OrdinalIgnoreCase));
    }

    public static List<FormaPago> ToList()
    {
        return typeof(FormaPago).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.FieldType == typeof(FormaPago))
            .Select(pi => (FormaPago)pi.GetValue(null)!)
            .OrderBy(p => p.Clave)
            .ToList();
    }

    public override string ToString()
    {
        return $"{Clave} - {Descripcion}";
    }
}
