using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class WebPayment
    {
        public string ResponseCode { get; set; }
        public string ResponseCodeDetail { get; set; }
        public string CardNumber { get; set; }
        // public virtual string RetrievalNumber { get; set; }
        public int AgencyGatewayId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Split { get; set; }

        public decimal AmountBeingPaid { get; set; }

        public string PostedData { get; set; }
        public bool Verified { get; set; }
        public bool Successful { get; set; }
        public string InvoiceNumber { get; set; }
        public string RetrievalReferenceNumber { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
