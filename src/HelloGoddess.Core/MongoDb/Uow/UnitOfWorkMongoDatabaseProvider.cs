using HelloGoddess.Infrastructure.Dependency;
using HelloGoddess.Infrastructure.Domain.Uow;
using MongoDB.Driver;

namespace HelloGoddess.Core.MongoDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMongoDatabaseProvider"/> that gets database from active unit of work.
    /// </summary>
    public class UnitOfWorkMongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        public IMongoDatabase Database => ((MongoDbUnitOfWork)_currentUnitOfWork.Current).Database;

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        public UnitOfWorkMongoDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}