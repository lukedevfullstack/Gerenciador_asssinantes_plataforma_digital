namespace Gerenciador_asssinantes_plataforma_digital.Application.DTOs
{
    public record SubscriberResponse(Guid Id, 
        string FullName,
        string Email, 
        int Months, 
        decimal MonthlyValue, 
        string Plan,
        bool IsActive);
}
