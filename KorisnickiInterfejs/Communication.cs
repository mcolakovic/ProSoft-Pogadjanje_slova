using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs
{
    public class Communication
    {
        private static Communication instance;
        private Socket socket;
        private CommunicationHelper helper;

        private Communication()
        {
        }

        public static Communication Instance
        {
            get
            {
                if (instance == null) instance = new Communication();
                return instance;
            }
        }

        public void Connect()
        {
            try
            {
                if (socket == null || !socket.Connected)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect("127.0.0.1", 9999);
                    helper = new CommunicationHelper(socket);
                }
            }
            catch (SocketException)
            {
                throw;
            }

        }

        public void CloseConnestion()
        {
            try
            {
                Poruka request = new Poruka { Operations = Operations.EndCommunication };
                helper.Send(request);
                socket.Shutdown(SocketShutdown.Both);
                socket.Dispose();
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        internal void SendMessage<T>(T m) where T : class
        {
            try
            {
                helper.Send<T>(m);
            }
            catch (Exception)
            {

                throw;
            }

        }

        internal T ReadMessage<T>() where T : class
        {
            try
            {
                return helper.Recieve<T>();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
