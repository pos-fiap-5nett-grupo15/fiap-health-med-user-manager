using Microsoft.Extensions.Configuration;

namespace Fiap.Health.Med.User.Manager.Infrastructure.UnitOfWork
{
    public class HealthDatabase : BaseConnection, IHealthDatabase
    {
        public HealthDatabase(IConfiguration configuration) : base(configuration) { }
    }
}
