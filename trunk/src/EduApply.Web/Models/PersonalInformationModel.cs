using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class PersonalInformationModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Required]
        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }
        [Required(ErrorMessage = "Select State of Residence")]
        [Display(Name = "State of Residence")]
        public string StateOfResidence { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Enter a valid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 11, ErrorMessage = "Phone number is less than 11 characters")]
        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){2,}$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Postal Address")]
        public string PostalAddress { get; set; }

        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int Nationality { get; set; }
        [Required(ErrorMessage = "Select State of Origin")]
        [Display(Name = "State of Origin")]
        public long StateOfOrigin { get; set; }
        [Required(ErrorMessage = "Select LGA")]
        [Display(Name = "Local Government")]
        public long LocalGovernment { get; set; }

        [Required]
        public string Religion { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string NameOfNextOfkin { get; set; }
        [Required]
        [Display(Name = "Relationship")]
        public string NextOfKinRelationship { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string AddressOfNextOfkin { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Enter a valid Email Address")]
        public string EmailOfNextOfKin { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 11, ErrorMessage = "Phone number is less than 11 characters")]
        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){2,}$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Phone Number")]
        public string PhoneOfNextOfkin { get; set; }

        public IEnumerable<CountryModel> Countries { get; set; }
        public IEnumerable<StateModel> States { get; set; }
        public IEnumerable<StateModel> ResidentStates { get; set; }
        public IEnumerable<LocalGovernmentAreaModel> Lgaz { get; set; } 
     
        public string Id { get; set; }
        public long ApplicationId { get; set; }
        public virtual ApplicationModel Application { get; set; }
    }
}