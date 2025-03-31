using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using Dapper;
using Fiap.Health.Med.User.Manager.Infrastructure.UnitOfWork;

namespace Fiap.Health.Med.User.Manager.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IHealthDatabase _database;

        public DoctorRepository(IHealthDatabase database)
        {
            this._database = database;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            var query = @"SELECT * FROM Users.Doctors";

            return await _database.Connection.QueryAsync<Doctor>(query);
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {

            var query = @"SELECT Id, CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email 
                      FROM Users.Doctors 
                      WHERE Id = @Id";

            return await _database.Connection.QueryFirstOrDefaultAsync<Doctor>(query, new { Id = id });
        }

        public async Task<Doctor?> GetByConcilAsync(string concilUf, int concilNumber)
        {
            var query = @"SELECT Id, CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email 
                      FROM Users.Doctors 
                      WHERE CrmNumber = @CrmNumber AND CrmUf = @CrmUf";

            return await _database.Connection.QueryFirstOrDefaultAsync<Doctor>(query, new { CrmNumber = concilNumber, CrmUf = concilUf });
        }

        public async Task<int> AddAsync(Doctor doctor)
        {

            var query = @"INSERT INTO Users.Doctors (CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email)
                      VALUES (@CrmNumber, @CrmUf, @Name, @HashedPassword, @MedicalSpecialty, @Email)";

            return await _database.Connection.ExecuteScalarAsync<int>(query, doctor);
        }

        public async Task UpdateAsync(Doctor doctor)
        {

            var query = @"UPDATE Users.Doctors 
                      SET CrmNumber = @CrmNumber, CrmUf = @CrmUf, Name = @Name, 
                      HashedPassword = @HashedPassword, MedicalSpecialty = @MedicalSpecialty, Email = @Email
                      WHERE Id = @Id";

            await _database.Connection.ExecuteAsync(query, doctor);
        }

        public async Task DeleteAsync(int id)
        {

            var query = @"DELETE FROM Users.Doctors WHERE Id = @Id";

            await _database.Connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
