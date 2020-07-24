using System;

namespace HamBusCommonCore.Model
{
  public class SignalRCommands
  {
    public readonly static string ErrorReport = "ErrorReport";
    public readonly static string InfoPacket = "InfoPacket";
    public readonly static string LockRig = "LockRig";
    public readonly static string LockRigAck = "LockRigAck";
    public readonly static string ReceiveConfiguration = "ReceiveConfiguration";
    public readonly static string State = "state";
    public readonly static string ActiveUpdate = "ActiveUpdate";
  }
  public class SignalRGroups
  {
    public readonly static string Ui = "ui";
    public readonly static string Control = "control";
    public readonly static string Radio = "radio";

  }
}
