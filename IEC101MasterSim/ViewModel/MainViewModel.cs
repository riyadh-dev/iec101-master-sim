using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using IEC101MasterSim.Model;
using lib60870.CS101;
using lib60870.linklayer;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;

namespace IEC101MasterSim.ViewModel;

public class MainViewModel : ViewModelBase
{
    #region variables

    private CS101Master _cS101Master;
    private Dictionary<int, string> _addressNamesDictionary;
    private int[] _highlightedList;

    private bool _running;
    public bool Running
    {
        get => _running;
        set
        {
            SetProperty(ref _running, value);
            OnPropertyChanged(nameof(NotRunning));
            StartConnectionCommand.InvokeCanExecuteChange();
            CloseConnectionCommand.InvokeCanExecuteChange();
            SendCtrlCmdCommand.InvokeCanExecuteChange();
        }
    }
    public bool NotRunning => !_running;

    private string _currentLinkLayerState;
    public string CurrentLinkLayerState
    {
        get => _currentLinkLayerState;
        set
        {
            SetProperty(ref _currentLinkLayerState, value);
            OnPropertyChanged();
        }
    }

    public SerialPort SerialPort { get; set; }
    public LinkLayerParameters LinkLayerParameters { get; set; }
    public ApplicationLayerParameters ApplicationLayerParameters { get; set; }
    public int SlaveAddress { get; set; }
    public IEC101Command IEC101Command { get; set; }

    public SnackbarMessageQueue SnackbarMessageQueue { get; }
    public string SnackbarBackgroundColor { get; set; }

    public FiltredTypes FiltredTypes { get; set; }
    public ObservableCollection<CustomInfoObj> InfoSignals { get; set; }
    public DelegateCommand StartConnectionCommand { get; }
    public DelegateCommand CloseConnectionCommand { get; }
    public DelegateCommand ClearCommand { get; }
    public DelegateCommand LoadAddressNamesCommand { get; }
    public DelegateCommand LoadHighlightedAddressesCommand { get; }
    public DelegateCommand SendCtrlCmdCommand { get; }
    public DelegateCommand SaveCurrentSignalsCommand { get; }
    #endregion

    #region IEC101 Handlers
    private void LinkLayerStateChanged(object _parameter, int _slaveAddress, LinkLayerState newState)
    {
        if (newState != LinkLayerState.AVAILABLE)
        {
            Application.Current.Dispatcher.Invoke(new(() => { Running = false; }));
        }

        CurrentLinkLayerState = newState.ToString();
    }

    private bool AsduReceivedHandler(object _parameter, int _slaveAddress, ASDU asdu)
    {
        try
        {
            for (int i = 0; i < asdu.NumberOfElements; i++)
            {
                var infoObj = asdu.GetElement(i);
                var objName = CustomInfoObj.GetInfoObjName(infoObj, _addressNamesDictionary);
                var isHighlighted = Array.Exists(_highlightedList, elem => elem == infoObj.ObjectAddress);
                var customInfoObj = CustomInfoObj.CustomInfoObjFactory(infoObj, asdu.Cot, FiltredTypes, objName);

                if (customInfoObj == null)
                {
                    continue;
                }

                Application.Current.Dispatcher.Invoke(new(() =>
                {
                    if (FiltredTypes.HS && !isHighlighted)
                    {
                        return;
                    }

                    customInfoObj.IsHighlighted = isHighlighted;
                    InfoSignals.Add(customInfoObj);
                }));
            }

            return true;
        }
        catch (Exception e)
        {
            Application.Current.Dispatcher.Invoke(new(() => { Running = false; }));
            CurrentLinkLayerState = e.Message;
            return false;
        }
    }
    #endregion

    #region CommandFuncs
    private async void StartConnection(object _)
    {
        //InfoSignals.Add(MockData.ReturnRandSignal());
        try
        {
            SerialPort.Open();
            SerialPort.DiscardInBuffer();
            SerialPort.DiscardOutBuffer();
        }
        catch (Exception e)
        {
            CurrentLinkLayerState = e.Message;
            return;
        }

        Running = true;

        _cS101Master = new CS101Master(
            SerialPort,
            LinkLayerMode.UNBALANCED,
            LinkLayerParameters,
            ApplicationLayerParameters
            )
        {
            DebugOutput = false,
            SlaveAddress = SlaveAddress
        };

        _cS101Master.SetASDUReceivedHandler(AsduReceivedHandler, null);
        _cS101Master.SetLinkLayerStateChangedHandler(LinkLayerStateChanged, null);
        _cS101Master.AddSlave(SlaveAddress);

        IEC101Command.AddMaster(_cS101Master);

        //look into it
        //CP56Time2a currentTime = new(DateTime.Now);
        //_cS101Master.SendClockSyncCommand(SlaveAddress, currentTime);

        await Task.Run(() =>
          {
              while (Running)
              {
                  Thread.Sleep(100);
                  _cS101Master.PollSingleSlave(SlaveAddress);
                  _cS101Master.Run();
                  Thread.Sleep(100);
              }
              SerialPort.Close();
          });
    }

    private void CloseConnection(object _)
    {
        Running = false;
        CurrentLinkLayerState = LinkLayerState.IDLE.ToString();

        var props = Properties.Settings.Default;

        props.SlaveAddress = SlaveAddress;

        props.Parity = SerialPort.Parity;
        props.PortName = SerialPort.PortName;
        props.BaudRate = SerialPort.BaudRate;
        props.Handshake = SerialPort.Handshake;

        props.AddressLength = LinkLayerParameters.AddressLength;

        props.SizeOfCA = ApplicationLayerParameters.SizeOfCA;
        props.SizeOfCOT = ApplicationLayerParameters.SizeOfCOT;
        props.SizeOfIOA = ApplicationLayerParameters.SizeOfIOA;
    }

    private bool CanCloseConnection(object _) => Running;

    private bool CanStartConnection(object _)
    {
        if (DateTime.Now > DateTime.Parse(Properties.Settings.Default.ET))
        {
            return false;
        }

        return !Running;
    }

    private bool CanSendCtrlCmd(object _) => Running;

    private void Clear(object _) => InfoSignals.Clear();

    private void LoadJsonAddressNames(object jsonString)
    {
        try
        {
            _addressNamesDictionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(jsonString as string);
            SnackbarMessageQueue.Enqueue("ANs Loading Succeeded");
        }
        catch
        {
            SnackbarMessageQueue.Enqueue("ANs Loaded Failed");
        }
    }

    private void LoadHighlightedAddresses(object jsonString)
    {
        try
        {
            _highlightedList = JsonConvert.DeserializeObject<int[]>(jsonString as string);
            SnackbarMessageQueue.Enqueue("HS Loading Succeeded");

        }
        catch
        {
            SnackbarMessageQueue.Enqueue("HS Loading Failed");
        }
    }

    private void SaveCurrentSignals(object filename)
    {
        try
        {
            InfoSignalsSaver.SaveFile(filename as string, InfoSignals);
            SnackbarMessageQueue.Enqueue("Signals Save Succeeded");

        }
        catch (Exception err)
        {
            SnackbarMessageQueue.Enqueue("Signals Save Failed: " + err.Message);
        }
    }
    #endregion

    public MainViewModel()
    {
        CurrentLinkLayerState = LinkLayerState.IDLE.ToString();

        var props = Properties.Settings.Default;

        InfoSignals = new();
        FiltredTypes = new();

        IEC101Command = new();

        SnackbarMessageQueue = new();

        _highlightedList = Array.Empty<int>();


        StartConnectionCommand = new(StartConnection, CanStartConnection);
        CloseConnectionCommand = new(CloseConnection, CanCloseConnection);
        LoadAddressNamesCommand = new(LoadJsonAddressNames);
        LoadHighlightedAddressesCommand = new(LoadHighlightedAddresses);
        SendCtrlCmdCommand = new(IEC101Command.SendCommand, CanSendCtrlCmd);
        SaveCurrentSignalsCommand = new(SaveCurrentSignals);
        ClearCommand = new(Clear);

        Running = false;

        SlaveAddress = props.SlaveAddress;

        SerialPort = new()
        {
            Parity = props.Parity,
            PortName = props.PortName,
            BaudRate = props.BaudRate,
            Handshake = props.Handshake
        };

        LinkLayerParameters = new()
        {
            AddressLength = props.AddressLength
        };

        ApplicationLayerParameters = new()
        {
            SizeOfCA = props.SizeOfCA,
            SizeOfCOT = props.SizeOfCOT,
            SizeOfIOA = props.SizeOfIOA
        };
    }
}
