using Repository.IRepository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TagRepo : Repository<Tag>, ITagRepo
    {
        private readonly FUNewsManagementDbContext _context;
        public TagRepo(FUNewsManagementDbContext context) : base(context)
        {
        }
    }
}



