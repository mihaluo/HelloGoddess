using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Common.Util;
using HelloGoddess.Crawlar.Core;
using HelloGoddess.Crawlar.Model;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;
using HelloGoddess.Core.Application;

namespace HelloGoddess.Crawlar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TestApplicationService test = new TestApplicationService();
            //test.TestQuery();
            //return;
            Console.WriteLine(TimeStampHelper.GetTodayTimeStampSeconds());
            Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine(DateTimeOffset.Now.ToString());
            Console.WriteLine(DateTime.Now.ToLocalTime().Hour);
            Console.WriteLine(DateTime.Now.Hour);
            //if (args.Length <= 0)
            //{
            //    Console.WriteLine("未传入参数");
            //    return;
            //}

            //string roomId = args[0];
            //if (!PandaConstant.GoddessRoomIdList.Contains(roomId))
            //{
            //    Console.WriteLine("非女神房间号");
            //    return;
            //}

            Console.WriteLine("redis ip :{0},mongo ip :{1}", IpHelper.GetIp("redis"), IpHelper.GetIp("db"));

            CheckConnection();

            foreach (string roomId in PandaConstant.GoddessRoomIdList)
            {
                PandaRoom pandaRoom = new PandaRoom();
                do
                {
                    try
                    {
                        //string roomId = PandaConstant.XXLRoomId;
                        Console.WriteLine("connecting:{0}", roomId);
                        pandaRoom.Connect(roomId);
                        Console.WriteLine("connection done,{0}", roomId);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    Thread.Sleep(200);
                } while (true);

                pandaRoom.Execute();
            }

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
            }

        }

        public static void CheckConnection()
        {
            Console.WriteLine("start check connection");
            //var redisConnectionString = "redis,connectTimeout=500,SyncTimeout=5000";
            var redisConnectionString = $"{IpHelper.GetIp("redis")}:6379,connectTimeout=500,SyncTimeout=5000";
            var connection = ConnectionMultiplexer.Connect(redisConnectionString);
            var database = connection.GetDatabase();
            var result = database.StringSet("test", Guid.NewGuid().ToString());

            string connectionString = $"mongodb://{IpHelper.GetIp("db")}:27017";
            MongoClient mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase("foo");
            var collection = mongoDatabase.GetCollection<BsonDocument>("bar");
            collection.InsertOne(new BsonDocument("test", Guid.NewGuid()));
            Console.WriteLine("end check connection");

        }
    }



}
