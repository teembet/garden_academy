using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class FormTemplateSettingsModel
    {
        public long Id { get; set; }
        public int ApplicationFormId { get; set; }
        public int FormTemplateId { get; set; }
        public int MinEntry { get; set; }
        public int MaxEntry { get; set; }
    }
}