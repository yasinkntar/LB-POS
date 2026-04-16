using LB_POS.Data.Entities;
using LB_POS.Infrastructure.Abstracts;
using LB_POS.Infrastructure.BaseInfrastructure;
using LB_POS.Infrastructure.Data;

namespace LB_POS.Infrastructure.Repositories
{
    public class SectionsRepository : GenericRepositoryAsync<Section>, ISectionsRepository
    {
        public SectionsRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }
    }
}
