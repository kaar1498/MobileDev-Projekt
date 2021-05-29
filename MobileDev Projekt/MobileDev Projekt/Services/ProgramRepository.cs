using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MobileDev_Projekt.Entities;
using MobileDev_Projekt.Models;
using RestSharp;

namespace MobileDev_Projekt.Services
{
  public class ProgramRepository
  {
    private ObservableCollection<ProgramModel> _programModels;
    
    public async Task<ObservableCollection<ProgramModel>> ListAsync()
    {
      if (_programModels is not null) return _programModels;
      var request = new RestRequest("/programs") {Method = Method.GET};
      var response = await App.ApiService.ExecuteAsync<IEnumerable<Program>>(request);
      var enumerable = response as Program[] ?? response.ToArray();
      
      var models = enumerable.Adapt<ObservableCollection<ProgramModel>>();
      foreach (var model in models)
      {
        model.ExerciseModels = new ObservableCollection<ExerciseModel>(enumerable.FirstOrDefault(m => m.Id == model.Id)?.Exercises.Adapt<List<ExerciseModel>>() ?? new List<ExerciseModel>());
      }

      return _programModels = models;
    }
    
    public async Task<bool> AddAsync(ProgramModel model)
    {
      try
      {
        var program = model.Adapt<Program>();
        program.Exercises = model.ExerciseModels.Adapt<List<Exercise>>();

        var request = new RestRequest($"/programs") {Method = Method.POST};
        request.AddJsonBody(program);
        
        var response = await App.ApiService.ExecuteAsync<Program>(request);
        
        var newModel = response.Adapt<ProgramModel>();
        newModel.ExerciseModels = new ObservableCollection<ExerciseModel>(response.Exercises.Adapt<List<ExerciseModel>>());
        _programModels.Add(newModel);
        return true;
      }
      catch
      {
        return false;
      }
    }
    
    public async Task<bool> DeleteAsync(Guid? id)
    {
      try
      {
        var request = new RestRequest($"/programs/{id}") {Method = Method.DELETE};
        await App.ApiService.ExecuteAsync<Program>(request);
        _programModels.Remove(_programModels.FirstOrDefault(m => m.Id == id));
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}