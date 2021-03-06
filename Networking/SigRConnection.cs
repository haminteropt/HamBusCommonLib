﻿using System;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Threading.Tasks;
using CoreHambusCommonLibrary.Model;
using HamBusCommonCore.Model;
using HamBusCommonStd;
using Microsoft.AspNetCore.SignalR.Client;
using Serilog;

namespace CoreHambusCommonLibrary.Networking
{
  public class SigRConnection
  {
    #region singleton
    private static SigRConnection? instance = null;
    private static readonly object padlock = new object();
    public static SigRConnection Instance
    {
      get
      {
        lock (padlock)
        {
          if (instance == null)
          {
            instance = new SigRConnection();

          }
          return instance;
        }
      }
    }
    #endregion
    public HubConnection? connection = null;
    public Subject<RigState> RigState__ { get; set; } = new Subject<RigState>();
    public ReplaySubject<HamBusError> HBErrors__ { get; set; } = new ReplaySubject<HamBusError>(1);
    public Subject<RigConf> RigConfig__ { get; set; } = new Subject<RigConf>();
    public Subject<LockModel> LockModel__ { get; set; } = new Subject<LockModel>();

    public async Task<HubConnection> StartConnection(string url)
    {

      connection = new HubConnectionBuilder()
          .WithUrl(url)
          .WithAutomaticReconnect()
          .Build();

      connection.Closed += async (error) =>
      {
        await Task.Delay(new Random().Next(0, 5) * 1000);
        await connection.StartAsync();
      };
      connection.Reconnecting += error =>
      {
        Log.Error("SigRConnection: 55: Connection Lost attempting to reconnect: {@error}", error);

        // Notify users the connection was lost and the client is reconnecting.
        // Start queuing or dropping messages.

        return Task.CompletedTask;
      };

      try
      {
        await connection.StartAsync();
        SignalHandler();
      }
      catch (Exception ex)
      {
        Log.Error("SigRConnect:71: Exception {@ex}", ex);
        Environment.Exit(-1);
      }
      return connection;
    }

    private void SignalHandler()
    {
      connection.On<HamBusError>("ErrorReport", (HamBusError error) =>
      {
        HBErrors__.OnNext(error);
        Log.Error("SigRConnection: 82: Connection Lost attempting to reconnect: {@error}", error);
      });

      connection.On<RigState>("state", (RigState state) =>
        {
          Log.Verbose("SigRConnection:86:  got state change {@state}", state);
          RigState__.OnNext(state);
        });
      connection.On<LockModel>("LockRig", (LockModel rigLock) => LockModel__.OnNext(rigLock));

      connection.On<BusConfigurationDB>("ReceiveConfiguration", (busConf) =>
      {
        var conf = JsonSerializer.Deserialize<RigConf>(busConf.Configuration);
        RigConfig__.OnNext(conf);
      });
    }


    public async void SendRigState(RigState state)
    {
      if (state.IsDirty() == false) return;
      try
      {
        await connection.InvokeAsync("RadioStateChange", state);
        state.ClearDirty();
      }
      catch (Exception ex)
      {
      
        Log.Error("SigRConnect:110: Exception {@ex}", ex);
      }
    }
  }
}

