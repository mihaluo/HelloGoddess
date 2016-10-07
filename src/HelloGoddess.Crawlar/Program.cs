using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Common.Util;
using HelloGoddess.Crawlar.Core;
using HelloGoddess.Crawlar.Model;
using System.Threading;

namespace HelloGoddess.Crawlar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //long roomId = args.Length >= 1 ? long.Parse(args[0]) : 353622;//485118;// 666666;//

            //foreach (string roomId in PandaConstant.GoddessRoomIdList)
            {
                PandaRoom pandaRoom = new PandaRoom();
                do
                {
                    try
                    {
                        string roomId = PandaConstant.XXLRoomId;
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


            Console.ReadKey();
        }
    }



}
