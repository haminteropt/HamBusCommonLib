﻿using System;
using System.Threading.Tasks;
using CoreHambusCommonLibrary.Model;
using HambusCommonLibrary;
using Microsoft.AspNetCore.SignalR.Client;

namespace CoreHambusCommonLibrary.Networking
{
  public class SigRConnection
  {
    public HubConnection? connection = null;
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
        Console.WriteLine($"Connection Lost attempting to reconnect: {error.Message}");

              // Notify users the connection was lost and the client is reconnecting.
              // Start queuing or dropping messages.

              return Task.CompletedTask;
      };

      try
      {
        await connection.StartAsync();
        connection.On<HamBusError>("ErrorReport", ErrorReport);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Environment.Exit(-1);
      }
      // this needs to go else where


      return connection;
    }

    private void ErrorReport(HamBusError error)
    {
      Console.WriteLine($"Error: {error.ErrorNum}: {error.Message}");
      Environment.Exit((int) error.ErrorNum);

    }

    private void loginRespCB(string message)
    {
      Console.WriteLine(message);
    }
    public async void sendRigState(RigState state, Action<string>? cb = null)
    {
      Console.WriteLine("Sending state");
      try
      {
        if (cb != null)
          connection.On<string>("loginResponse", cb);
        await connection.InvokeAsync("RadioStateChange", state);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}

