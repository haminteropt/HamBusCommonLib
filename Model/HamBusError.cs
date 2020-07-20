using HamBusCommonCore.Model;

namespace CoreHambusCommonLibrary.Model
{
  public enum HamBusErrorNum { NoError = 0, NoConfigure , Unknown }
  public class HamBusError : HamBusBase
  {
    public string Message { get; set; } = "something";
    public HamBusErrorNum ErrorNum { get; set;}  = HamBusErrorNum.Unknown;
  }
}
