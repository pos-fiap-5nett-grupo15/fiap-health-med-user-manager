using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Infrastructure.Repositories;
using System.Transactions;

namespace Fiap.Health.Med.User.Manager.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHealthDatabase _baseConnection;
        public IDoctorRepository DoctorRepository { get; }

        public UnitOfWork(IHealthDatabase database)
        {
            this._baseConnection = database;

            this.DoctorRepository = new DoctorRepository(this._baseConnection);
        }

        public ITransaction BeginTransaction()
        {
            var tx = new Transaction();
            this._baseConnection.EnsureConnectionIsOpen();
            return tx;
        }

        public ITransaction BeginTransaction(TransactionOptions transactionOptions)
        {
            var tx = new Transaction(transactionOptions);
            this._baseConnection.EnsureConnectionIsOpen();
            return tx;
        }


        #region IDisposible Support

        private bool _disposidedValue = false;

        void Dispose(bool disposing)
        {
            if (!_disposidedValue)
            {
                this._baseConnection.Dispose();
            }
            _disposidedValue = true;
        }

        public void Dispose() => Dispose(true);

        #endregion


    }
}
