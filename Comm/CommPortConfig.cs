namespace HambusCommonLibrary
{
  public class CommPortConfig
  {
    public string? DisplayName { get; set; }
    public int? BaudRate { get; set; }
    public string? PortName { get; set; }
    public string? Parity { get; set; }
    public int? DataBits { get; set; }
    public string? Handshake { get; set; }
    public string? StopBits { get; set; }
    public int? ReadTimeout { get; set; }
    public int? WriteTimeout { get; set; }
  }
}
