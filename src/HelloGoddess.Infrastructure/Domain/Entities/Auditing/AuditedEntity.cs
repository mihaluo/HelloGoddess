using HelloGoddess.Infrastructure.Domain.Entities.Auditing;

namespace HelloGoddess.Infrastructure.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class AuditedEntity : AuditedEntity<int>
    {

    }
}