using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Infrastructure.Repositories;
using System.Transactions;

namespace Fiap.Health.Med.User.Manager.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHealthDatabase _baseConnection;

        public IDoctorRepository DoctorRepository { get; }
        public IPatientRepository PatientRepository { get; }

        public UnitOfWork(IHealthDatabase database)
        {
            _baseConnection = database;
            this.DoctorRepository = new DoctorRepository(_baseConnection);
            this.PatientRepository = new PatientRepository(_baseConnection);
        }

        public ITransaction BeginTransaction()
        {
            var tx = new Transaction();
            _baseConnection.EnsureConnectionIsOpen();
            return tx;
        }

        public ITransaction BeginTransaction(TransactionOptions transactionOptions)
        {
            var tx = new Transaction(transactionOptions);
            _baseConnection.EnsureConnectionIsOpen();
            return tx;
        }

        #region IDisposible Support

        private bool _disposidedValue = false;

        void Dispose(bool disposing)
        {
            if (!_disposidedValue)
            {
                _baseConnection.Dispose();
            }
            _disposidedValue = true;
        }

        public void Dispose() => Dispose(true);

        #endregion
    }
}
