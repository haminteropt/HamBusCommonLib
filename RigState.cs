using System;

namespace HambusCommonLibrary
{
  public class RigState : ICloneable
  {
    public string RigName { get; set; } = "";
    public int RigId { get; set; }
    public string? Group { get; set; }
    public long Freq { get; set; }
    public long FreqA { get; set; }
    public long FreqB { get; set; }
    public string? Mode { get; set; }
    public int Pitch { get; set; }
    public string? RigType { get; set; }
    public string? Rit { get; set; }
    public int RitOffset { get; set; }
    public string? Status { get; set; }
    public string? StatusStr { get; set; }
    public string? Split { get; set; }
    public bool Tx { get; set; }
    public string? Vfo { get; set; }
    public string? Xit { get; set; }

    public Object Clone()
    {
      return (RigState)MemberwiseClone();
    }
  }
}
