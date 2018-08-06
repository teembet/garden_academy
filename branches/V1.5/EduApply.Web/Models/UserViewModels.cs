using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class UserViewModels
    {
    }

    public class CreateAdminModel
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Enter a valid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(20, MinimumLength = 11, ErrorMessage = "Phone number is less than 11 characters")]
        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){2,}$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Select a Role for User")]
        public string UserRole { get; set; }
        [DataType(DataType.Password)]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Password can only contain 8 - 14 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain atleast 8 characters, a lower case alphabet, an uppercase alphabet and a number")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public IEnumerable<ApplicationRole> AdminRoles { get; set; }

    }

    public class CreateApplicantModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Enter a valid Email Address")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Password can only contain 8 - 14 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain atleast 8 characters, a lower case alphabet, an uppercase alphabet and a number")]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    public class AdminUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string UserRole { get; set; }

    }

    public class RoleEditModel
    {
        public ApplicationRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}