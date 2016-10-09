using HelloGoddess.Common.Util;
using HelloGoddess.Core.MongoDb;
using HelloGoddess.Core.MongoDb.Configuration;
using HelloGoddess.Core.MongoDb.Uow;
using HelloGoddess.Infrastructure.Domain.Uow;

namespace HelloGoddess.Core.Application
{
    public abstract class ApplicationService
    {

        private static IMongoDatabaseProvider _mongoDatabaseProvider;
        private static readonly string ConnectionString = $"mongodb://{IpHelper.GetIp("db")}:27017";
        protected static IMongoDatabaseProvider MongoDatabaseProvider
        {
            get
            {
                if (_mongoDatabaseProvider != null && _mongoDatabaseProvider.Database != null) return _mongoDatabaseProvider;

                ICurrentUnitOfWorkProvider currentUnitOfWorkProvider = new CallContextCurrentUnitOfWorkProvider();
                if (currentUnitOfWorkProvider.Current == null)
                {
                    IMongoDbModuleConfiguration mongoDbModuleConfiguration = new MongoDbModuleConfiguration
                    {
                        ConnectionString = ConnectionString,
                        DatatabaseName = "HelloGoddess"
                    };
                    var mongoDbUnitOfWork = new MongoDbUnitOfWork(mongoDbModuleConfiguration);
                    currentUnitOfWorkProvider.Current = mongoDbUnitOfWork;
                    mongoDbUnitOfWork.BeginUow();

                }
                _mongoDatabaseProvider = new UnitOfWorkMongoDatabaseProvider(currentUnitOfWorkProvider);

                return _mongoDatabaseProvider;

            }
        }
    }
}