using System;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    
    public abstract class FullAuditedEntityDto : FullAuditedEntityDto<int>
    {

    }
}