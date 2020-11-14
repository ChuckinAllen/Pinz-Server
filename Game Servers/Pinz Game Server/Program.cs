using System;

namespace Pinz_Game_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Pinz Game Server";

            Server.Start(50, 9995);

            Console.ReadKey();
        }
    }
}
