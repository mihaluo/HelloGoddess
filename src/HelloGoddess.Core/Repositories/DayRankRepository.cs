using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Core.MongoDb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Core.MongoDb;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HelloGoddess.Core.Repositories
{
    public class DayRankRepository : MongoDbRepositoryBase<FansDayRank>, IRepository<FansDayRank>
    {
        public DayRankRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

    }
}
