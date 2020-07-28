using System;
using HamBusCommonCore.Model;

namespace HamBusCommmonCore

{
  public class RigState : HamBusBase, ICloneable
  {
#nullable enable

    private bool dirty;
    private string? Name_;

    public string? Name
    {
      get { return Name_; }
      set
      {
        if (!string.Equals(Name_, value))
        {
          Name_ = value;
          dirty = true;
        }
      }
    }

    private long Freq_;

    public long Freq
    {
      get { return Freq_; }
      set
      {
        if (Freq_ != value)
        {
          Freq_ = value;
          dirty = true;
        }
      }
    }

    private long _freqA;

    public long FreqA
    {
      get { return _freqA; }
      set
      {
        if (_freqA != value)
        {
          _freqA = value;
          dirty = true;
        }
      }
    }
    private long _freqB;

    public long FreqB
    {
      get { return _freqB; }
      set
      {
        if (value != _freqB)
        {
          _freqB = value;
          dirty = true;
        }
      }
    }

    private string? mode_;

    public string? Mode
    {
      get { return mode_; }
      set
      {
        if (!String.Equals(Mode, value))
          dirty = true;
        mode_ = value;

      }
    }

    private int pitch_;

    public int Pitch
    {
      get { return pitch_; }
      set
      {
        if (value == pitch_) dirty = true;
        pitch_ = value;

      }
    }

    private string? rigType_;

    public string? RigType
    {
      get { return rigType_; }
      set
      {
        if (!string.Equals(value, rigType_))
          dirty = true;
        rigType_ = value;

      }
    }

    private string? rit_;

    public string? Rit
    {
      get { return rit_; }
      set
      {
        if (!string.Equals(rit_, value)) dirty = true;
        rit_ = value;

      }
    }
    private int ritOffset_;

    public int RitOffset
    {
      get { return ritOffset_; }
      set
      {
        if (ritOffset_ != value) dirty = true;
        ritOffset_ = value;

      }
    }

    private string? status_;

    public string? Status
    {
      get { return status_; }
      set
      {
        if (!string.Equals(value, status_)) dirty = true;
        status_ = value;
      }
    }

    private string? statusStr_;

    public string? StatusStr
    {
      get { return statusStr_; }
      set
      {
        if (!string(value, statusStr_)) dirty = true;
        statusStr_ = value;
      }
    }

    private string? split_;

    public string? Split
    {
      get { return split_; }
      set
      {
        if (!string.Equals(split_, value) dirty = true;
        split_ = value;
      }
    }

    private bool tx_;

    public bool Tx
    {
      get { return tx_; }
      set
      {
        if (value == tx_) dirty = true;
        tx_ = value;
      }
    }

    private string? vfo_;

    public string? Vfo
    {
      get { return vfo_; }
      set
      {
        if (!string.Equals(value, vfo_)) dirty = true;
        vfo_ = value;
      }
    }
    private string? xit_;

    public string? Xit
    {
      get { return xit_; }
      set
      {
        if (!string.Equals(value, xit_)) dirty = true;
        xit_ = value;
      }
    }

    public bool IsDirty()
    {
      return dirty;
    }
    public void ClearDirty()
    {
      dirty = false;
    }

#nullable disable
    public Object Clone()
    {
      return (RigState)MemberwiseClone();
    }
  }
}
