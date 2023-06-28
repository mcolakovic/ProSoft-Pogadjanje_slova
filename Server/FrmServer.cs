using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmServer : Form
    {
        Server server = new Server();
        int max;
        bool kraj = false;
        public FrmServer()
        {
            InitializeComponent();
            InitializeTabela();
            server.ServerObradiIgraca += Server_ServerObradiIgraca;
        }

        private void Server_ServerObradiIgraca(object sender, EventArgs e)
        {
            Slova p = new Slova
            {
                Slovo = ((MyEventArgs)e).slova.Slovo,
                Poeni = 0,
                Pogodio = false
            };

            foreach (Slova slova in server.Slova)
            {
                if(slova.Slovo == ((MyEventArgs)e).slova.Slovo && slova.Pogodio == false){
                    slova.Pogodio = true;
                    ((ClientHandler)sender).user.BrojPoena += slova.Poeni;
                    ((ClientHandler)sender).user.BrojPogodaka++;
                    p.Pogodio = true;
                    p.Poeni = slova.Poeni;
                    break;
                }
            }

            ((ClientHandler)sender).user.BrojPokusaja++;
            Invoke(new Action(() => dgvTabela.Rows.Add(p.Slovo, p.Poeni)));

            Poruka poruka = new Poruka
            {
                IsSuccessful = true,
                Operations = Operations.Igra,
                PorukaObject = new Igra
                {
                    PrviIgrac = server.Clients[0].user,
                    DrugiIgrac = server.Clients[1].user,
                    Slova = p
                }
            };

            if ((server.Clients[0].user.BrojPokusaja + server.Clients[1].user.BrojPokusaja) == max && isPogodak())
            {
                kraj = true;
                if (server.Clients[0].user.BrojPoena > server.Clients[1].user.BrojPoena)
                {
                    ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat = "Pobjeda";
                    ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat = "Poraz";
                }
                else if (server.Clients[0].user.BrojPoena < server.Clients[1].user.BrojPoena)
                {
                    ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat = "Poraz";
                    ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat = "Pobjeda";
                }
                else if (server.Clients[0].user.BrojPoena == server.Clients[1].user.BrojPoena)
                {
                    ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat = "Remi";
                    ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat = "Remi";
                }
            }
            else if ((server.Clients[0].user.BrojPokusaja + server.Clients[1].user.BrojPokusaja) == max)
            {
                kraj = true;
                ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat = "Poraz";
                ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat = "Poraz";
            }
            else if (isPogodak())
            {
                kraj = true;
                if (server.Clients[0].user.BrojPoena > server.Clients[1].user.BrojPoena)
                {
                    ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat = "Pobjeda";
                    ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat = "Poraz";
                }
                else if (server.Clients[0].user.BrojPoena < server.Clients[1].user.BrojPoena)
                {
                    ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat = "Poraz";
                    ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat = "Pobjeda";
                }
                else if (server.Clients[0].user.BrojPoena == server.Clients[1].user.BrojPoena)
                {
                    ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat = "Remi";
                    ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat = "Remi";
                }
            }

            Invoke(new Action(() => lblBrojPokusaja.Text = "Broj pokusaja: " + (server.Clients[0].user.BrojPokusaja + server.Clients[1].user.BrojPokusaja).ToString()));
            Invoke(new Action(() => lblIgracA.Text = server.Clients[0].user.Email + "  " + server.Clients[0].user.BrojPoena.ToString()));
            Invoke(new Action(() => lblIgracB.Text = server.Clients[1].user.Email + "  " + server.Clients[1].user.BrojPoena.ToString()));

            server.Clients[0].Helper.Send(poruka);
            server.Clients[1].Helper.Send(poruka);

            if (kraj)
            {
                server.Clients[0].Stop();
                server.Clients[0].Stop();
            }
        }
    
        private bool isPogodak()
        {
            foreach (Slova s in server.Slova)
            {
                if (!s.Pogodio) return false;
            }
            return true;
        }

        private void InitializeTabela()
        {
            dgvTabela.DataSource = null;
            dgvTabela.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvTabela.DefaultCellStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dgvTabela.ColumnCount = 2;
            dgvTabela.Columns[0].HeaderText = "Slovo";
            dgvTabela.Columns[1].HeaderText = "Poeni";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtRijec.Text != "")
            {
                string s = txtRijec.Text;
                server.Slova = new List<Slova>();

                List<char> l;
                l = s.ToList().Distinct().ToList();

                foreach (char c in l)
                {
                    server.Slova.Add(new Slova
                    {
                        Slovo = c,
                        Poeni = s.Where(x => x == c).Count(),
                        Pogodio = false
                    });
                }
                max = server.Slova.Count * 3;
                lblMaxBrojPokusaja.Text = "Maksimalan broj pokusaja: " + max.ToString();
                try
                {
                    server.Start();
                    MessageBox.Show("Server je pokrenut");
                    Thread serverNit = new Thread(server.HandleClients);
                    serverNit.Start();
                }
                catch (Exception)
                {
                    MessageBox.Show("Server ne moze da se pokrene");
                }
            }
        }

        private void dgvTabela_SelectionChanged(object sender, EventArgs e)
        {
            dgvTabela.ClearSelection();
        }
    }
}
