using MongoDB.Bson;

namespace HelloGoddess.Core.Domain.Entity
{
    public class Test : Infrastructure.Domain.Entities.Entity<ObjectId>
    {
        public string Name { get; set; }

        public int Total { get; set; } 
    }
}