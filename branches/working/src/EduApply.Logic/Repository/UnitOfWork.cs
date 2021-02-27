using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope transaction;
        public void CommitTransaction()
        {
            this.transaction.Complete();
        }

        public void StartTransaction()
        {
            this.transaction = new TransactionScope();
        }

        public void Dispose()
        {
            this.transaction.Dispose();
        }
    }
}
