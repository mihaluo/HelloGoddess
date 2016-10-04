using System;
using HelloGoddess.Infrastructure.Domain.Entities;
using MongoDB.Bson;

namespace HelloGoddess.Core.Domain.Entity
{
    public class DayRank : Entity<ObjectId>
    {
        /// <summary>
        /// 女神ID
        /// </summary>
        public int GooddessId { get; set; }

        /// <summary>
        /// RoomId
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 女神名称
        /// </summary>
        public string GoddessName { get; set; }

        /// <summary>
        /// 女神值
        /// </summary>
        public long GoddessValue { get; set; }

        /// <summary>
        ///  时间
        /// </summary>
        public long TimeStamp { get; set; }

    }
}