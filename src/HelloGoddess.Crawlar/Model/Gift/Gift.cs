namespace HelloGoddess.Crawlar.Model.Gift
{
    public class Gift
    {
        public From from { get; set; }
        public To to { get; set; }
        public Content content { get; set; }
    }

    public class From
    {
        public string nickName { get; set; }
        public string rid { get; set; }
    }

    public class To
    {
        public string toroom { get; set; }
    }

    public class Content
    {
        public string alias { get; set; }
        public string app { get; set; }
        public string avatar { get; set; }
        public string bamboo { get; set; }
        public string[] bomb { get; set; }
        public string bomb_effect { get; set; }
        public string bomb_stage { get; set; }
        public string bomb_state { get; set; }
        public long combo { get; set; }
        public int count { get; set; }
        public string createtime { get; set; }
        public string credit { get; set; }
        public string credit_day { get; set; }
        public string desc { get; set; }
        public string effective { get; set; }
        public string exp { get; set; }
        public Ext ext { get; set; }
        public string free { get; set; }
        public string gift_status { get; set; }
        public string goddessValue { get; set; }
        public string id { get; set; }
        public string isSpecial { get; set; }
        public string name { get; set; }
        public Pic pic { get; set; }
        public decimal price { get; set; }
        public string range { get; set; }
        public string receive_count { get; set; }
        public string special { get; set; }
        public string status { get; set; }
        public string updatetime { get; set; }
        public string weeklyGoddessValue { get; set; }
    }

    public class Ext
    {
        public string type { get; set; }
        public string typeName { get; set; }
    }

    public class Pic
    {
        public Pc pc { get; set; }
    }

    public class Pc
    {
        public string chat { get; set; }
        public string _default { get; set; }
        public string effect { get; set; }
        public string ext { get; set; }
        public string m_icon { get; set; }
        public string tips { get; set; }
    }

}