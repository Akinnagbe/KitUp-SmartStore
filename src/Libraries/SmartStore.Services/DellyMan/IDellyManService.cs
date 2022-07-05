using SmartStore.Core.Domain.DellyMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Services.DellyMan
{
    public interface IDellyManService
    {
        Task<List<City>> GetCitiesAsync(string stateId);
        Task<List<State>> GetStatesAsync();
    }
}
