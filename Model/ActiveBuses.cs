using System.Collections.Generic;

namespace BusMaster.Services
{
  public class ActiveBuses
  {
    public string? Name { get; set; }
    public string? Configuration { get; set; }
    public List<string> Groups { get; set; } = new List<string>();
    public List<string> Ports { get; set; } = new List<string>();
    public bool IsActive { get; set; }
  }
}
