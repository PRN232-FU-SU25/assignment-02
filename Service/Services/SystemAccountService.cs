using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Repository.IRepository;
using Repository.Models;
using Repository.Models.DTOs.Response;
using Repository.Models.DTOs.Resquest;
using Services.IService;
using System;

namespace Services.Services
{
    public class SystemAccountService : ISystemAccountService
    {

        private readonly ISystemAccountRepo _systemAccountRepo;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        public SystemAccountService(IJwtTokenService jwtTokenService, ISystemAccountRepo systemAccountRepo, IMapper mapper)
        {
            _systemAccountRepo = systemAccountRepo;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }
        
        public async Task<AccountResponse> CreateAsync(AccountRequest dto)
        {
            short id = (short) await _systemAccountRepo.CountAsync();
            var user = new SystemAccount
            {
                AccountId = id,
                AccountName = dto.AccountName,
                AccountEmail = dto.AccountEmail,
                AccountRole = dto.AccountRole,
                AccountPassword = dto.AccountPassword,
            };
            
            await _systemAccountRepo.AddAsync(user);
            var res = _mapper.Map<AccountResponse>(user);
            return res;
        }

        public async Task<(bool Success, string Message)> DeleteAsync(short id)
        {
            var account = await _systemAccountRepo.GetAccountById(id);
            if (account == null) return (false, "Account not found");

            if (await _systemAccountRepo.HasNewsArticlesAsync(id))
                return (false, "Cannot delete account with created articles");

            await _systemAccountRepo.Delete(account);
            return (true, "Account deleted");
        }

        public async Task<SystemAccount> GetAccountByUsername(string username)
        {
            var acc = await _systemAccountRepo.GetAccountByUsername(username);
            if(acc == null)
            {
                throw new KeyNotFoundException("user not found");
            }
            return acc;
        }

        public async Task<AccountResponse?> GetByIdAsync(short id)
        {
            var account = await _systemAccountRepo.GetAccountById(id);
            if (account == null)
            {
                throw new KeyNotFoundException("user not found");
            }
            var res = _mapper.Map<AccountResponse>(account);
            return res;
        }

        public async Task<List<AccountResponse>> GetQueryable()
        {
           
            var accs = await _systemAccountRepo.GetQueryable();
            var list = await accs.ToListAsync();
            var res = _mapper.Map<List<AccountResponse>>(list);
            return res;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _systemAccountRepo.GetAccountByUsername(request.username);


            if (user == null || user.AccountPassword != request.password)
            {
                throw new ArgumentException("Sai tài khoản hoặc mật khẩu");
            }
            var token = _jwtTokenService.GenerateToken(user);
            var acc = _mapper.Map<AccountResponse>(user);
            return new AuthResponse
            {
                AccessToken = token,
                Account = acc
            };
        }

        public async Task<AccountResponse> UpdateAsync(short id, AccountRequest dto)
        {
            var account = await _systemAccountRepo.GetAccountById(id);
            if (account == null)
            {
                throw new KeyNotFoundException("không tìm thấy tài khoản");
            }
            account.AccountEmail = dto.AccountEmail;
            account.AccountPassword = dto.AccountPassword;
            account.AccountRole = dto.AccountRole;
            account.AccountName = dto.AccountName;
            await _systemAccountRepo.UpdateAsync(account);
            var res = _mapper.Map<AccountResponse>(account);
            return res;
        }
    }
}
