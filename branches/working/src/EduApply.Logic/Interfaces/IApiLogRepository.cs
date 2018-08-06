using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IApiLogRepository
    {
        void LogApiEvent(ApiLog apiLog);
        void LogAttemptedPayment(AttemptedPayment payment);
        void UpdateAttemptedPayment(AttemptedPayment payment);
        AttemptedPayment GetAttemptedPayment(long transactionReference);
        AttemptedPayment GetAttemptedPaymentByAppId(long applicationId);
        IEnumerable<AttemptedPayment> GetAttemptedPaymentsForApplicant(long applicationId);
    }
}
