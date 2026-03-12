using Gerenciador_asssinantes_plataforma_digital.Domain.Entities;

namespace Gerenciador_asssinantes_plataforma_digital.Domain.Interfaces
{
    public interface ISubscriberRepository
    {
        Task<Subscriber?> GetByIdAsync(Guid id);
        Task<Subscriber?> GetByIdActiveAsync(Guid id);
        Task<IEnumerable<Subscriber>> GetAllActiveAsync();
        Task<bool> EmailExistsAsync(string email);
        Task AddAsync(Subscriber subscriber);
        Task UpdateAsync(Subscriber subscriber);
        Task DeleteAsync(Guid id);
    }
}
