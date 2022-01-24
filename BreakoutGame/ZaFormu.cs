using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    class ZaFormu
    {
        public static string PrikaziTekst(string tekst, string opis)
        {
            Form forma = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = opis,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label label = new Label() { Left = 50, Top = 20, Text = tekst, Width = 300 };
            TextBox box = new TextBox() { Left = 50, Top = 50, Width = 300 };
            Button potvrda = new Button() { Text = "Ok", Left = 250, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            potvrda.Click += (sender, e) => { forma.Close(); };
            forma.Controls.Add(box);
            forma.Controls.Add(potvrda);
            forma.Controls.Add(label);
            forma.AcceptButton = potvrda;

            return forma.ShowDialog() == DialogResult.OK ? box.Text : "";
        }
    }
}
