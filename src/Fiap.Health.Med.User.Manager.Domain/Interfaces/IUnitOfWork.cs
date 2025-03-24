using System.Transactions;

namespace Fiap.Health.Med.User.Manager.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorRepository DoctorRepository { get; }

        ITransaction BeginTransaction();

        ITransaction BeginTransaction(TransactionOptions transactionOptions);

    }
}
