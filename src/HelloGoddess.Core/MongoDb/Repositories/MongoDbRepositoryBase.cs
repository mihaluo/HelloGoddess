using System.Linq;
using HelloGoddess.Infrastructure.Domain.Entities;
using HelloGoddess.Infrastructure.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HelloGoddess.Core.MongoDb.Repositories
{
    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, ObjectId>, IRepository<TEntity>
        where TEntity : class, IEntity<ObjectId>
    {
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public virtual IMongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        public virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        private readonly IMongoDatabaseProvider _databaseProvider;

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            //IFindFluent<TEntity, TEntity> findFluent = Collection.Find(entity => entity.Id == id);
            //return findFluent.FirstOrDefault(); //TODO: What if no entity with id?
            return null;
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            //return Collection.FindOne(query); //TODO: What if no entity with id?
            return null;
        }

        public override TEntity Insert(TEntity entity)
        {
            Collection.InsertOneAsync(entity);
            return entity;
        }
        public override TEntity Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            var updateDefinition = UpdateDefiinitionGenerate<TEntity>.Gen(entity);
            Collection.UpdateOne(filter, updateDefinition);
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public override void Delete(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            //Collection.DeleteOne(entity => entity.Id == id);
        }
    }
}