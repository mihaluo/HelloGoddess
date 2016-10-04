using HelloGoddess.Infrastructure.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Core.Domain.Entity
{
    public class FansDayRank : Entity<ObjectId>
    {

        /// <summary>
        /// 
        /// </summary>
        public string FansId { get; set; }


        /// <summary>
        /// 女神房间号
        /// </summary>
        public string RansName { get; set; }

        /// <summary>
        /// 献女神值
        /// </summary>
        public long ContributionGoddessValue { get; set; }

        /// <summary>
        /// 女神房间号
        /// </summary>
        public string GoddessRoomId { get; set; }

        /// <summary>
        ///  时间
        /// </summary>
        public long TimeStamp { get; set; }
    }
}
