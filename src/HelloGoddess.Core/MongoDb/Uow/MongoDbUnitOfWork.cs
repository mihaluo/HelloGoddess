using System;
using HelloGoddess.Core.MongoDb.Configuration;
using HelloGoddess.Infrastructure.Domain.Uow;
using MongoDB.Driver;

namespace HelloGoddess.Core.MongoDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MongoDB.
    /// </summary>
    public class MongoDbUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets a reference to MongoDB Database.
        /// </summary>
        public IMongoDatabase Database { get; private set; }

        private readonly IMongoDbModuleConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MongoDbUnitOfWork(IMongoDbModuleConfiguration configuration)
        {
            _configuration = configuration;
            Id = Guid.NewGuid().ToString("n");
        }

        public void BeginUow()
        {
            var mongoClient = new MongoClient(_configuration.ConnectionString);
            Database = mongoClient.GetDatabase(_configuration.DatatabaseName);
        }

        public string Id { get; }
        public IUnitOfWork Outer { get; set; }
        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}