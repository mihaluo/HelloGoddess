using System;

namespace HelloGoddess.Core.Dto
{
    public class DayRankDto : IntKeyDto

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