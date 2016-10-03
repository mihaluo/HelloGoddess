namespace HelloGoddess.Crawlar.Model
{

    public class PandaMessage
    {
        public PandaMessageType type { get; set; }
        public int time { get; set; }
        public object data { get; set; }
    }

    public enum PandaMessageType
    {
        /// <summary>
        /// 弹幕消息 
        /// </summary>
        Nomal = 1,
        /// <summary>
        /// 竹子
        /// </summary>
        Bamboo = 206,
        /// <summary>
        /// 礼物（女神票)
        /// </summary>
        Gift = 306,
        /// <summary>
        /// 观众人数
        /// </summary>
        Audience = 207,
    }
}