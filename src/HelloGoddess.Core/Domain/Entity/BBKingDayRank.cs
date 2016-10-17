
using HelloGoddess.Infrastructure.Domain.Entities;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Core.Domain.Entity
{
    public class BBKingDayRank : Entity<ObjectId>
    {
        public long BBTimes { get; set; }

        public string FansId { get; set; }

        public string FansName { get; set; }

        public Identity Identity { get; set; }

        /// <summary>
        /// RoomId
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 女神名称
        /// </summary>
        public string GoddessName { get; set; }

        public string Content { get; set; }

        /// <summary>
        ///  时间
        /// </summary>
        public long TimeStamp { get; set; }

        public long LastTimeStamp { get; internal set; }
    }


    public enum Identity
    {
        /// <summary>
        /// 房管
        /// </summary>
        RoomManager = 60,

        /// <summary>
        /// 超管
        /// </summary>
        Admin = 120,

        /// <summary>
        /// 主播
        /// </summary>
        Hoster = 90
    }
}
