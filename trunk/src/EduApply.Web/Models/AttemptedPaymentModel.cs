using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduApply.Data.Entities;

namespace EduApply.Web.Models
{
    public class AttemptedPaymentModel
    {
        public long TransactionReference { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Email { get; set; }
        public string Customer { get; set; }
        public string Currency { get; set; }
        public decimal AmountDue { get; set; }
        public string Narration { get; set; }
        public long ApplicationId { get; set; }
        public bool Successful { get; set; }
        public string AgencyCode { get; set; }
        public string GatewayAgencyCode { get; set; }

        public string PhoneNumber { get; set; }
        public string Session { get; set; }
        public string Semester { get; set; }
        public string MatricNumber { get; set; }
        public string StudyType { get; set; }
        public string ProgrammeType { get; set; }
        public string Department { get; set; }

        public string Faculty { get; set; }
        public string PayeeName { get; set; }
        public string Level { get; set; }
        public List<Split> Splits { get; set; }
    }
}