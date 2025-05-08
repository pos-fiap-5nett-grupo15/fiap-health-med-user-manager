using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.CreateDoctor;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.UpdateDoctor;
using Fiap.Health.Med.User.Manager.Application.Services;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using FluentValidation;
using Moq;

namespace Fiap.Health.Med.User.Manager.UnitTests.Application
{
    public class DoctorServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<CreateDoctorInput>> _createDoctorInputValidatorMock;
        private readonly Mock<IValidator<UpdateDoctorInput>> _updateDoctorInputValidatorMock;

        public DoctorServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _createDoctorInputValidatorMock = new Mock<IValidator<CreateDoctorInput>>();
            _updateDoctorInputValidatorMock = new Mock<IValidator<UpdateDoctorInput>>();
        }

        [Fact]
        public async Task GetByFilter_WhenDoctorsExist_ShouldReturnDoctors()
        {
            // Arrange
            var doctorService = new DoctorService(_unitOfWorkMock.Object, _createDoctorInputValidatorMock.Object, _updateDoctorInputValidatorMock.Object);
            var doctors = new List<Doctor>
            {
                new Doctor { Id = 1, Name = "John Doe", CrmNumber = 12345, CrmUf = "SP" },
                new Doctor { Id = 2, Name = "Jane Doe", CrmNumber = 67890, CrmUf = "RJ" }
            };
            _unitOfWorkMock.Setup(u => u.DoctorRepository.GetByFilterAsync(It.IsAny<string>(), It.IsAny<EMedicalSpecialty?>(), It.IsAny<int?>(), It.IsAny<string>()))
                .ReturnsAsync(doctors);
            // Act
            var result = await doctorService.GetByFilterAsync(null, null, null, null);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetByFilter_WhenDoctorsDoesNotExist_ShouldFailure()
        {
            // Arrange
            var doctorService = new DoctorService(_unitOfWorkMock.Object, _createDoctorInputValidatorMock.Object, _updateDoctorInputValidatorMock.Object);
            _unitOfWorkMock.Setup(u => u.DoctorRepository.GetByFilterAsync(It.IsAny<string>(), It.IsAny<EMedicalSpecialty?>(), It.IsAny<int?>(), It.IsAny<string>()))
                .ReturnsAsync(Enumerable.Empty<Doctor>());

            // Act
            var result = await doctorService.GetByFilterAsync(null, null, null, null);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }
    }
}
