using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class ApplicantsViewModel
    {
        public IEnumerable<ApplicationViewModel> SavedApplications { get; set; }
        public IEnumerable<SubmittedApplicationViewModel> SubmittedApplications { get; set; }
        public IEnumerable<ApplicationFormModel> OpenApplicationForms { get; set; }
    }
}