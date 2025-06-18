using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SystemAccountRepo : Repository<SystemAccount>, ISystemAccountRepo
    {
        private readonly FUNewsManagementDbContext _context;
        public SystemAccountRepo(FUNewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.SystemAccounts.CountAsync();
        }

        public async Task Delete(SystemAccount acc)
        {
            _context.SystemAccounts.Remove(acc);
            await _context.SaveChangesAsync();
        }

        public async Task<SystemAccount?> GetAccountById(short Id)
        {
            return await _context.SystemAccounts.FirstOrDefaultAsync(s => s.AccountId == Id);
        }

        public async Task<SystemAccount> GetAccountByUsername(string username)
        {

            return await _context.SystemAccounts
                .FirstOrDefaultAsync(u => u.AccountEmail == username);
        }

        public async Task<IQueryable<SystemAccount>> GetQueryable()
        {
            return _context.SystemAccounts.AsQueryable();
        }

        public async  Task<bool> HasNewsArticlesAsync(short accountId)
        {
            return await _context.Set<NewsArticle>().AnyAsync(x => x.CreatedBy.AccountId == accountId);
        }
    }
}
