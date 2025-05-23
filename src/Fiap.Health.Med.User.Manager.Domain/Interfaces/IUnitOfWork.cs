﻿using System.Transactions;

namespace Fiap.Health.Med.User.Manager.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }

        #region Transaction control methods:
        ITransaction BeginTransaction();
        ITransaction BeginTransaction(TransactionOptions transactionOptions);
        #endregion Transaction control methods.
    }
}
