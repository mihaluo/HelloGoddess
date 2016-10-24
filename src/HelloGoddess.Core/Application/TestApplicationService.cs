using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Core.MongoDb;
using HelloGoddess.Core.MongoDb.Repositories;

namespace HelloGoddess.Core.Application
{
    public class TestApplicationService : ApplicationService
    {
        public void Insert()
        {
            IRepository<Test> repository = new MongoDbRepositoryBase<Test>(MongoDatabaseProvider);
            var insert = repository.Insert(new Test
            {
                Name = "test",
                Total = 222
            });
        }

        public void TestQuery()
        {
            IRepository<FansDayRank> repository = new MongoDbRepositoryBase<FansDayRank>(MongoDatabaseProvider);
            string roomId = "473073";
            long timeStamp = 1477238400;
            string fansId = "4853524";
            repository.Get(new MongoDB.Bson.ObjectId("580dfef9fb57210001919527"));
            var dayrank = repository.FirstOrDefault(rank => rank.GoddessRoomId == roomId &&
                                                 rank.TimeStamp == timeStamp &&
                                                 rank.FansId == fansId);
        }
    }
}