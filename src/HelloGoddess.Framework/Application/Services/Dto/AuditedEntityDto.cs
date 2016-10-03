using System;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class AuditedEntityDto : AuditedEntityDto<int>
    {

    }
}