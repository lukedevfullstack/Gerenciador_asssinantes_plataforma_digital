using Gerenciador_asssinantes_plataforma_digital.Application.DTOs;
using Gerenciador_asssinantes_plataforma_digital.Application.Services;
using Gerenciador_asssinantes_plataforma_digital.Domain.Entities;
using Gerenciador_asssinantes_plataforma_digital.Domain.Enums;
using Gerenciador_asssinantes_plataforma_digital.Domain.Interfaces;
using Moq;

namespace Gerenciador_asssinantes_plataforma_digital.Tests.Application
{
    public class SubscriberServiceTests
    {
        private readonly Mock<ISubscriberRepository> _repositoryMock;
        private readonly SubscriberService _service;

        public SubscriberServiceTests()
        {
            _repositoryMock = new Mock<ISubscriberRepository>();
            _service = new SubscriberService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var request = new SubscriberRequest("usuário teste", "existe@gmail.com", DateTime.Now, 100, PlanType.Basic);
            _repositoryMock.Setup(x => x.EmailExistsAsync(request.Email)).ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CreateAsync(request));
            Assert.Equal("Email já registrado.", exception.Message);

            // Assert
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Subscriber>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenSubscriberIsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new SubscriberRequest("Novo nome", "teste@gmail.com", DateTime.Now, 50, PlanType.Basic);
            _repositoryMock.Setup(x => x.GetByIdActiveAsync(id)).ReturnsAsync((Subscriber)null!);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(id, request));
        }
    }
}
