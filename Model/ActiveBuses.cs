using System.Collections.Generic;
using BusMaster.Model;
using HamBusCommonCore.Model;

namespace BusMaster.Services
{
  public class ActiveBuses : HamBusBase
  {
#nullable enable
    public string? Name { get; set; }
    public string? Configuration { get; set; }
    public List<string> Groups { get; set; } = new List<string>();
    public List<string> Ports { get; set; } = new List<string>();
    public bool IsActive { get; set; }
    public BusType? Type { get; set; }
#nullable disable
  }
}
