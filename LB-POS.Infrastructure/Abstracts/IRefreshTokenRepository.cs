using LB_POS.Data.Entities.Identity;
using LB_POS.Infrastructure.BaseInfrastructure;

namespace LB_POS.Infrastructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {

    }
}
