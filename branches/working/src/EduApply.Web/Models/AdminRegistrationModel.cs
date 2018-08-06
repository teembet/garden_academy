using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class AdminRegistrationModel
    {
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Required]
        [Display(Name = "Home Address")]
        [StringLength(150)]
        public string HomeAddress { get; set; }
        [Required]
        [Display(Name = "State of Residence")]
        public string StateOfResidence { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Enter a valid Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(20, MinimumLength = 11, ErrorMessage = "Phone number is less than 11 characters")]
        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){2,}$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }
        [Display(Name="Postal Address")]
        public string PostalAddress { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Nationality { get; set; }
        [Required]
        [Display(Name = "State of Origin")]
        public string StateOfOrigin { get; set; }
        [Required]
        [Display(Name = "Local Government")]
        public string LocalGovernment { get; set; }

        public string Id { get; set; }
        public string  ConfirmationCode { get; set; }
        [Required(ErrorMessage = "Please enter a Password")]
        [DataType(DataType.Password)]
        [StringLength(14,MinimumLength = 8,ErrorMessage = "Maximum length is 14 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain at least 8 characters, a lower case alphabet, an uppercase alphabet and a number")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<CountryModel> Countries { get; set; }
        public IEnumerable<StateModel> States { get; set; }
        public IEnumerable<StateModel> ResidentStates { get; set; }
        public IEnumerable<LocalGovernmentAreaModel> Lgaz { get; set; } 
    }
}