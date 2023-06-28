using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientHandler
    {
        private Socket socket;
        private List<ClientHandler> clients;
        private CommunicationHelper helper;

        public ClientHandler(Socket socket, List<ClientHandler> clients)
        {
            this.socket = socket;
            this.clients = clients;
            helper = new CommunicationHelper(socket);
        }

        public event EventHandler OdjavljenKlijent;
        public event EventHandler PrijavljenKlijent;
        public event EventHandler ServerObradiIgru;
        public User user { get; set; }
        public CommunicationHelper Helper { get => helper; }

        internal void HandleRequests()
        {
            try
            {
                Poruka poruka;
                while ((poruka = helper.Recieve<Poruka>()).Operations != Operations.EndCommunication)
                {
                    try
                    {
                        CreatePoruka(poruka);
                    }
                    catch (Exception ex)
                    {
                        poruka = new Poruka
                        {
                            IsSuccessful = false,
                            MessageText = ex.Message
                        };
                        helper.Send(poruka);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                Stop();
            }
        }

        public void Stop()
        {
            if(socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Dispose();
                socket = null;
            }
            OdjavljenKlijent?.Invoke(this, EventArgs.Empty);
        }

        private void CreatePoruka(Poruka poruka)
        {
            switch (poruka.Operations)
            {
                case Operations.Login:
                    Login((User)poruka.PorukaObject);
                    break;
                case Operations.Igra:
                    ServerObradiIgru?.Invoke(this, new MyEventArgs((Slova)poruka.PorukaObject));
                    break;
            }
        }

        private void Login(User user)
        {
            bool postoji = false;
            foreach (ClientHandler client in clients)
            {
                if(client.user != null)
                {
                    if(client.user.Email == user.Email)
                    {
                        postoji = true;
                    }
                }
            }
            Poruka poruka = new Poruka();
            if (postoji)
            {
                poruka.IsSuccessful = false;
                poruka.MessageText = "Korisnicko ime vec postoji";
                helper.Send(poruka);
            }
            else
            {
                this.user = user;
                poruka.IsSuccessful = true;
                poruka.PorukaObject = user;
                helper.Send(poruka);
                PrijavljenKlijent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
