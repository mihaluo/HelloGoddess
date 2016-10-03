using System;

namespace HelloGoddess.Infrastructure.Events.Bus.Entities
{
    /// <summary>
    /// This type of event is used to notify just before creation of an Entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is being created</param>
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}