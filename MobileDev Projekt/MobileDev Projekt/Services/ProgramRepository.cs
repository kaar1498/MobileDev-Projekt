using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MobileDev_Projekt.Entities;
using MobileDev_Projekt.Models;

namespace MobileDev_Projekt.Services
{
  public class ProgramRepository
  {
    private readonly FastCastleApi _fastCastleApi;

    public ProgramRepository()
    {
      _fastCastleApi = new FastCastleApi();
    }

    public async Task<ObservableCollection<ProgramModel>> ListAsync()
    {
      ObservableCollection<ProgramModel> cache = null;
      
      if (cache is not null) return new ObservableCollection<ProgramModel>(); //TODO return cache
      
      var data = await _fastCastleApi.GetExercisePlans(App.AppUser.Id);
      var exercisePlans = data as ExercisePlan[] ?? data.ToArray();
      
      if (!exercisePlans.Any()) return new ObservableCollection<ProgramModel>();
      
      var mapped = exercisePlans.Adapt<IEnumerable<ProgramModel>>();
      //TODO Save to cache
      return new ObservableCollection<ProgramModel>(mapped);
    }

    public void Get(Guid id)
    {
    }

    public void Add()
    {
    }

    public void Update()
    {
    }

    public void Delete()
    {
    }
  }
}