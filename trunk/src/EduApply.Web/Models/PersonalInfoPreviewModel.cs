using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class PersonalInfoPreviewModel
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


        public string Nationality { get; set; }
        public string StateOfOrigin { get; set; }
        public string LocalGovernment { get; set; }

        public string Religion { get; set; }

        public string NameOfNextOfkin { get; set; }
        public string NextOfKinRelationship { get; set; }

        public string AddressOfNextOfkin { get; set; }

        public string EmailOfNextOfKin { get; set; }
        public string PhoneOfNextOfkin { get; set; }
        public long ApplicationId { get; set; }
        public string RegNum { get; set; }
    }
}