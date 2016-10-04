using HelloGoddess.Infrastructure.Domain.Entities;
using HelloGoddess.Infrastructure.Domain.Repositories;
using MongoDB.Bson;

namespace HelloGoddess.Core.MongoDb
{
    public interface IRepository<TEntity> : IRepository<TEntity, ObjectId> where TEntity : class, IEntity<ObjectId>
    {

    }
}