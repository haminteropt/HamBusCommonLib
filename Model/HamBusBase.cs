using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace HamBusCommonCore.Model
{
  public class HamBusBase
  {
    public override string ToString()
    {
      var options = new JsonSerializerOptions
      {
        //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
      };

      string output = JsonSerializer.Serialize(this, options);
      Console.WriteLine($"ToString output: {output}");
      return output;
    }
  }
}
