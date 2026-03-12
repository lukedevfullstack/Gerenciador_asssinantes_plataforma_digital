using Gerenciador_asssinantes_plataforma_digital.Domain.Entities;
using Gerenciador_asssinantes_plataforma_digital.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_asssinantes_plataforma_digital.Infrastructure.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly AppDbContext _context;

        public SubscriberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subscriber?> GetByIdAsync(Guid id)
        {
            return await _context.Subscribers.FindAsync(id);
        }

        public async Task<Subscriber?> GetByIdActiveAsync(Guid id)
        {
            return await _context.Subscribers
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);
        }

        public async Task<IEnumerable<Subscriber>> GetAllActiveAsync()
        {
            return await _context.Subscribers
                .Where(s => s.IsActive).AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Subscribers
                .AnyAsync(s => s.Email == email);
        }

        public async Task AddAsync(Subscriber subscriber)
        {
            await _context.Subscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subscriber subscriber)
        {
            _context.Subscribers.Update(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var subscriber = await GetByIdAsync(id);
            if (subscriber != null)
            {
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
            }
        }
    }
}
