using Gerenciador_asssinantes_plataforma_digital.Application.DTOs;
using Gerenciador_asssinantes_plataforma_digital.Application.Interfaces;
using Gerenciador_asssinantes_plataforma_digital.Domain.Entities;
using Gerenciador_asssinantes_plataforma_digital.Domain.Interfaces;

namespace Gerenciador_asssinantes_plataforma_digital.Application.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _repository;

        public SubscriberService(ISubscriberRepository repository) => _repository = repository;

        public async Task<Guid> CreateAsync(SubscriberRequest req)
        {
            if (await _repository.EmailExistsAsync(req.Email))
                throw new Exception("Email já registrado.");

            var subscriber = new Subscriber(req.FullName, req.Email, req.SubscriptionDate, req.MonthlyValue, req.Plan);
            await _repository.AddAsync(subscriber);
            return subscriber.Id;
        }

        public async Task<IEnumerable<SubscriberResponse>> GetAllActiveAsync()
        {
            var list = await _repository.GetAllActiveAsync();
            return list.Select(s => new SubscriberResponse(s.Id, s.FullName, s.Email, s.SubscriptionMonths, s.MonthlyValue, s.Plan.ToString(), s.IsActive));
        }

        public async Task<SubscriberResponse?> GetByIdActiveAsync(Guid id)
        {
            var subscriber = await _repository.GetByIdActiveAsync(id);
            if (subscriber == null) return null;
            return new SubscriberResponse(subscriber.Id, subscriber.FullName, subscriber.Email, subscriber.SubscriptionMonths, subscriber.MonthlyValue, subscriber.Plan.ToString(), subscriber.IsActive);
        }

        public async Task UpdateAsync(Guid id, SubscriberRequest req)
        {
            var subscriber = await _repository.GetByIdActiveAsync(id);
            if (subscriber == null) throw new KeyNotFoundException("Assinante ativo não encontrado.");

            subscriber.Update(req.FullName, req.Email, req.MonthlyValue, req.Plan);
            await _repository.UpdateAsync(subscriber);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var subscriber = await _repository.GetByIdAsync(id);
            if (subscriber != null)
            {
                subscriber.Deactivate();
                await _repository.UpdateAsync(subscriber);
            }
        }

        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}
