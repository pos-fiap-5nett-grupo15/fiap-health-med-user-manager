using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Fiap.Health.Med.User.Manager.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly string _connectionString;

        public DoctorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"SELECT * FROM Users.Doctors";

            return await connection.QueryAsync<Doctor>(query);
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"SELECT Id, CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email 
                      FROM Users.Doctors 
                      WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<Doctor>(query, new { Id = id });
        }

        public async Task<int> AddAsync(Doctor doctor)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"INSERT INTO Users.Doctors (CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email)
                      VALUES (@CrmNumber, @CrmUf, @Name, @HashedPassword, @MedicalSpecialty, @Email)";

            return await connection.ExecuteScalarAsync<int>(query, doctor);
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"UPDATE Users.Doctors 
                      SET CrmNumber = @CrmNumber, CrmUf = @CrmUf, Name = @Name, 
                      HashedPassword = @HashedPassword, MedicalSpecialty = @MedicalSpecialty, Email = @Email
                      WHERE Id = @Id";

            await connection.ExecuteAsync(query, doctor);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"DELETE FROM Users.Doctors WHERE Id = @Id";

            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
