using Microsoft.EntityFrameworkCore;
using WorkerModels.Interface.Repository;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerRepositories.Data;

namespace WorkerRepositories.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly AppDbContext _context;

        public WorkerRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Worker>> GetAll()
        {
            var result = await _context.Workers.ToListAsync();
            return result;
        }
        public async Task<Worker> Get(int id)
        {
            var result = await _context.Workers.FindAsync(id);
            return result;
        }

        public async Task<Worker> Add(WorkerAddRequest work)
        {
            var workAdded = await _context.Workers.AddAsync((Worker)work);
            await _context.SaveChangesAsync();
            return workAdded.Entity;
        }

        public async Task<Worker> Edit(Worker work)
        {
            _context.Entry(work).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return work;
        }

        public async Task<Worker> Delete(Worker work)
        {
            _context.Workers.Remove(work);
            await _context.SaveChangesAsync();
            return work;
        }

        public async Task<List<Worker>> GetByEmailWorkNumber(string email, string workNumber)
        {
            var result = await _context.Workers.Where(w=>w.CorporateEmail == email || w.WorkerNumber == workNumber).ToListAsync();
            return result;
        }

        public async Task<List<Worker>> GetLeaderName(string name)
        {
            var result = await _context.Workers.Where(w=>w.FirstName == name).ToListAsync();
            return result;
        }
    }
}
