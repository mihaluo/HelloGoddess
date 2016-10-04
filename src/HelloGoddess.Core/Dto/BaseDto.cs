using MongoDB.Bson;

namespace HelloGoddess.Core.Dto
{
    public abstract class BaseDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }

    public abstract class IntKeyDto : BaseDto<ObjectId>
    {

    }
}