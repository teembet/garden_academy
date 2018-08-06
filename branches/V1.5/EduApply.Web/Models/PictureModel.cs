using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduApply.Web.Models
{
    public class PictureModel
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public long ApplicationId { get; set; }
    }
}