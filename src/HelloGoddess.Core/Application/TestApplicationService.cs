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
    }
}