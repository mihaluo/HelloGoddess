using System;

namespace HelloGoddess.Core.Dto
{
    public class DayRankDto
    {
        /// <summary>
        /// 女神ID
        /// </summary>
        public int GooddessId { get; set; }

        /// <summary>
        /// 女神名称
        /// </summary>
        public string GoddessName { get; set; }

        /// <summary>
        /// 女神值
        /// </summary>
        public long GoddessValue { get; set; }

        public DateTime DateTime { get; set; }
    }
}