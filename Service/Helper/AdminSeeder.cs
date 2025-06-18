using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var config = services.GetRequiredService<IConfiguration>();
            var context = services.GetRequiredService<FUNewsManagementDbContext>();

            var email = config["AdminAccount:Email"];
            var password = config["AdminAccount:Password"];
            var existing = await context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email);
            if (existing == null)
            {
                context.SystemAccounts.Add(new SystemAccount
                {
                    AccountName = "System Admin",
                    AccountEmail = email,
                    AccountPassword = password,
                    AccountRole = 0
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
