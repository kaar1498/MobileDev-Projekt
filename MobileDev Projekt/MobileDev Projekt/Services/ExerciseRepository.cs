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
  public class ExerciseRepository
  {
    private ObservableCollection<ExerciseModel> _exerciseModels;
    
    public async Task<ObservableCollection<ExerciseModel>> ListAsync()
    {
      if (_exerciseModels is not null) return _exerciseModels;
      var request = new RestRequest("/exercises") {Method = Method.GET};
      var response = await App.ApiService.ExecuteAsync<IEnumerable<Exercise>>(request);
      return _exerciseModels = response.Adapt<ObservableCollection<ExerciseModel>>();
    }
    
    public async Task<bool> AddAsync(ExerciseModel model)
    {
      try
      {
        var request = new RestRequest($"/exercises") {Method = Method.POST};
        request.AddJsonBody(model.Adapt<Exercise>());
        
        var response = await App.ApiService.ExecuteAsync<Exercise>(request);
        _exerciseModels.Add(response.Adapt<ExerciseModel>());
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
        var request = new RestRequest($"/exercises/{id}") {Method = Method.DELETE};
        await App.ApiService.ExecuteAsync<Exercise>(request);
        _exerciseModels.Remove(_exerciseModels.FirstOrDefault(m => m.Id == id));
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}