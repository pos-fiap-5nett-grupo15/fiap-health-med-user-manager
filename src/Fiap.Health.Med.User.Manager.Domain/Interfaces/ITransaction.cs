namespace Fiap.Health.Med.User.Manager.Domain.Interfaces
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
