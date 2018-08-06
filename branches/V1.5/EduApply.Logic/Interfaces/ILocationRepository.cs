using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface ILocationRepository
    {
        void Save(LocalGovernmentArea lga);
        IEnumerable<Country> GetCountries();
        IEnumerable<State> GetStates();
        IEnumerable<State> GetStates(int countryId);
        IEnumerable<LocalGovernmentArea> GetLgas();
        IEnumerable<LocalGovernmentArea> GetLgas(long stateId);

        Country GetCountry(int id);
        State GetState(long id);
        LocalGovernmentArea GetLocalGovernmentArea(long id);
    }
}
