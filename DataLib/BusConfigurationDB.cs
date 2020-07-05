
using System;
using BusMaster.Model;

namespace CoreHambusCommonLibrary.DataLib
{
  public class BusConfigurationDB
  {
    public long? Id { get; set; }
    public string Name { get; set; } = "";
    public long Version { get; set; }
    public BusType BusType { get; set; } = BusType.Unknown;

    public string Configuration { get; set; } = "{}";
    public override string ToString()
    {
      var bType = BusType.ToString();
      return $"{Name} - {bType}";
    }
  }
}
