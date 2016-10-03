namespace HelloGoddess.Crawlar.Model.Bamboo
{
    public class Bamboo
    {
        public From from { get; set; }
        public To to { get; set; }
        public string content { get; set; }
    }

    public class From
    {
        public string identity { get; set; }
        public string nickName { get; set; }
        public string rid { get; set; }
        public string sp_identity { get; set; }
    }

    public class To
    {
        public string toroom { get; set; }
    }

}