using Microsoft.EntityFrameworkCore;
using WorkerModels.Interface.Repository;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerRepositories.Data;

namespace WorkerRepositories.Repositories
{
    public class PhonesRepository : IPhonesRepository
    {
        private readonly AppDbContext _context;

        public PhonesRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Phones>> GetAll(int idWorker)
        {
            var result = await _context.Phones.Where(p => p.IdWorker == idWorker).ToListAsync();
            return result;
        }

        public async Task Add(Phones[] phones)
        {
            await _context.Phones.AddRangeAsync(phones);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int idWorker)
        {
            var all = await _context.Phones.Where(p => p.IdWorker == idWorker).ToListAsync();
            _context.Phones.RemoveRange(all);
            await _context.SaveChangesAsync();
        }
    }
}
