using HelloGoddess.Infrastructure.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Core.Domain.Entity
{
    public class BambooDayRank : Entity<ObjectId>
    {
        public long BambooValue { get; set; }

        public string FansId { get; set; }

        public string FansName { get; set; }

        /// <summary>
        /// RoomId
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 女神名称
        /// </summary>
        public string GoddessName { get; set; }

        /// <summary>
        ///  时间
        /// </summary>
        public long TimeStamp { get; set; }

    }
}
