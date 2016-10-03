using System;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be used to send Id of an entity as response from an <see cref="IApplicationService"/> method.
    /// </summary>
    
    public class EntityResultOutput : EntityResultOutput<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityResultOutput"/> object.
        /// </summary>
        public EntityResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityResultOutput"/> object.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityResultOutput(int id)
            : base(id)
        {

        }
    }
}