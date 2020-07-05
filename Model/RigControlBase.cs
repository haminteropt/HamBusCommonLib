using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using CoreHambusCommonLibrary.Networking;
using HamBusCommmonCore;
using HambusCommonLibrary;

namespace HamBusCommonCore.Model
{
  public abstract class RigControlBase
  {
  public RigControlBase(SigRConnection sigRConnection)
    {
      sigConnect = sigRConnection;
    }
    public RigState state = new RigState();
    public RigState prevState = new RigState();

    #region connection info
    public int pollTimer { get; set; } = 500;
    public RigConf? portConf;
    protected SerialPort? serialPort;
    protected SigRConnection? sigConnect = null;
    protected bool continueReadingSerialPort;

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
          return StopBits.One;
        case "onepointfive":
          return StopBits.OnePointFive;
        case "two":
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
    public abstract long Freq { get; set; }
    public abstract long FreqA { get; set; }
    public abstract long FreqB { get; set; }
    public abstract string Mode { get; set; }
    public abstract int Pitch { get; set; }
    public abstract string? RigType { get; set; }
    public abstract string? Rit { get; set; }
    public abstract int RitOffset { get; set; }
    public abstract string? Status { get; set; }
    public abstract string? StatusStr { get; set; }
    public abstract string? Split { get; set; }
    public abstract bool Tx { get; set; }
    public abstract string? Vfo { get; set; }
    public abstract string? Xit { get; set; }

    #endregion
    public virtual void OpenPort(RigConf port)
    {
      if (serialPort != null && serialPort.IsOpen)
      {
        serialPort.Close();
      }
      else {
        if (serialPort == null) 
          serialPort = new SerialPort();
      }
        portConf = port;
      StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
      Thread readThread = new Thread(ReadSerialPortThread);
      Thread pollThread = new Thread(PollRig);



      // Allow the user to set the appropriate properties.
      serialPort.PortName = port.CommPortName;
      if (port.BaudRate != null)
        serialPort.BaudRate = (int)port.BaudRate;
      serialPort.Parity = ToParity(port.Parity!);
      serialPort.DataBits = 8;
      if (port.StopBits != null)
        serialPort.StopBits = ToStop(port.StopBits);


      serialPort.Handshake = port.Handshake == null ? Handshake.None : (Handshake)port.Handshake;

      serialPort.Open();
      continueReadingSerialPort = true;
      readThread.Start();
      pollThread.Start();
    }
  }
}
