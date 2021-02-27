using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class PersonalInformation
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }


        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }


        public string HomeAddress { get; set; }
        public string StateOfResidence { get; set; }


        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalAddress { get; set; }


        public int Nationality { get; set; }
        public long StateOfOrigin { get; set; }
        public long LocalGovernment { get; set; }

        public string Religion { get; set; }

        public string NameOfNextOfkin { get; set; }
        public string NextOfKinRelationship { get; set; }

        public string AddressOfNextOfkin { get; set; }

        public string EmailOfNextOfKin { get; set; }
        public string PhoneOfNextOfkin { get; set; }
        public long ApplicationId { get; set; }
        public string RegNum { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<State> ResidentStates { get; set; }
        public IEnumerable<LocalGovernmentArea> Lgaz { get; set; } 
        

    }
}
