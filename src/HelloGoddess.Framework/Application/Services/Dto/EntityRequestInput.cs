using System;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be used to send Id of an entity to an <see cref="IApplicationService"/> method.
    /// </summary>
    public class EntityRequestInput : EntityRequestInput<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityRequestInput"/> object.
        /// </summary>
        public EntityRequestInput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityRequestInput"/> object.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityRequestInput(int id)
            : base(id)
        {

        }
    }
}