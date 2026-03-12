using Gerenciador_asssinantes_plataforma_digital.Domain.Enums;

namespace Gerenciador_asssinantes_plataforma_digital.Application.DTOs
{
    public record SubscriberRequest(string FullName,
        string Email, 
        DateTime SubscriptionDate,
        decimal MonthlyValue,
        PlanType Plan);
}
