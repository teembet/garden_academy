using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    class LocationRepository : SqlRepository, ILocationRepository
    {

        public LocationRepository(IDbContext context)
                : base(context)
            { }
            public void Save(LocalGovernmentArea lga)
            {
                this.Insert<LocalGovernmentArea>(lga);
                this.SaveChanges();
            }


            public IEnumerable<LocalGovernmentArea> GetLgas()
            {
                var lgas = this.GetAll<LocalGovernmentArea>();
                return lgas.ToList();
            }


            public IEnumerable<Country> GetCountries()
            {
                var countries = this.GetAll<Country>();
                return countries.ToList();
            }

            public IEnumerable<State> GetStates()
            {
                var states = this.GetAll<State>();
                return states.ToList();
            }

            public IEnumerable<State> GetStates(int countryId)
            {
                var states = this.GetAll<State>().Where(s=>s.CountryId == countryId);
                return states.ToList();
            }


            public IEnumerable<LocalGovernmentArea> GetLgas(long stateId)
            {
                var lgaz = this.GetAll<LocalGovernmentArea>().Where(x => x.StateId == stateId);
                return lgaz.ToList();
            }


            public Country GetCountry(int id)
            {
                var country = this.GetAll<Country>().FirstOrDefault(x => x.Id == id);
                return country;
            }

            public State GetState(long id)
            {
                var state = this.GetAll<State>().FirstOrDefault(x => x.Id == id);
                return state;
            }

            public LocalGovernmentArea GetLocalGovernmentArea(long id)
            {
                var lga = this.GetAll<LocalGovernmentArea>().FirstOrDefault(x => x.Id == id);
                return lga;
            }
    }
}
