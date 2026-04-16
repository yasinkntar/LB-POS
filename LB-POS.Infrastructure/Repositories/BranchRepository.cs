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
        public BranchRepository(ApplicationDBContext dbContext):base(dbContext)
        {
        }
        public async Task<List<Branch>> GetBranchesAsync()
        {
            return await _dbContext.Branches.ToListAsync();
        }
    }
}
