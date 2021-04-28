using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MobileDev_Projekt.Models;

namespace MobileDev_Projekt.Services
{
  public class ApiService
  {
    public async Task<bool> Login()
    {
      await Task.Delay(1000);
      return true;
    }

    public async Task<bool> Register(string name, string username, string password,
      string email, string address, int phoneNumber)
    {
      await Task.Delay(3000);
      return true;
    }

    public async Task<ObservableCollection<ProgramModel>> GetPrograms()
    {
      await Task.Delay(3000);
      return new ObservableCollection<ProgramModel>
      {
        new()
        {
          Name = "Aerobic",
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
        },
        new()
        {
          Name = "Stretching",
        },
        new()
        {
          Name = "Balancing",
        },
        new()
        {
          Name = "Aquatic",
        },
        new()
        {
          Name = "Strengthening",
        },
        new()
        {
          Name = "Breathing",
        },
        new()
        {
          Name = "Relaxation",
        }
      };
    }
    
  }
}