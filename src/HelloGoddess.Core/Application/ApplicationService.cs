﻿using HelloGoddess.Core.MongoDb;
using HelloGoddess.Core.MongoDb.Configuration;
using HelloGoddess.Core.MongoDb.Uow;
using HelloGoddess.Infrastructure.Domain.Uow;

namespace HelloGoddess.Core.Application
{
    public abstract class ApplicationService
    {
        private IMongoDatabaseProvider _mongoDatabaseProvider;
        protected IMongoDatabaseProvider MongoDatabaseProvider
        {
            get
            {
                if (_mongoDatabaseProvider != null) return _mongoDatabaseProvider;

                ICurrentUnitOfWorkProvider currentUnitOfWorkProvider = new CallContextCurrentUnitOfWorkProvider();
                IMongoDbModuleConfiguration mongoDbModuleConfiguration = new MongoDbModuleConfiguration
                {
                    ConnectionString = "mongodb://10.211.55.2:27017",
                    DatatabaseName = "HelloGoddess"
                };
                var mongoDbUnitOfWork = new MongoDbUnitOfWork(mongoDbModuleConfiguration);
                currentUnitOfWorkProvider.Current = mongoDbUnitOfWork;
                mongoDbUnitOfWork.BeginUow();
                _mongoDatabaseProvider = new UnitOfWorkMongoDatabaseProvider(currentUnitOfWorkProvider);

                return _mongoDatabaseProvider;

            }
        }
    }
}