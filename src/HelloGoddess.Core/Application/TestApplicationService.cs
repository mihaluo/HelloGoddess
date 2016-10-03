using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Core.MongoDb.Repositories;
using HelloGoddess.Infrastructure.Domain.Repositories;

namespace HelloGoddess.Core.Application
{
    public class TestApplicationService : ApplicationService
    {
        public void Insert()
        {
            IRepository<Test> repository = new MongoDbRepositoryBase<Test>(base.MongoDatabaseProvider);
            var insert = repository.Insert(new Test
            {
                Id = 111,
                Name = "test",
                Total = 222
            });
        }
    }
}