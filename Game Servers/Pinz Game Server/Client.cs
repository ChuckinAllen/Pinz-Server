using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Pinz_Game_Server
{
    class Client
    {
        public static int dataBufferSize = 4096;
        public int id;
        public TCP tcp;

        public Client(int clientId)
        {
            id = clientId;
            tcp = new TCP(id);
        }
        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            //private Packet receivedData;
            private byte[] receiveBuffer;

            public TCP(int id)
            {
                this.id = id;
            }

            public void Connect(TcpClient socket)
            {
                this.socket = socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                //receivedData = new Packet();

                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                ServerSend.Welcome(id, "Welcome to Pinz"); //sends welcome packet, welcome message to the server
            }
            public void SendData(Packet packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error sending data to player {id} via TCP");
                }
            }
        }
    }

}
