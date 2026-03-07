using LB_POS.Data.Entities;
using LB_POS.Infrastructure.BaseInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_POS.Infrastructure.Abstracts
{
    public interface IBranchRepository : IGenericRepositoryAsync<Branch>
    {
        Task<List<Branch>> GetBranchesAsync();
    }
}
