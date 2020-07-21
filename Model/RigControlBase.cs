using System;
using System.IO.Ports;
using System.Threading;
using CoreHambusCommonLibrary.Networking;
using HamBusCommmonCore;

namespace HamBusCommonCore.Model
{
  public abstract class RigControlBase : HamBusBase
  {
    public RigControlBase()
    {
    }

    public RigState prevState = new RigState();
    public abstract void SetState(RigState state);
    private Thread? readThread;
    private Thread? pollThread;

    public SigRConnection SigRCon = SigRConnection.Instance;

    public string? Name { get; set; }
    #region connection info
    public int PollTimer { get; set; } = 500;
    public RigConf? portConf;
    protected SerialPort? serialPort;
    protected SigRConnection? sigConnect = null;
    protected bool continueReadingSerialPort;
    public RigState State { get; set; } = new RigState();

    abstract protected void initStartupState();
    public abstract void ReadSerialPortThread();
    protected virtual void SendSerial(string str)
    {
      if (serialPort == null) return;
      serialPort.Write(str);
    }
    public virtual void ClosePort()
    {
      continueReadingSerialPort = false;
    }
    protected virtual Parity ToParity(string parity)
    {
      switch (parity.ToLower())
      {
        case "none":
          return Parity.None;
        case "odd":
          return Parity.Odd;
        case "even":
          return Parity.Even;
        case "mark":
          return Parity.Mark;
        case "space":
          return Parity.Space;
      }

      return Parity.None;
    }
    protected virtual StopBits ToStop(string stop)
    {
      switch (stop.ToLower())
      {
        case "none":
          return StopBits.None;
        case "one":
        case "1":
          return StopBits.One;
        case "onepointfive":
        case "1.5":
          return StopBits.OnePointFive;
        case "two":
        case "2":
          return StopBits.Two;
        default:
          return StopBits.None;
      }
    }
    protected Handshake ToHandShake(string hand)
    {
      switch (hand.ToLower())
      {
        case "none":
          return Handshake.None;
        case "xonxoff":
          return Handshake.XOnXOff;
        case "requesttosend":
          return Handshake.RequestToSend;
        case "requesttosendxonxoff":
          return Handshake.RequestToSendXOnXOff;
        default:
          return Handshake.None;
      }
    }
    public abstract void PollRig();
    #endregion


    #region commands
    public abstract void SetFrequency(long freq);
    public abstract void SetFrequencyA(long freq);
    public abstract void SetFrequencyB(long freq);
    public abstract void SetMode(string mode);
    #endregion
    public virtual void OpenPort(RigConf port)
    {
      SigRCon.RigConfig__.Subscribe<RigConf>((conf) =>
      {

        if (serialPort != null && serialPort.IsOpen)
        {
          serialPort.Close();
          if (readThread != null)
            readThread.Abort();
          if (pollThread != null)
            pollThread.Abort();
        }
        else
        {
          if (serialPort == null)
            serialPort = new SerialPort();
        }
        portConf = port;
        StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
        readThread = new Thread(ReadSerialPortThread);
        pollThread = new Thread(PollRig);

        // Allow the user to set the appropriate properties.
        serialPort.PortName = port.commPortName;
        if (port.baudRate != null)
          serialPort.BaudRate = (int)port.baudRate;
        serialPort.Parity = ToParity(port.parity!);
        serialPort.DataBits = 8;
        if (port.stopBits != null)
          serialPort.StopBits = ToStop(port.stopBits);

        serialPort.Handshake = port.handshake == null ? Handshake.None : (Handshake)port.handshake;

        serialPort.Open();
        continueReadingSerialPort = true;
        readThread.Start();
        pollThread.Start();
      });
    }
  }
}
