using System;
using System.Collections.Generic;
using System.Text;
using CoreHambusCommonLibrary.Networking;
using Microsoft.AspNetCore.SignalR.Client;

namespace CoreHambusCommonLibrary.Model
{
  public class Bus
  {
    public  SigRConnection? sigConnect { get; set; }
    public  HubConnection? connection { get; set; }

    public string? MasterHost {get; set;}
    public int MasterPort { get; set; }
  }
}
