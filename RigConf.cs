using System.IO.Ports;
using BusMaster.Model;

namespace HamBusCommonCore.Model
{
  public class RigConf : BusConfigBase
  {
    private static RigConf? instance = null;
    public static RigConf Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new RigConf();
        }
        return instance;
      }
    }
    private RigConf() { }
    public string? name { get; set; }
    public string? commPortName { get; set; }
    public int? baudRate { get; set; } = 4800;
    public string? parity { get; set; } = "none";
    public int? dataBits { get; set; }
    public string? stopBits { get; set; } = "one";
    public Handshake? handshake { get; set; }
    public int? readTimeout { get; set; }
    public int? writeTimeout { get; set; } = null;
  }
}
