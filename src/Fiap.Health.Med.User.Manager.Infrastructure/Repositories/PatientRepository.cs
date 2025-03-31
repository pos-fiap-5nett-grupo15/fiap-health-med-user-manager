using Dapper;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Patient;
using Fiap.Health.Med.User.Manager.Infrastructure.UnitOfWork;

namespace Fiap.Health.Med.User.Manager.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IHealthDatabase _database;

        public PatientRepository(IHealthDatabase database)
        {
            this._database = database;
        }

        public async Task<(IEnumerable<Patient>, string)> GetAllAsync()
        {
            try
            {
                var query = @"SELECT * FROM Users.Patients";

                return (await _database.Connection.QueryAsync<Patient>(query), string.Empty);
            }
            catch (Exception e)
            {
                return (Enumerable.Empty<Patient>(), e.Message);
            }
        }

        public async Task<(Patient?, string)> GetByIdAsync(int id)
        {
            try
            {
                var query = @$"SELECT
                            {nameof(Patient.Id)},
                            {nameof(Patient.Name)},
                            {nameof(Patient.Email)},
                            {nameof(Patient.Document)},
                            {nameof(Patient.HashedPassword)}
                          FROM Users.Patients 
                          WHERE {nameof(Patient.Id)} = @Id";

                return (await _database.Connection.QueryFirstOrDefaultAsync<Patient>(query, new { Id = id }), string.Empty);
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public async Task<(Patient?, string)> GetByDocumentAsync(long document)
        {
            try
            {
                var query = @$"SELECT
                            {nameof(Patient.Id)},
                            {nameof(Patient.Name)},
                            {nameof(Patient.Email)},
                            {nameof(Patient.Document)},
                            {nameof(Patient.HashedPassword)}
                          FROM Users.Patients 
                          WHERE {nameof(Patient.Document)} = @Document";
                return (await _database.Connection.QueryFirstOrDefaultAsync<Patient>(query, new { Document = document }), string.Empty);
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public async Task<(bool, string)> AddAsync(Patient patient)
        {
            try
            {
                var query = $@"INSERT INTO Users.Patients
                                ({nameof(Patient.Name)},
                                {nameof(Patient.Email)},
                                {nameof(Patient.Document)},
                                {nameof(Patient.HashedPassword)})
                            VALUES (@Name, @Email, @Document, @HashedPassword)";

                return (await _database.Connection.ExecuteScalarAsync<int>(query, patient) == 0, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<(bool, string)> UpdateAsync(Patient patient)
        {
            try
            {
                var query = @$"UPDATE Users.Patients 
                                SET 
                                    {nameof(Patient.Name)} = @Name,
                                    {nameof(Patient.Email)} = @Email,
                                    {nameof(Patient.Document)} = @Document,
                                    {nameof(Patient.HashedPassword)} = @HashedPassword
                                WHERE Id = @Id";

                return (await _database.Connection.ExecuteAsync(query, patient) > 0, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<(bool, string)> DeleteAsync(int id)
        {
            try
            {
                var query = @"DELETE FROM Users.Patients WHERE Id = @Id";

                return (await _database.Connection.ExecuteAsync(query, new { Id = id }) == 0, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
