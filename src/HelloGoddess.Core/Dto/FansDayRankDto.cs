using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Core.Dto
{
    public class FansDayRankDto : ObjectIdDto
    {

        /// <summary>
        /// 
        /// </summary>
        public string FansId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string FansName { get; set; }

        /// <summary>
        /// 献女神值
        /// </summary>
        public long ContributionGoddessValue { get; set; }

        /// <summary>
        /// 女神房间号
        /// </summary>
        public string GoddessRoomId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GoddessName { get; set; }

        /// <summary>
        ///  时间
        /// </summary>
        public long TimeStamp { get; set; }

       
    }
}
