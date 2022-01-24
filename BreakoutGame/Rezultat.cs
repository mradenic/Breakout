using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    public partial class Rezultat : Form
    {
        List<Label> imena = new List<Label>();
        public Rezultat(int rezultat, string ime = "", int k = 0)
        {
            InitializeComponent();

            imena.Add(label1);
            imena.Add(label2);
            imena.Add(label3);
            imena.Add(label4);
            imena.Add(label5);
            imena.Add(label6);
           
            // nije najbolji rezultat
            if (ime == "")
            {
                var stream = new StreamReader(@".\..\..\Resources\rezultati.txt");
                for (int i = 0; i < 6; i += 2)
                {
                    string red = stream.ReadLine();
                    string[] postojecaImena = red.Split(',');
                    imena[i].Text = postojecaImena[1];
                    imena[i + 1].Text = postojecaImena[0];
                }

                label8.Text = "Vaš rezultat: " + rezultat.ToString();
            }
            //medu najboljim rezultatima
            else   
            {
                label8.Text = "Čestitamo!";
                var stream = new StreamReader(@".\..\..\Resources\rezultati.txt");
                List<string> novoIme = new List<string>();
                for (int i = 0; i < 6; i += 2)
                {
                    if (i/2 == k)
                    {
                        imena[i].ForeColor = Color.DarkGreen;
                        imena[i].Text = ime;
                        imena[i + 1].ForeColor = Color.DarkGreen;
                        imena[i + 1].Text = rezultat.ToString();
                        novoIme.Add(rezultat.ToString());
                        novoIme.Add(ime);
                    }
                    else
                    {
                        string red = stream.ReadLine();
                        string[] postojecaImena = red.Split(',');
                        imena[i].Text = postojecaImena[1];
                        imena[i + 1].Text = postojecaImena[0];

                        novoIme.Add(postojecaImena[0]);
                        novoIme.Add(postojecaImena[1]);
                    }
                }
                stream.Close();

                //prvo obrisemo, pa upisemo
                var pisac = new StreamWriter(@".\..\..\Resources\rezultati.txt", false);
                pisac.WriteLine("");
                pisac.Close();

                using (StreamWriter pis = new StreamWriter(@".\..\..\Resources\rezultati.txt"))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        string linija = novoIme[i * 2] + "," + novoIme[i * 2 + 1];
                        pis.WriteLine(linija);
                    }
                }                
            }          
        }

        private void ScoresForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
                this.Close();
        }
    }
}
