using HelloGoddess.Infrastructure.Domain.Entities;

namespace HelloGoddess.Infrastructure.Domain.Entities
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class Entity : Entity<int>, IEntity
    {

    }
}