using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    public class ApiLogRepository : SqlRepository, IApiLogRepository
    {
        public ApiLogRepository(IDbContext context)
            : base(context)
        {

        }


        public void LogApiEvent(ApiLog apiLog)
        {
            this.Insert<ApiLog>(apiLog);
            this.SaveChanges();
        }


        public void LogAttemptedPayment(AttemptedPayment payment)
        {
            if (payment.TransactionReference <= 0)
            {
                this.Insert<AttemptedPayment>(payment);
            }
            this.SaveChanges();

        }


        public AttemptedPayment GetAttemptedPayment(long transactionReference)
        {
            var attemptedPayment = this.GetAll<AttemptedPayment>().FirstOrDefault(x => x.TransactionReference == transactionReference);
            return attemptedPayment;
        }


        public void UpdateAttemptedPayment(AttemptedPayment payment)
        {
            this.Update<AttemptedPayment>(payment);
            this.SaveChanges();
        }


        public AttemptedPayment GetAttemptedPaymentByAppId(long applicationId)
        {
            var attemptedPayment = this.GetAll<AttemptedPayment>().Where(x => x.ApplicationId == applicationId)
    .OrderByDescending(x => x.TransactionReference);
            return attemptedPayment.FirstOrDefault();
        }


        public IEnumerable<AttemptedPayment> GetAttemptedPaymentsForApplicant(long applicationId)
        {
            var attemptedPayments = this.GetAll<AttemptedPayment>().Where(x => x.ApplicationId == applicationId);
            return attemptedPayments.ToList();
        }
    }
}
