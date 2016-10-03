using HelloGoddess.Infrastructure.Domain.Entities.Auditing;

namespace HelloGoddess.Infrastructure.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class FullAuditedEntity : FullAuditedEntity<int>
    {

    }
}