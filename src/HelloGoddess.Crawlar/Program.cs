using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Common.Util;
using HelloGoddess.Crawlar.Core;
using HelloGoddess.Crawlar.Model;

namespace HelloGoddess.Crawlar
{
    public class Program
    {
        public static void Main(string[] args)
        {

            PandaRoom pandaRoom = new PandaRoom();
            long roomId = args.Length >= 1 ? long.Parse(args[0]) : 487558;//485118;// 666666;//

            do
            {
                try
                {
                    pandaRoom.Connect(roomId);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } while (true);

            pandaRoom.Execute();
            Console.ReadKey();
        }
    }



}
