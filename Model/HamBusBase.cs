﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace HamBusCommonCore.Model
{
  public class HamBusBase
  {
    static public long SerialNum_ { get; set; } = 1;
    private long myVar;

    public long SerialNum
    {
      get { return SerialNum_; }
    }

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
    public virtual void IncSerial()
    {
      SerialNum_++;
    }

  }
}
