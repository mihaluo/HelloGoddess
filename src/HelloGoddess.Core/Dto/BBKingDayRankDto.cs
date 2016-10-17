
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Core.Dto
{
    public class BBKingDayRankDto : ObjectIdDto
    {
        public long BBTimes { get; set; }

        public string FansId { get; set; }

        public string FansName { get; set; }

        /// <summary>
        /// RoomId
        /// </summary>
        public string RoomId { get; set; }

        public IdentityType Identity { get; set; }
        public string Content { get; set; }

        /// <summary>
        /// 女神名称
        /// </summary>
        public string GoddessName { get; set; }

        /// <summary>
        ///  时间
        /// </summary>
        public long TimeStamp { get; set; }
    }

    public enum IdentityType
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
