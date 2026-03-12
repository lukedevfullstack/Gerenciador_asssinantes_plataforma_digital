using Gerenciador_asssinantes_plataforma_digital.Domain.Entities;
using Gerenciador_asssinantes_plataforma_digital.Domain.Enums;
using Gerenciador_asssinantes_plataforma_digital.Infrastructure;
using Gerenciador_asssinantes_plataforma_digital.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_asssinantes_plataforma_digital.Tests.Integration
{
    public class SubscriberIntegrationTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<AppDbContext> _options;

        public SubscriberIntegrationTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = new AppDbContext(_options);
            context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_ShouldPersistSubscriberInDatabase()
        {
            // ARRANGE
            using var contextAction = new AppDbContext(_options);
            var repository = new SubscriberRepository(contextAction);

            var subscriber = new Subscriber(
                "Teste de integração",
                "teste@gmail.com",
                DateTime.Now.AddDays(-1),
                100.00m,
                PlanType.Basic
            );

            // ACT
            await repository.AddAsync(subscriber);

            // ASSERT
            using var contextAssert = new AppDbContext(_options);
            var result = await contextAssert.Subscribers
                .FirstOrDefaultAsync(x => x.Email == "teste@gmail.com");

            Assert.NotNull(result);
            Assert.Equal("Teste de integração", result.FullName);
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        [Fact]
        public async Task GetByIdActiveAsync_ShouldNotReturnInactiveSubscriber()
        {
            // Arrange
            using var context = new AppDbContext(_options);
            var repository = new SubscriberRepository(context);

            var subscriber = new Subscriber("Usuário inativo", "inativo@gmail.com", DateTime.Now, 50, PlanType.Basic);
            subscriber.Deactivate();

            await context.Subscribers.AddAsync(subscriber);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdActiveAsync(subscriber.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
