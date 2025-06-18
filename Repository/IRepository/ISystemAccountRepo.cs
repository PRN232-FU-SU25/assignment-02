using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ISystemAccountRepo : IRepository<SystemAccount>
    {
        Task<SystemAccount> GetAccountByUsername(string username);
        Task<SystemAccount> GetAccountById(short Id);
        Task<int> CountAsync();
        Task Delete(SystemAccount acc);
        Task<IQueryable<SystemAccount>> GetQueryable();
        Task<bool> HasNewsArticlesAsync(short accountId);
    }
}
