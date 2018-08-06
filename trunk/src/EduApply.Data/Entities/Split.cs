using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Split : BaseEntity<long>
    {
        public string Name { get; set; }
        public int BankId { get; set; }
        public int ApplicationFormId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }
    }
}
