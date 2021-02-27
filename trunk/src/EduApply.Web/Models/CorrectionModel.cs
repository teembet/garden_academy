using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class CorrectionModel
    {
        public string RegNum { get; set; }
        public string AppNum { get; set; }
        public List<SearchResult> SearchResults { get; set; }
    }
}