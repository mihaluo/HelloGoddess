using System.Collections.Generic;

namespace HelloGoddess.Crawlar.Core
{
    public class PandaConstant
    {
        public const string RoomSockerInfoApi = "http://riven.panda.tv/chatroom/getinfo?roomid={0}";

        public const string RoomInfoApi = "http://api.m.panda.tv/ajax_search?roomid={0}";

        /*房间号 */
        /// <summary>
        /// 小小蕾
        /// </summary>
        public const string XXLRoomId = "353622";

        /// <summary>
        /// 陈海沛
        /// </summary>
        public const string CHPRoomId = "471687";

        /// <summary> 
        /// 蒋雪菲  
        /// </summary>
        public const string JXFRoomId = "472021";

        /// <summary>
        /// 谭盐盐
        /// </summary>
        public const string TXXpRoomId = "472245";

        /// <summary>
        /// 曹婉瑾
        /// </summary>
        public const string CWJRoomId = "473073";

        /// <summary>
        /// 陈姝君
        /// </summary>
        public const string CSJRoomId = "472691";

        /// <summary>
        /// 贝依霖
        /// </summary>
        public const string BYLRoomId = "472909";

        /// <summary>
        /// 李林蔚
        /// </summary>
        public const string LLWRoomId = "462053";

        /// <summary>
        /// 李元一 
        /// </summary>
        public const string LYYRoomId = "473695";

        /// <summary>
        ///主直播间 
        /// </summary>
        public const string MainRoom = "485118";

        public static readonly List<string> GoddessRoomIdList = new List<string>
        {
            //主直播间
            "485118",
            "353622",
            "471687",
            "472021",
            "472245",
            "473073",
            "472691",
            "472909",
            "462053",
            "473695"
        };

    }
}