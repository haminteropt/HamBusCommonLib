using CoreHambusCommonLibrary.Networking;
using Microsoft.AspNetCore.SignalR.Client;

namespace CoreHambusCommonLibrary.Model
{
  public class Bus
  {
    public  SigRConnection? sigConnect { get; set; }
    public  HubConnection? connection { get; set; }

    public string MasterHost { get; set; } = "localhost";
    public int MasterPort { get; set; } = 7300;
    public static string Name  { get; set; } = "";
  }
}
