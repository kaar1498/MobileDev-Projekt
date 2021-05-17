using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MobileDev_Projekt.Models;
using Plugin.Connectivity;

namespace MobileDev_Projekt.Services
{
  public class RestClient
  {
    private const string Url = "https://www.Google.com";
    
    public async Task<ObservableCollection<ProgramModel>> GetStandardPrograms()
    {
      if (!await IsSiteReachableAndRunning(new Uri(Url)))
      {
        return new ObservableCollection<ProgramModel>();
      }

      await Task.Delay(500);
      return new ObservableCollection<ProgramModel>
      {
        new()
        {
          Name = "Aerobic",
          IsStandard = true,
          ExerciseModels = new ObservableCollection<ExerciseModel>
          {
            new()
            {
              Duration = 1,
              Name = "Warm-Up",
              Repetitions = 1,
              ImageModels = new ObservableCollection<ImageModel>(),
              RestDuration = 1,
              RestFrequency = 1,
            },
            new()
            {
              Duration = 1,
              Name = "Aerobic",
              Repetitions = 1,
              ImageModels = new ObservableCollection<ImageModel>(),
              RestDuration = 1,
              RestFrequency = 1,
            },
            new()
            {
              Duration = 1,
              Name = "Cool-Down",
              Repetitions = 1,
              ImageModels = new ObservableCollection<ImageModel>(),
              RestDuration = 1,
              RestFrequency = 1,
            },
          }
        },
        new()
        {
          Name = "Range of Motion",
          IsStandard = true,
        },
        new()
        {
          Name = "Stretching",
          IsStandard = true,
        },
        new()
        {
          Name = "Balancing",
          IsStandard = true,
        },
        new()
        {
          Name = "Aquatic",
          IsStandard = true,
        },
        new()
        {
          Name = "Strengthening",
          IsStandard = true,
        },
        new()
        {
          Name = "Breathing",
          IsStandard = true,
        },
        new()
        {
          Name = "Relaxation",
          IsStandard = true,
        }
      };
    }
    
    public async Task<bool> Login()
    {
      if (!await IsSiteReachableAndRunning(new Uri(Url)))
      {
        return false;
      }

      await Task.Delay(500);
      return true;
    }

    public async Task<bool> Register(string name, string username, string password,
      string email, string address, int phoneNumber)
    {
      if (!await IsSiteReachableAndRunning(new Uri(Url)))
      {
        return false;
      }

      await Task.Delay(3000);
      return true;
    }
    
    private static async Task<bool> IsSiteReachableAndRunning(Uri url)
    {
      var connectivity = CrossConnectivity.Current;
      if (!connectivity.IsConnected)
        return false;

      var reachable = await connectivity.IsRemoteReachable(url.AbsoluteUri);

      return reachable;
    }
  }
}