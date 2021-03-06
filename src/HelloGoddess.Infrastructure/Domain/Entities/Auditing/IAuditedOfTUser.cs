using HelloGoddess.Infrastructure.Domain.Entities;
using HelloGoddess.Infrastructure.Domain.Entities.Auditing;

namespace HelloGoddess.Infrastructure.Domain.Entities.Auditing
{
    /// <summary>
    /// Adds navigation properties to <see cref="IAudited"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}