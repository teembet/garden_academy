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
            this.Insert<AttemptedPayment>(payment);
            this.SaveChanges();
        }


        public AttemptedPayment GetAttemptedPayment(string appNum, long payeeId)
        {
            var attemptedPayment = this.GetAll<AttemptedPayment>().FirstOrDefault(x => x.ApplicationNumber == appNum && x.PayeeId == payeeId);
            return attemptedPayment;
        }
    }
}
