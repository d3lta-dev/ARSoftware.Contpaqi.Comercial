﻿using System.Collections.Generic;
using ARSoftware.Contpaqi.Comercial.Sdk.Constantes;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace ARSoftware.Contpaqi.Comercial.Sdk.Extras.Services
{
    public class AgenteService : IAgenteService
    {
        private readonly IContpaqiSdk _sdk;

        public AgenteService(IContpaqiSdk sdk)
        {
            _sdk = sdk;
        }

        public void Actualizar(int idAgente, Dictionary<string, string> datosAgente)
        {
            _sdk.fBuscaIdAgente(idAgente).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fEditaAgente().ToResultadoSdk(_sdk).ThrowIfError();
            SetDatos(datosAgente);
            _sdk.fGuardaAgente().ToResultadoSdk(_sdk).ThrowIfError();
        }

        public void Actualizar(string codigoAgente, Dictionary<string, string> datosAgente)
        {
            _sdk.fBuscaAgente(codigoAgente).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fEditaAgente().ToResultadoSdk(_sdk).ThrowIfError();
            SetDatos(datosAgente);
            _sdk.fGuardaAgente().ToResultadoSdk(_sdk).ThrowIfError();
        }

        public int Crear(Dictionary<string, string> datosAgente)
        {
            _sdk.fInsertaAgente().ToResultadoSdk(_sdk).ThrowIfError();
            SetDatos(datosAgente);
            _sdk.fGuardaAgente().ToResultadoSdk(_sdk).ThrowIfError();
            string idAgenteDato = _sdk.LeeDatoAgente(nameof(admAgentes.CIDAGENTE), SdkConstantes.kLongId);
            return int.Parse(idAgenteDato);
        }

        public void SetDatos(Dictionary<string, string> datos)
        {
            foreach (KeyValuePair<string, string> dato in datos)
                _sdk.fSetDatoAgente(dato.Key, dato.Value).ToResultadoSdk(_sdk).ThrowIfError();
        }
    }
}
