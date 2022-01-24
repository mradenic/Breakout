using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace Breakout
{
	public partial class Form1 : Form
	{
		int rezultat, plocaBrzina, timer, vrijemeSpustanja, vrijemeUbrzavanje, vrijemeEfekt, najniza;
		double lopticaBrzina, standardnaBrzina;
		bool smjerL, smjerD, igraGotova;

		//varijable za pokretanje loptice
		double lopX, lopY;

		List<PictureBox> plociceLista = new List<PictureBox>();
		List<PictureBox> efektiLista = new List<PictureBox>();
		PictureBox loptica1 = new PictureBox();



		DateTime pocetakZ = DateTime.Now;

		string zadnjiZTip;
		double zadnjiZVrijeme;
		Random random = new Random();


		public Form1()
		{
			InitializeComponent();
			postaviPlocice();
		}

		private void postaviPlocice()
		{
			nacrtajRed(4);
			postaviIgru();
		}

		private void postaviIgru() 
		{
			rezultatTekst.Text = "Rezultat: " + rezultat;
			textPocetniUdarac.Text = "Kliknite na željenu pločicu za početak";
			labelVrijeme.Text = "00:00";

			smjerL = false;
			smjerD = false;
			igraGotova = false;
			rezultat = 0;
			timer = 0;
			vrijemeSpustanja = 0;
			vrijemeEfekt = 0;
			vrijemeUbrzavanje = 0;
			najniza = 0;

			plocaBrzina = 16;
			ploca.Left = (int)(splitContainer1.Panel2.Width / 2 - ploca.Width / 2);

			lopticaBrzina = 0;
			standardnaBrzina = 0;
			
			//Stvaramo  lopticu.
			var loptica = new PictureBox();
			loptica.Height = 26;
			loptica.Width = 26;
			loptica.BackColor = Color.Thistle;
			loptica.BackgroundImage = Properties.Resources.loptica;
			loptica.BackgroundImageLayout = ImageLayout.Stretch;
			loptica.Left = ploca.Left + ploca.Width / 2 - loptica.Width / 2;
			loptica.Top = ploca.Top - loptica.Height;
			loptica1 = loptica;
			this.splitContainer1.Panel2.Controls.Add(loptica);
			lopX = 0;
			lopY = 0;

			pocetakZ = DateTime.Now;
			zadnjiZTip = "";
			zadnjiZVrijeme = 0;

			igraVrijeme.Start();
		}

		private void nacrtajRed(int n) 
        {

			int gore = 5;
			int lijevo = 1;			
			int sirina = (int)(splitContainer1.Panel2.Width - 14) / 10;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 10; j++)
                {
					var plocica = new PictureBox();
					plocica.Width = sirina;
					plocica.Left = lijevo;
					plocica.Top = gore;
					plocica.Height = 32;
					plocica.BackColor = Color.Thistle;
					bool biranjeEfekta = false;
					double biranjeBoje = random.NextDouble();
					if(biranjeBoje <= 0.20)
                    {
						plocica.BackgroundImage = Properties.Resources.narancasta;
						plocica.Tag = new Plocica { plocicaBoja = "narancasta" };
					}
					else if (biranjeBoje <= 0.40) 
                    {
						plocica.BackgroundImage = Properties.Resources.zelena;
						plocica.Tag = new Plocica { plocicaBoja = "zelena" };
					}
					else if(biranjeBoje <= 0.60) 
                    {
						plocica.BackgroundImage = Properties.Resources.siva;
						plocica.Tag = new Plocica { plocicaBoja = "siva" };

					}
					else if(biranjeBoje <= 0.80) 
                    {
						plocica.BackgroundImage = Properties.Resources.efekt;
						plocica.Tag = new Plocica { plocicaBoja = "efekt" };

					}
					else if (biranjeBoje <= 1) 
                    {
						plocica.BackgroundImage = Properties.Resources.boom;
						plocica.Tag = new Nova { Pokretan = false, Opis = "boom" };
						biranjeEfekta = true;
					}

					plocica.BackgroundImageLayout = ImageLayout.Stretch;
					
					if (!biranjeEfekta)
						plociceLista.Add(plocica);
					else 
						efektiLista.Add(plocica);

					this.splitContainer1.Panel2.Controls.Add(plocica);

					lijevo += sirina;
				}
				gore += 33;
				lijevo = 1;

				if (gore > najniza)
					najniza = gore;
            }
		}


		private void igraTimerEvent(object sender, EventArgs e) 
		{
			rezultatTekst.Text = "Rezultat: " + rezultat;
			if (smjerL == true && ploca.Left > 0)
			{
				ploca.Left -= plocaBrzina;
				if (lopticaBrzina == 0.0)
					loptica1.Left -= plocaBrzina;			
			}
			if (smjerD == true && ploca.Right < splitContainer1.Panel2.Width - 7) 
			{
				ploca.Left += plocaBrzina;
				if (lopticaBrzina == 0.0)
					loptica1.Left += plocaBrzina;
								
			}

			PictureBox mBall = loptica1;
			mBall.Top += (int)lopY;
			mBall.Left += (int)lopX;

			paziRub();

			foreach (PictureBox efekt in efektiLista.ToList())
			{
				Nova efektTag = (Nova)efekt.Tag;
				if (!efektTag.Pokretan) 
					continue;				
				efekt.Top += 10;
				if (efekt.Top + 10 > ploca.Top)
				{
					this.splitContainer1.Panel2.Controls.Remove(efekt);
					efektiLista.Remove(efekt);
				}
				else if (efekt.Bounds.IntersectsWith(ploca.Bounds))
				{
					if (efektTag.Opis == "bonus100")
					{
						rezultat += 100;
						this.splitContainer1.Panel2.Controls.Remove(efekt);
						efektiLista.Remove(efekt);
					}
					else if (efektTag.Opis == "fast")
					{
						//namjestanje brzine i kuta
						lopticaBrzina = (int)(1.25 * standardnaBrzina);
						vrijemeEfekt = 1;
						this.splitContainer1.Panel2.Controls.Remove(efekt);
						efektiLista.Remove(efekt);

						azurirajBrzinuLoptice();
		
					}
				}
			}

			paziPloca();
			paziPlocica();

			//Provjera je li loptica izasla iz granica polja.
			if(loptica1.Top > ploca.Top)
            {
				loptica1.Top = -100;
				this.splitContainer1.Panel2.Controls.Remove(loptica1);
			}

			if(loptica1.Top == -100)
				GameOver();
            
			if (vrijemeSpustanja == 11)
				pokreniZvuk("ohOh");

			//Ako je proslo bar 15 sekundi od zadnjeg dodavanja probaj dodat novi red na vrh.
			if (vrijemeSpustanja > 15)
			{
				//prvo provjeri moze li se pomaknuti 
				foreach (var pl in plociceLista)
						if(loptica1.Bounds.IntersectsWith(pl.Bounds))
							return;
				
				foreach (var ef in efektiLista)
				{
					Nova efektTag = (Nova)ef.Tag;
					if(!efektTag.Pokretan && loptica1.Bounds.IntersectsWith(ef.Bounds))
							return;
				}

				foreach (var pl in plociceLista)
				{
					pl.Top += 33;
					najniza += 33;
					//ako su cigle dosle prenisko igra je gotova
					if (pl.Top + 33 > ploca.Top)
						GameOver();
				}
				foreach (var ef in efektiLista)
				{
					Nova efektTag = (Nova)ef.Tag;
                    if (!efektTag.Pokretan)
                    {
						ef.Top += 33;
						najniza += 33;
						if (ef.Top + 33 > ploca.Top)
							GameOver();
					}					
				}
				nacrtajRed(1);
				vrijemeSpustanja = 0;
			}
		}

		private void paziRub()
		{

			if (loptica1.Top < 0 && lopY < 0)
				lopY = -lopY;

			if ((loptica1.Left < 0 && lopX < 0) || (loptica1.Right > splitContainer1.Panel2.Width && lopX > 0))
				lopX = -lopX;

		}
		private void paziPlocica()
		{
				foreach (Control x in this.splitContainer1.Panel2.Controls)
				{
					//Gledamo presjek lopte s plocicama.
					if (loptica1.Bounds.IntersectsWith(x.Bounds) && x is PictureBox)
					{
						if (x.Tag is Plocica || (x.Tag is Nova && !((Nova)x.Tag).Pokretan))
						{
							PictureBox y = new PictureBox();
							bool dvijeOdjednom = false;

							// provjerimo je li imamo presjek s dvije plocice odjednom
							foreach (Control z in this.splitContainer1.Panel2.Controls)
							{
								if (loptica1.Bounds.IntersectsWith(z.Bounds) && z is PictureBox)
								{
									if (z.Tag is Plocica || (z.Tag is Nova && !((Nova)z.Tag).Pokretan))
									{
										if (z.Left == x.Left && z.Top == x.Top)
											continue;
										else
										{
											y = (PictureBox)z;
											dvijeOdjednom = true;
											break;
										}
									}
								}
							}

							// loptica udara samo od jednu plocicu
							if (!dvijeOdjednom)
							{
							//od kuda dolazi lopticai
								int lopticaY = loptica1.Top + (int)(loptica1.Height / 2);
								int lopticaX = loptica1.Left + (int)(loptica1.Width / 2);

								if ((lopticaX >= x.Left && lopticaX <= x.Right && loptica1.Top <= x.Bottom && lopY < 0 ) ||
										(lopticaX >= x.Left && lopticaX <= x.Right && loptica1.Bottom >= x.Top && lopX > 0))
									//s gornje ili donje strane
									lopY = -lopY;
								else if ((lopticaY <= x.Bottom && lopticaY >= x.Top && loptica1.Right >= x.Left && lopX > 0) ||
										(lopticaY <= x.Bottom && lopticaY >= x.Top && loptica1.Left <= x.Right && lopX < 0))
									// s lijeva ili desna
									lopX = -lopX;

								else if ((lopticaX > x.Right && lopticaY > x.Bottom) ||
											(lopticaY > x.Bottom && lopticaX > x.Right))
								//u koji rub udara
								//udara u desni donji rub
								{
										lopX = Math.Abs(lopX);
										lopY = Math.Abs(lopY);
								}
								else if ((lopticaX < x.Left && lopticaY > x.Bottom) ||
										(lopticaY > x.Bottom && lopticaX < x.Left))
									//udara u lijevi donji rub
									{
										lopX = -Math.Abs(lopX);
										lopY = Math.Abs(lopY);
									}
								else if ((lopticaX > x.Right && lopticaY < x.Top) ||
										(lopticaY < x.Top && lopticaX > x.Right))
									//udara u gornji desni rub									
									{
										lopX = Math.Abs(lopX);
										lopY = -Math.Abs(lopY);
									}
								else if ((lopticaX < x.Left && lopticaY < x.Top) ||
												(lopticaY < x.Top && lopticaX < x.Left))
									//udara u gornji lijevi rub
									{
										lopX = -Math.Abs(lopX);
										lopY = -Math.Abs(lopY);
									}

								unistiPlocicu(x);
							}
							// loptica udara od dvije plocice
							else if (dvijeOdjednom)
							{
								int lopticaX = loptica1.Left + (int)(loptica1.Width / 2);
								int lopticaY = loptica1.Top + (int)(loptica1.Height / 2);
								int xX = x.Left + (int)(x.Width / 2);
								int xY = x.Top + (int)(x.Height / 2);
								int yX = y.Left + (int)(y.Width / 2);
								int yY = y.Top + (int)(y.Height / 2);

								if ((lopticaX >= x.Left && lopticaX <= y.Right) ||
											(lopticaX >= y.Left && lopticaX <= x.Right))
								// s gornje ili donje strane
								{
									lopY = -lopY;
									//ne zelimo da se unisti druga 
									if (Math.Abs(loptica1.Bottom - x.Top) > Math.Abs(loptica1.Top - x.Bottom))
									loptica1.Top = x.Bottom + 1;
									else
									loptica1.Top = x.Top - 1 - loptica1.Height;
									//uništavamo bližu ciglu
									if (Math.Abs(lopticaX - xX) > Math.Abs(lopticaX - yX))
										unistiPlocicu(y);
									else
										unistiPlocicu(x);
								}
								else
								// s lijeva ili desna
								{
									lopX = -lopX;
									//ne zelimo da se unisti druga 
									if (Math.Abs(loptica1.Left - x.Right) > Math.Abs(loptica1.Right - x.Left))
										loptica1.Left = x.Left - 1 - loptica1.Width;
										else
										loptica1.Left = x.Right + 1;
										//uništavamo bližu ciglu
										if (Math.Abs(lopticaX - xY) > Math.Abs(lopticaX - yY))
											unistiPlocicu(y);
										else
											unistiPlocicu(x);
								}
							}
						}
					}
				}
		}

		private void paziPloca()
        {
			if (loptica1.Bounds.IntersectsWith(ploca.Bounds))
			{
				//gdje loptica udara o plocu
				double pozicija = loptica1.Width / 2 + loptica1.Left;
				double sredinaPloce = ploca.Left + ploca.Width / 2;
				double omjer = 2 * (pozicija - sredinaPloce) / ploca.Width;

				//namjestanje kuteva
				omjer = (omjer < -1) ? -1 : omjer;
				omjer = (omjer > 1) ? 1 : omjer;
				double kut = Math.PI / 2 + omjer * (-Math.PI / 2);
				kut = Math.Abs(kut) < Math.PI / 7 ? Math.PI / 7 : kut;
				kut = Math.Abs(kut) > 6 * Math.PI / 7 ? 6 * Math.PI / 7 : kut;
				lopX = Math.Cos(kut) * lopticaBrzina;
				lopY = -Math.Sin(kut) * lopticaBrzina;
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			timer++;
			int minute = timer / 60;
			int sekunde = timer % 60;
			labelVrijeme.Text = minute.ToString("D2") + ":" + sekunde.ToString("D2");

			vrijemeUbrzavanje++;
			vrijemeSpustanja++;

			//ubrzavanje loptice svakih 20 sekundi
			if (vrijemeUbrzavanje > 20)
			{
				standardnaBrzina += 1;
				vrijemeUbrzavanje = 0;
				if (lopticaBrzina == standardnaBrzina - 1)
				{
					lopticaBrzina = standardnaBrzina;
					azurirajBrzinuLoptice();
				}
			}

			// Ako vrijemeEfekt > 0 znaci da je pokupljen efekt za brzu lopticu
			if (vrijemeEfekt != 0)
				vrijemeEfekt++;
			//efekt traje 10 sekundi
			if (vrijemeEfekt > 10)
			{
				vrijemeEfekt = 0;
				lopticaBrzina = standardnaBrzina;
				azurirajBrzinuLoptice();
			}
		}

		private void klikPloca(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Right)
				smjerD = true;
			else if (e.KeyCode == Keys.Left)			
				smjerL = true;		
		}

		private void klikPlocaPusten(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && igraGotova == true)
			{
				ukloniPlocice();
				postaviPlocice();
			}

			if (e.KeyCode == Keys.Right)
				smjerD = false;

			if (e.KeyCode == Keys.Left)			
				smjerL = false;
					
		}


		private void unistiPlocicu(Control x)
        {
			if (x.Tag is Plocica)
			{
				pokreniZvuk("plocica");
				Plocica plocicaTag = (Plocica)x.Tag;
				if (plocicaTag.plocicaBoja == "siva")
				{
					x.BackgroundImage = Properties.Resources.sivaPuknuta;
					x.Tag = new Plocica { plocicaBoja = "sivaPuknuta" };
				}
				else
				{
					if (plocicaTag.plocicaBoja == "sivaPuknuta")					
						rezultat += 50;
					
					else
						rezultat += 10;

					this.splitContainer1.Panel2.Controls.Remove(x);

					najniza = 0;
					foreach (PictureBox p in plociceLista.ToList())
						if (p.Top == x.Top && p.Left == x.Left)
							plociceLista.Remove(p);
                        else if (p.Top > najniza) 
							najniza = p.Top;
                        

					//za efekte koji se krecu
					if (plocicaTag.plocicaBoja == "efekt")
					{
						int sirina = (int)(splitContainer1.Panel2.Width - 14) / 10;

						var efekt = new PictureBox();
						efekt.Height = 32;
						efekt.Width = sirina;
						efekt.Left = x.Left;
						efekt.Top = x.Top;
						efekt.BackColor = Color.Thistle;

						double kojiEfekt = random.NextDouble();
						 if (kojiEfekt <= 0.5)
						{
							efekt.BackgroundImage = Properties.Resources.bonus100;
							efekt.Tag = new Nova { Pokretan = true, Opis = "bonus100" };
						}
						else
						{
							efekt.BackgroundImage = Properties.Resources.fast;
							efekt.Tag = new Nova { Pokretan = true, Opis = "fast" };
						}

						efekt.BackgroundImageLayout = ImageLayout.Stretch;
						efektiLista.Add(efekt);
						this.splitContainer1.Panel2.Controls.Add(efekt);

					}
				}
			}
			else if (x.Tag is Nova)
            {

				Nova efektTag = (Nova)x.Tag;
				if (!efektTag.Pokretan)
				{
					if (efektTag.Opis == "boom")
					{
						pokreniZvuk("boom");

						this.splitContainer1.Panel2.Controls.Remove(x);

						najniza = 0;
						foreach (PictureBox eff in efektiLista.ToList())
							if (eff.Top == x.Top && eff.Left == x.Left)
								efektiLista.Remove(eff);
							else if (eff.Top > najniza)
							{
								Nova ef = (Nova)eff.Tag;
								if (!ef.Pokretan)
									najniza = eff.Top;
							}

						unistiOkolnePlocice(x); 
					}
				}
			}
		}

		private void unistiOkolnePlocice(Control x)
		{			
			var rect = new Rectangle(x.Left - 5, x.Top - 5 ,x.Width + 10, x.Height + 10);
			foreach (Control c in this.splitContainer1.Panel2.Controls)
			{
				if (c is PictureBox && (c.Tag is Plocica || c.Tag is Nova))
				{
					var c_rect = new Rectangle(c.Left, c.Top, c.Width, c.Height);
					if (c_rect.IntersectsWith(rect))
						unistiPlocicu(c);
				}
			}
		}

		private void ukloniPlocice()
		{
			foreach (PictureBox x in efektiLista)
			{
				this.splitContainer1.Panel2.Controls.Remove(x);
			}
			efektiLista.Clear();

			foreach (PictureBox x in plociceLista)
			{
				this.splitContainer1.Panel2.Controls.Remove(x);
			}
			plociceLista.Clear();

		}

		void azurirajBrzinuLoptice()
        {
			double kut = Math.Atan2(lopY, lopX);
			lopX = lopticaBrzina * Math.Cos(kut);
			lopY = lopticaBrzina * Math.Sin(kut);
		}

		private void pocetniUdarac(object sender, MouseEventArgs e)
        {
			//klik za usmjeravanje pocetnog udarca
			if (lopticaBrzina == 0)
            {
				//ako korisnik kliknuo ispod plocica, klik zanemarujemo
				if (e.Y > ploca.Top - 5)
					return;

				int lopticaX = loptica1.Left + (int)loptica1.Width / 2;
				int lopticaY = loptica1.Top;
				int a = Math.Abs((e.X - splitContainer1.Panel1.Width) - lopticaX);
				int b = Math.Abs(e.Y - lopticaY);
			
				double kut = Math.Atan2(b,a);
				

				if ((e.X - splitContainer1.Panel1.Width) < lopticaX)
					kut = Math.PI - kut;

				lopticaBrzina = 13;
				standardnaBrzina = lopticaBrzina;
				textPocetniUdarac.Text = "";

				lopX = Math.Cos(kut) * lopticaBrzina;
				lopY = -Math.Sin(kut) * lopticaBrzina;

				timerGlavni.Start();
			}
        }

		System.Media.SoundPlayer izgubioZ = new System.Media.SoundPlayer(Properties.Resources.nope_snimka);
		System.Media.SoundPlayer pobijedioZ = new System.Media.SoundPlayer(Properties.Resources.success_snimka);
		System.Media.SoundPlayer noveZ = new System.Media.SoundPlayer(Properties.Resources.ohOh_snimka);
		System.Media.SoundPlayer eksplozijaZ = new System.Media.SoundPlayer(Properties.Resources.boom_snimka);
		System.Media.SoundPlayer plocicaZ = new System.Media.SoundPlayer(Properties.Resources.plocica_snimka);


		private void pokreniZvuk(string s)
		{

			DateTime trenutnoVrijeme = DateTime.Now;
			TimeSpan protekloVrijeme = trenutnoVrijeme - pocetakZ;
			double protekleSekunde = protekloVrijeme.TotalSeconds;
			if (s != "izgubio" && s != "boom" && s != "pobijedio" && s != "ohOh")
			{
				if (zadnjiZTip == "ohOh" && protekleSekunde - zadnjiZVrijeme < 2.1)
					return;
				if (zadnjiZTip == "boom" && protekleSekunde - zadnjiZVrijeme < 1.4)
					return;
				if (zadnjiZTip == s && protekleSekunde - zadnjiZVrijeme < 0.05)
					return;
				if (zadnjiZTip == "plocica" && protekleSekunde - zadnjiZVrijeme < 0.2)
					return;
			}

			zadnjiZTip = s;
			zadnjiZVrijeme = protekleSekunde;

			if (s == "ohOh")
				noveZ.Play();
			else if (s == "boom")
				eksplozijaZ.Play();
			else if (s == "plocica")
				plocicaZ.Play();
			else if (s == "izgubio")
				izgubioZ.Play();
			else if (s == "pobijedio")
				pobijedioZ.Play();

		}

		private void GameOver()
		{

			igraGotova = true;
			timerGlavni.Stop();
			igraVrijeme.Stop();
			textPocetniUdarac.Text = "Igra gotova! Pritisni enter za novu igru.";
			rezultatTekst.Text = "Rezultat: " + rezultat;

			//Ako je score među top 3, upiši ga u rezultati.txt

			bool zastavica = false;
			var stream = new StreamReader(@".\..\..\Resources\rezultati.txt");

			for (int i = 0; i < 3; i++)
			{
				string red = stream.ReadLine();
				string[] imena = red.Split(',');
				if (Int32.Parse(imena[0]) < rezultat)
				{
					pokreniZvuk("pobijedio");
					string ime = ZaFormu.PrikaziTekst("Osvojili ste " + (i + 1).ToString() + ". najbolji rezultat. Molimo upišite svoje ime.", "Čestitamo!");
					while (ime == "")
						ime = ZaFormu.PrikaziTekst("Molimo upišite ime.", "Mora sadržavati barem jedan znak");
					stream.Close();
					Form najRezultati = new Rezultat(rezultat, ime, i);
					najRezultati.ShowDialog();
					zastavica = true;
					break;
				}
			}

			if (!zastavica)
			{
				pokreniZvuk("izgubio");
				Form najRezultati = new Rezultat(rezultat);
				najRezultati.ShowDialog();
			}
		}

	}
}
