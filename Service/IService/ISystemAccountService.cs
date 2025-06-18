using Repository.Models;
using Repository.Models.DTOs.Response;
using Repository.Models.DTOs.Resquest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface ISystemAccountService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<List<AccountResponse>> GetQueryable();
        Task<SystemAccount> GetAccountByUsername(string username);
        Task<AccountResponse?> GetByIdAsync(short id);
        Task<AccountResponse> CreateAsync(AccountRequest dto);
        Task<AccountResponse> UpdateAsync(short id, AccountRequest dto);
        Task<(bool Success, string Message)> DeleteAsync(short id);
    }
}
