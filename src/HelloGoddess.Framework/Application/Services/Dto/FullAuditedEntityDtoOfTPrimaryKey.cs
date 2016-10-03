using System;
using HelloGoddess.Infrastructure.Domain.Entities.Auditing;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IFullAudited{TUser}"/> interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// Is this entity deleted?
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Deleter user's Id, if this entity is deleted,
        /// </summary>
        public long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time, if this entity is deleted,
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}