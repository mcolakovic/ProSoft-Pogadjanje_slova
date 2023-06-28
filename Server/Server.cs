using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private Socket serverSocket;
        private bool isRunning = false;
        private List<ClientHandler> clients = new List<ClientHandler>();
        public List<ClientHandler> Clients { get => clients; }
        public List<Slova> Slova { get => slova; set => slova = value; }

        public event EventHandler ServerObradiIgraca;
        private List<Slova> slova;



        public void Start()
        {
            if (!isRunning)
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999));
                serverSocket.Listen(5);
                isRunning = true;
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                serverSocket.Dispose();
                serverSocket = null;
                isRunning = false;
            }
        }

        public void HandleClients()
        {
            try
            {
                while (true)
                {
                    Socket clientSocket = serverSocket.Accept();
                    ClientHandler handler = new ClientHandler(clientSocket, clients);
                    Clients.Add(handler);
                    handler.OdjavljenKlijent += Handler_OdjavljenKlijent;
                    handler.PrijavljenKlijent += Handler_PrijavljenKlijent;
                    handler.ServerObradiIgru += Handler_ServerObradiIgru;
                    Thread nitKlijenta = new Thread(handler.HandleRequests);
                    nitKlijenta.IsBackground = true;
                    nitKlijenta.Start();
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine(">>>" + ex.Message);
            }
        }

        private void Handler_PrijavljenKlijent(object sender, EventArgs e)
        {
            if (clients.Count == 2)
            {
                Poruka poruka = new Poruka
                {
                    MessageText = "Igra moze da pocene",
                    IsSuccessful = true,
                    Operations = Operations.ZapocniIgru,
                    PorukaObject = new Info
                    {
                        MaxBrojPokusaja = slova.Count * 3
                    }
                };
                clients[0].Helper.Send(poruka);
                clients[1].Helper.Send(poruka);
                Stop();
            }
        }

        private void Handler_ServerObradiIgru(object sender, EventArgs e)
        {
            ServerObradiIgraca?.Invoke(sender, e);
        }

        private void Handler_OdjavljenKlijent(object sender, EventArgs e)
        {
            Clients.Remove((ClientHandler)sender);

        }
    }
}
