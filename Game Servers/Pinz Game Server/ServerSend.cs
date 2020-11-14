using System;
using System.Collections.Generic;
using System.Text;

namespace Pinz_Game_Server
{
    class ServerSend
    {
        private static void SendTCPData(int toclient, Packet packet)
        {
            packet.WriteLength();
            Server.clients[toclient].tcp.SendData(packet);
        }

        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 0; i < Server.maxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(packet);
            }
        }
        private static void SendTCPDataToAll(int ecceptClient, Packet packet)
        {
            packet.WriteLength();
            for (int i = 0; i < Server.maxPlayers; i++)
            {
                if(i != ecceptClient)
                {
                    Server.clients[i].tcp.SendData(packet);
                }
            }
        }
        public static void Welcome(int toClient, string msg)
        {
            using (Packet packet = new Packet((int)ServerPackets.welcome))
            {
                packet.Write(msg);
                packet.Write(toClient);

                SendTCPData(toClient, packet);
            }
        }
    }
}
