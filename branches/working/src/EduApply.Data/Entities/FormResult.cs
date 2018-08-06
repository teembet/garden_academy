using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class FormResult : BaseEntity<long>
    {
        public int? ApplicationFormId { get; set; }
        public string RegNum { get; set; }
        public string AppNum { get; set; }
        public decimal? EngScore { get; set; }
        public string Subject2 { get; set; }
        public decimal? Subject2Score { get; set; }
        public string Subject3 { get; set; }
        public decimal? Subject3Score { get; set; }
        public string Subject4 { get; set; }
        public decimal? Subject4Score { get; set; }
        public string Subject5 { get; set; }
        public decimal? Subject5Score { get; set; }
        public decimal TotalScore { get; set; }
        public IEnumerable<ApplicationForm> ApplicationForms { get; set; }
    }
}
