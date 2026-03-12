using Gerenciador_asssinantes_plataforma_digital.Application.DTOs;

namespace Gerenciador_asssinantes_plataforma_digital.Application.Interfaces
{
    public interface ISubscriberService
    {
        Task<Guid> CreateAsync(SubscriberRequest request);
        Task<IEnumerable<SubscriberResponse>> GetAllActiveAsync();
        Task<SubscriberResponse?> GetByIdActiveAsync(Guid id);
        Task UpdateAsync(Guid id, SubscriberRequest request);
        Task DeactivateAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
