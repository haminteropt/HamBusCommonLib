
using System;
using BusMaster.Model;
using HamBusCommonCore.Model;

namespace CoreHambusCommonLibrary.DataLib
{
  public class BusConfigurationDB : HamBusBase
  {
    public long? Id { get; set; }
    public string Name { get; set; } = "";
    public long Version { get; set; }
    public BusType BusType { get; set; } = BusType.Unknown;

    public string Configuration { get; set; } = "{}";

  }
}
