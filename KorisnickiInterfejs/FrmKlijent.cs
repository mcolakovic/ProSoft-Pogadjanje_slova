using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KorisnickiInterfejs
{
    public partial class FrmKlijent : Form
    {
        string email;
        public FrmKlijent()
        {
            InitializeComponent();
            InitializeTabela();
            try
            {
                Communication.Instance.Connect();
            }
            catch (Exception)
            {
                MessageBox.Show("Igra je u toku");
                Environment.Exit(0);
            }
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validacija())
                {
                    User user = new User { Email = txtMail.Text };
                    Poruka poruka = new Poruka { Operations = Operations.Login, PorukaObject = user };
                    Communication.Instance.SendMessage(poruka);
                    poruka = Communication.Instance.ReadMessage<Poruka>();
                    if (poruka.IsSuccessful)
                    {
                        MessageBox.Show("OK");
                        email = ((User)poruka.PorukaObject).Email;
                        InitializeListener();
                    }
                    else if (poruka.MessageText != null)
                    {
                        MessageBox.Show(poruka.MessageText);
                        Environment.Exit(0);
                    }

                }
                else
                {
                    MessageBox.Show("Pogresan unos podataka");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool Validacija()
        {
            if (!Regex.IsMatch(txtMail.Text, @"^[1-9a-zA-Z]+\@{1}[1-9a-zA-Z]+")) return false;
            return true;
        }

        private void InitializeListener()
        {
            try
            {
                Thread nitPoruke = new Thread(CitajPoruke);
                nitPoruke.IsBackground = true;
                nitPoruke.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void CitajPoruke()
        {
            try
            {
                while (true)
                {
                    Poruka poruka = Communication.Instance.ReadMessage<Poruka>();
                    switch (poruka.Operations)
                    {
                        case Operations.Igra:
                            if (poruka.IsSuccessful)
                            {
                                Invoke(new Action(() => dgvTabela.Rows.Add(((Igra)poruka.PorukaObject).Slova.Slovo, ((Igra)poruka.PorukaObject).Slova.Poeni)));

                                if (((Igra)poruka.PorukaObject).PrviIgrac.Email == email)
                                    Invoke(new Action(() => lblIgracA.Text = ((Igra)poruka.PorukaObject).PrviIgrac.Email + "  " + ((Igra)poruka.PorukaObject).PrviIgrac.BrojPoena));
                                else
                                    Invoke(new Action(() => lblIgracB.Text = ((Igra)poruka.PorukaObject).PrviIgrac.Email + "  " + ((Igra)poruka.PorukaObject).PrviIgrac.BrojPoena));

                                if (((Igra)poruka.PorukaObject).DrugiIgrac.Email == email)
                                    Invoke(new Action(() => lblIgracA.Text = ((Igra)poruka.PorukaObject).DrugiIgrac.Email + "  " + ((Igra)poruka.PorukaObject).DrugiIgrac.BrojPoena));
                                else
                                    Invoke(new Action(() => lblIgracB.Text = ((Igra)poruka.PorukaObject).DrugiIgrac.Email + "  " + ((Igra)poruka.PorukaObject).DrugiIgrac.BrojPoena));

                                Invoke(new Action(() => txtBrojPokusaja.Text = "Broj pokusaja: " + (((Igra)poruka.PorukaObject).PrviIgrac.BrojPokusaja + ((Igra)poruka.PorukaObject).DrugiIgrac.BrojPokusaja).ToString()));

                                if (((Igra)poruka.PorukaObject).PrviIgrac.Email == email && ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat != null)
                                    Invoke(new Action(() => lblInfo.Text = ((Igra)poruka.PorukaObject).PrviIgrac.Rezultat));
                                else if (((Igra)poruka.PorukaObject).DrugiIgrac.Email == email && ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat != null)
                                    Invoke(new Action(() => lblInfo.Text = ((Igra)poruka.PorukaObject).DrugiIgrac.Rezultat));
                            }
                            break;
                        case Operations.ZapocniIgru:
                            if (poruka.IsSuccessful)
                            {
                                Invoke(new Action(() => btnPogadjaj.Enabled = true));
                                Invoke(new Action(() => lblInfo.Text = poruka.MessageText));
                                Invoke(new Action(() => lblMaxBrojPokusaja.Text = "Maksimalan broj pokusaja: " + ((Info)poruka.PorukaObject).MaxBrojPokusaja.ToString()));
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void btnPogadjaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPogadjaj.Text != "")
                {
                    Poruka poruka = new Poruka
                    {
                        Operations = Operations.Igra,
                        PorukaObject = new Slova
                        {
                            Slovo = char.Parse(txtPogadjaj.Text)
                        }
                    };
                    Communication.Instance.SendMessage(poruka);
                    txtPogadjaj.Text = "";
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }

        private void dgvTabela_SelectionChanged(object sender, EventArgs e)
        {
            dgvTabela.ClearSelection();
        }
    }
}
