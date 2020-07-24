using System.Collections.Generic;
using BusMaster.Model;
using HamBusCommonCore.Model;

namespace CoreHambusCommonLibrary.Model
{
  public class ActiveBusesModel : HamBusBase
  {
#nullable enable

    public string? Id { get; set; }

    public string? ConnectionId { get; set;}
    public string? Name { get; set; }
    public List<string> Groups { get; set; } = new List<string>();
    public bool IsActive { get; set; }
    public BusType? Type { get; set; }
#nullable disable
  }
}
