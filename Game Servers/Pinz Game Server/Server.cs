using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Pinz_Game_Server
{
    class Server
    {
        public static int maxPlayers { get; private set; }
        public static int port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        public static TcpListener tcpListener;

        public static void Start(int maxPlayers,int port)
        {
            Server.maxPlayers = maxPlayers;
            Server.port = port;

            Console.WriteLine($"Starting server...");
            InitiatizeServerData();

            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server started on {port}.");
        }
        private static void TCPConnectCallback(IAsyncResult result)
        {
            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Incoming connection from {client.Client.RemoteEndPoint}...");

            for(int i = 1; i <= maxPlayers; i++)
            {
                if(clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(client);
                    return;
                }
            }

            Console.WriteLine($"{client.Client.RemoteEndPoint} Failed to connect: Server full!");
        }

        private static void InitiatizeServerData()
        {
            for(int i = 1; i <= maxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }
}
