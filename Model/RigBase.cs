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
  public abstract class RigBase
  {
  public RigBase(SigRConnection sigRConnection)
    {
      sigConnect = sigRConnection;
    }
    public RigState state = new RigState();
    public RigState prevState = new RigState();
    public int pollTimer { get; set; } = 2000;
    public RigConf? portConf;
    protected SerialPort? serialPort;
    protected SigRConnection? sigConnect = null;
    protected bool continueReadingSerialPort;

    abstract protected void initStartupState();
    public abstract void ReadSerialPortThread();
    protected void SendSerial(string str)
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
    public virtual void OpenPort(RigConf port)
    {
      portConf = port;
      StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
      Thread readThread = new Thread(ReadSerialPortThread);
      Thread pollThread = new Thread(PollRig);

      // Create a new SerialPort object with default settings.
      serialPort = new SerialPort();

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
