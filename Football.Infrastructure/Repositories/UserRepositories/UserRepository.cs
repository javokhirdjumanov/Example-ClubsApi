using Football.Domain.Entities;
using Football.Infrastructure.Context;

namespace Football.Infrastructure.Repositories.UserRepositories
{
    public sealed class UserRepository : GenericRepository<Users, Guid>, IUserRepositiory
    {
        public UserRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
