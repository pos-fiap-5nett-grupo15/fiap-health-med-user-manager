using Dapper;
using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
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

        public async Task<Doctor?> GetByIdAsync(int id, CancellationToken ct)
        {

            var query = @"SELECT Id, CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email 
                      FROM Users.Doctors 
                      WHERE Id = @Id";

            return await _database.Connection.QueryFirstOrDefaultAsync<Doctor>(query, new { Id = id });
        }

        public async Task<Doctor?> GetByConcilAsync(string concilUf, int concilNumber, CancellationToken ct)
        {
            var query = @"SELECT Id, CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email 
                      FROM Users.Doctors 
                      WHERE CrmNumber = @CrmNumber AND CrmUf = @CrmUf";

            return await _database.Connection.QueryFirstOrDefaultAsync<Doctor>(query, new { CrmNumber = concilNumber, CrmUf = concilUf });
        }

        public async Task<int> AddAsync(Doctor doctor, CancellationToken ct)
        {

            var query = @"INSERT INTO Users.Doctors (CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email)
                      VALUES (@CrmNumber, @CrmUf, @Name, @HashedPassword, @MedicalSpecialty, @Email)";

            return await _database.Connection.ExecuteScalarAsync<int>(query, doctor);
        }

        public async Task UpdateAsync(Doctor doctor, CancellationToken ct)
        {

            var query = @"UPDATE Users.Doctors 
                      SET CrmNumber = @CrmNumber, CrmUf = @CrmUf, Name = @Name, 
                      HashedPassword = @HashedPassword, MedicalSpecialty = @MedicalSpecialty, Email = @Email
                      WHERE Id = @Id";

            await _database.Connection.ExecuteAsync(query, doctor);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {

            var query = @"DELETE FROM Users.Doctors WHERE Id = @Id";

            await _database.Connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<(IEnumerable<Doctor>, int)> GetByFilterAsync(string? doctorName, EMedicalSpecialty? doctorSpecialty, int? doctorConcilNumber, string? doctorCrmUf, int currentPage, int pageSize, CancellationToken ct)
        {
            var query = @"SELECT Id, CrmNumber, CrmUf, Name, HashedPassword, MedicalSpecialty, Email 
                          FROM Users.Doctors 
                          WHERE (@DoctorName IS NULL OR Name LIKE '%' + @DoctorName + '%') 
                            AND (@DoctorSpecialty IS NULL OR MedicalSpecialty = @DoctorSpecialty) 
                            AND (@DoctorConcilNumber IS NULL OR CrmNumber = @DoctorConcilNumber)
                            AND (@DoctorConcilUf IS NULL OR CrmUf LIKE @DoctorConcilUf)";

            var result = (await _database.Connection.QueryAsync<Doctor>(query, new
            {
                DoctorName = doctorName,
                DoctorSpecialty = doctorSpecialty,
                DoctorConcilNumber = doctorConcilNumber,
                DoctorConcilUf = doctorCrmUf
            })).ToList();

            return (result.Skip((currentPage - 1) * pageSize).Take(pageSize), result.Count);
        }
    }
}
