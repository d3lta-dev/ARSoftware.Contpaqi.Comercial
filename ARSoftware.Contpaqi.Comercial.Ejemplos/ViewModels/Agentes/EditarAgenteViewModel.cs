﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARSoftware.Contpaqi.Comercial.Ejemplos.Messages;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace ARSoftware.Contpaqi.Comercial.Ejemplos.ViewModels.Agentes;

public class EditarAgenteViewModel : ObservableRecipient
{
    private readonly IAgenteRepository<Agente> _agenteRepository;
    private readonly IAgenteService _agenteService;
    private readonly IDialogCoordinator _dialogCoordinator;
    private Agente _agente;

    public EditarAgenteViewModel(IAgenteService agenteService,
                                 IAgenteRepository<Agente> agenteRepository,
                                 IDialogCoordinator dialogCoordinator)
    {
        _agenteService = agenteService;
        _agenteRepository = agenteRepository;
        _dialogCoordinator = dialogCoordinator;

        GuardarCommand = new AsyncRelayCommand(GudardarAsync);
        CancelarCommand = new RelayCommand(CerrarVista);
    }

    public string Title => "Editar Agente";

    public IEnumerable<TipoAgente> TiposAgente { get; } = Enum.GetValues(typeof(TipoAgente)).Cast<TipoAgente>().ToList();

    public Agente Agente
    {
        get => _agente;
        private set => SetProperty(ref _agente, value);
    }

    public IAsyncRelayCommand GuardarCommand { get; }
    public IRelayCommand CancelarCommand { get; }

    private void CargarAgente(int idAgente)
    {
        Agente = _agenteRepository.BuscarPorId(idAgente);
    }

    private void CerrarVista()
    {
        Messenger.Send(new ViewModelVisibilityChangedMessage(this, false));
    }

    private async Task GudardarAsync()
    {
        try
        {
            int agenteId = Agente.CIDAGENTE;
            Dictionary<string, string> agenteDatos = Agente.ToDatosDictionary<admAgentes>();

            if (agenteId == 0)
            {
                agenteId = _agenteService.Crear(agenteDatos);
            }
            else
            {
                _agenteService.Actualizar(agenteId, agenteDatos);
            }

            await _dialogCoordinator.ShowMessageAsync(this, "Agente Guardado", "Agente guardado exitosamente.");

            CargarAgente(agenteId);
        }
        catch (Exception e)
        {
            await _dialogCoordinator.ShowMessageAsync(this, "Error", e.ToString());
        }
    }

    public void Inicializar(int? idAgente = null)
    {
        if (idAgente is null)
        {
            Agente = new Agente { Tipo = TipoAgente.VentasCobro };
        }
        else
        {
            CargarAgente(idAgente.Value);
        }
    }
}
