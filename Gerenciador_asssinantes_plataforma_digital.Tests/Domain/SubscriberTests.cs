using Gerenciador_asssinantes_plataforma_digital.Domain.Entities;
using Gerenciador_asssinantes_plataforma_digital.Domain.Enums;

namespace Gerenciador_asssinantes_plataforma_digital.Tests.Domain
{
    public class SubscriberTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Constructor_ShouldThrowException_WhenValueIsInvalid(decimal invalidValue)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Subscriber("Nome", "teste@gmail.com", DateTime.Now, invalidValue, PlanType.Basic));
        }

        [Fact]
        public void SubscriptionMonths_ShouldBeOne_WhenSubscribedToday()
        {
            // Arrange & Act
            var subscriber = new Subscriber("Name", "teste@gmail.com", DateTime.Now, 100, PlanType.Basic);

            // Assert
            Assert.Equal(1, subscriber.SubscriptionMonths);
        }

        [Fact]
        public void SubscriptionMonths_ShouldCalculateCorrectly_ForPastDate()
        {
            // Arrange
            var startDate = DateTime.Now.AddMonths(-12);
            var subscriber = new Subscriber("Nome", "teste@gmail.com", startDate, 100, PlanType.Basic);

            // Assert
            Assert.Equal(12, subscriber.SubscriptionMonths);
        }

        [Fact]
        public void Update_ShouldThrowException_WhenSubscriberIsInactive()
        {
            // Arrange
            var subscriber = new Subscriber("Nome", "teste@gmail.com", DateTime.Now, 100, PlanType.Basic);
            subscriber.Deactivate();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                subscriber.Update("Novo nome", "novo@gmail.com", 150, PlanType.Premium));
        }
    }
}