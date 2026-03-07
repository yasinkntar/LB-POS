using LB_POS.Data.Entities;
using LB_POS.Infrastructure.Abstracts;
using LB_POS.Infrastructure.BaseInfrastructure;
using LB_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_POS.Infrastructure.Repositories
{
    public class BranchRepository : GenericRepositoryAsync<Branch>, IBranchRepository
    {
        private readonly DbSet<Branch> _branch;
        public BranchRepository(ApplicationDBContext dbContext):base(dbContext)
        {
            _branch = dbContext.Set<Branch>();
        }
        public async Task<List<Branch>> GetBranchesAsync()
        {
            return await _dbContext.Branches.ToListAsync();
        }
    }
}
