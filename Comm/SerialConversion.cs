using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace HamBusCommonCore.Comm
{
  public class SerialConversion
  {
    public  Handshake ToHandShake(string hand)
    {
      switch (hand.ToLower())
      {
        case "none":
          return Handshake.None;

        case "xonxoff":
          return Handshake.XOnXOff;

        case "rts":
          return Handshake.RequestToSend;

        // this should be DTR, but doesn't seem to be supported
        case "dtr":
          return Handshake.None;

        default:
          return Handshake.None;
      }
    }
  }
}
