using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace KrizciKrozci
{
    public partial class Okno : Form
    {
        // spremenljivka naVrsti ima vrednost true, če je na vrsti X oz. false, če je na vrsti O
        private bool naVrsti = true;
        // štejemo kliknjena polja
        private int stevec = 0;
        // spremenljivka, ki nam pove, če igramo proti računalniku
        private bool protiRacunalniku = false;

        public Okno()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Priprava igre
        /// </summary>
        private void PonovnaIgra()
        {
            naVrsti = true;
            stevec = 0;

            foreach (Control element in Controls)
            {
                if (element.GetType() == typeof(Button) && (string)element.Tag != "igraj")
                {
                    Button gumb = (Button)element;
                    gumb.Enabled = true;
                    gumb.Text = "";
                    gumb.BackColor = Color.Silver;
                }
            }
        }

        /// <summary>
        /// Kaj se zgodi ob kliku na polje na igralni plošči
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KlikNaPolje(object sender, EventArgs e)
        {
            bool zmaga;

            // pridobimo gumb, na katerega smo kliknili
            Button gumb = (Button)sender;

            // na polje zapišemo značko igralca, ki je na vrsti
            if (naVrsti)
            {
                gumb.Text = "X";
                gumb.BackColor = Color.FromArgb(137, 225, 3);
            }

            else
            {
                gumb.Text = "O";
                gumb.BackColor = Color.FromArgb(240, 128, 128);
            }

            // zamenjamo igralca
            naVrsti = !naVrsti;

            //onemogočimo izbrano polje
            gumb.Enabled = false;

            // povečamo števec kliknjenih polj
            stevec++;

            // preverimo, če smo dobili zmagovalca
            zmaga = PreveriZaZmagovalca();

            if (protiRacunalniku && !zmaga)
            {
                poteza.Start();
            }
        }

        /// <summary>
        /// Metoda, ki preveri, če imamo zmagovalca ali
        /// pa je rezultat izenačen.
        /// </summary>
        private bool PreveriZaZmagovalca()
        {
            bool imamoZmagovalca = false;

            // za zmagovalca preverimo horizonzalno
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled && !A2.Enabled && !A3.Enabled))
                imamoZmagovalca = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled && !B2.Enabled && !B3.Enabled))
                imamoZmagovalca = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled && !C2.Enabled && !C3.Enabled))
                imamoZmagovalca = true;

            // za zmagovalca preverimo vertikalno
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled && !B1.Enabled && !C1.Enabled))
                imamoZmagovalca = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled && !B2.Enabled && !C2.Enabled))
                imamoZmagovalca = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled && !B3.Enabled && !C3.Enabled))
                imamoZmagovalca = true;

            // za zmagovalca preverimo diagonalno
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled && !B2.Enabled && !C3.Enabled))
                imamoZmagovalca = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!A3.Enabled && !B2.Enabled && !C1.Enabled))
                imamoZmagovalca = true;

            // v primeru zmagovalca
            if (imamoZmagovalca)
            {
                if (naVrsti)
                {
                    naVrstiJe.Text = "Konec igre!";
                    MessageBox.Show("Zmagal je krožec!");
                }

                else
                {
                    naVrstiJe.Text = "Konec igre!";
                    MessageBox.Show("Zmagal je križec!");
                }

                konecIgre.Visible = true;
                naVrstiJe.Text = "";

                igralecIgralec.Visible = true;
                igralecIgralec.Enabled = true;

                igralecRacunalnik.Visible = true;
                igralecRacunalnik.Enabled = true;

                racunalnikIgralec.Visible = true;
                racunalnikIgralec.Enabled = true;

                return true;
            }

            else
            {
                // v primeru izenačenega rezultata
                if (stevec == 9)
                {
                    naVrstiJe.Text = "Konec igre!";
                    MessageBox.Show("Izenačen rezultat!");

                    konecIgre.Visible = true;
                    naVrstiJe.Text = "";

                    igralecIgralec.Visible = true;
                    igralecIgralec.Enabled = true;

                    igralecRacunalnik.Visible = true;
                    igralecRacunalnik.Enabled = true;

                    racunalnikIgralec.Visible = true;
                    racunalnikIgralec.Enabled = true;

                    return true;
                }

                else
                {
                    if (naVrsti == true) naVrstiJe.Text = "Na vrsti je križec";
                    else naVrstiJe.Text = "Na vrsti je krožec";

                    return false;
                }
            }
        }

        /// <summary>
        /// Različica igralec proti igralcu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IgralecProtiIgralcu(object sender, EventArgs e)
        {
            naVrstiJe.Text = "Na vrsti je križec";

            konecIgre.Visible = false;

            igralecIgralec.Visible = false;
            igralecIgralec.Enabled = false;

            igralecRacunalnik.Visible = false;
            igralecRacunalnik.Enabled = false;

            racunalnikIgralec.Visible = false;
            racunalnikIgralec.Enabled = false;

            naVrstiJe.Visible = true;

            PonovnaIgra();
        }

        /// <summary>
        /// Različica igralec proti računalniku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IgralecProtiRacunalniku(object sender, EventArgs e)
        {
            protiRacunalniku = true;

            naVrstiJe.Text = "Na vrsti je križec";

            konecIgre.Visible = false;

            igralecIgralec.Visible = false;
            igralecIgralec.Enabled = false;

            igralecRacunalnik.Visible = false;
            igralecRacunalnik.Enabled = false;

            racunalnikIgralec.Visible = false;
            racunalnikIgralec.Enabled = false;

            naVrstiJe.Visible = true;

            PonovnaIgra();
        }

        /// <summary>
        /// Pripravi igro tako, da prvi igra računalnik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RacunalnikPriprava(object sender, EventArgs e)
        {
            protiRacunalniku = true;

            naVrstiJe.Text = "Na vrsti je križec";

            konecIgre.Visible = false;

            igralecIgralec.Visible = false;
            igralecIgralec.Enabled = false;

            igralecRacunalnik.Visible = false;
            igralecRacunalnik.Enabled = false;

            racunalnikIgralec.Visible = false;
            racunalnikIgralec.Enabled = false;

            naVrstiJe.Visible = true;

            PonovnaIgra();
            poteza.Start();
        }

        /// <summary>
        /// Poišče najboljšo potezo računalnika in jo izvede
        /// </summary>
        /// <returns>Najboljša poteza</returns>
        private Button PotezaRacunalnik()
        {
            Button poteza;
            string ikonaIgralec;
            string ikonaNasprotnik;

            if (naVrsti)
            {
                ikonaIgralec = "X";
                ikonaNasprotnik = "O";
            }

            else
            {
                ikonaIgralec = "O";
                ikonaNasprotnik = "X";
            }


            // Preveri za zmagovalno potezo
            poteza = PreveriZaZmagoAliObrambo(ikonaIgralec);
            if (poteza == null)
            {
                // Preveri za obrambo
                poteza = PreveriZaZmagoAliObrambo(ikonaNasprotnik);
                if (poteza == null)
                {
                    // Preveri za potezo v kotih
                    poteza = PreveriVKotih(ikonaIgralec);
                    if (poteza == null)
                    {
                        // Preveri za potezo v notranjosti
                        poteza = PreveriVNotranjosti();
                    }
                }

            }
            return poteza;
        }

        /// <summary>
        /// Preveri za potezo v notranjosti
        /// </summary>
        /// <returns></returns>
        private Button PreveriVNotranjosti()
        {
            Button b;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }
            }
            return null;
        }

        /// <summary>
        /// Preveri za potezo v kotih
        /// </summary>
        /// <returns></returns>
        private Button PreveriVKotih(string ikonaIgralec)
        {
            if (A1.Text == ikonaIgralec)
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == ikonaIgralec)
            {
                if (A1.Text == "")
                    return A1;
                if (C1.Text == "")
                    return C1;
                if (C3.Text == "")
                    return C3;
            }

            if (C3.Text == ikonaIgralec)
            {
                if (C1.Text == "")
                    return C1;
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
            }

            if (C1.Text == ikonaIgralec)
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        /// <summary>
        /// Preveri za zmago ali obrambo, odvisno od podanega parametra
        /// </summary>
        /// <param name="ikonaIgralca"></param>
        /// <returns></returns>
        private Button PreveriZaZmagoAliObrambo(string ikonaIgralca)
        {
            // Preverimo horizontalno
            // Prva vrstica
            if ((A1.Text == ikonaIgralca) && (A2.Text == ikonaIgralca) && (A3.Text == ""))
                return A3;
            if ((A2.Text == ikonaIgralca) && (A3.Text == ikonaIgralca) && (A1.Text == ""))
                return A1;
            if ((A1.Text == ikonaIgralca) && (A3.Text == ikonaIgralca) && (A2.Text == ""))
                return A2;

            // Druga vrstica
            if ((B1.Text == ikonaIgralca) && (B2.Text == ikonaIgralca) && (B3.Text == ""))
                return B3;
            if ((B2.Text == ikonaIgralca) && (B3.Text == ikonaIgralca) && (B1.Text == ""))
                return B1;
            if ((B1.Text == ikonaIgralca) && (B3.Text == ikonaIgralca) && (B2.Text == ""))
                return B2;

            // Tretja vrstica
            if ((C1.Text == ikonaIgralca) && (C2.Text == ikonaIgralca) && (C3.Text == ""))
                return C3;
            if ((C2.Text == ikonaIgralca) && (C3.Text == ikonaIgralca) && (C1.Text == ""))
                return C1;
            if ((C1.Text == ikonaIgralca) && (C3.Text == ikonaIgralca) && (C2.Text == ""))
                return C2;

            // Preverimo vertikalno
            //Prvi stolpec
            if ((A1.Text == ikonaIgralca) && (B1.Text == ikonaIgralca) && (C1.Text == ""))
                return C1;
            if ((B1.Text == ikonaIgralca) && (C1.Text == ikonaIgralca) && (A1.Text == ""))
                return A1;
            if ((A1.Text == ikonaIgralca) && (C1.Text == ikonaIgralca) && (B1.Text == ""))
                return B1;

            // Drugi stolpec
            if ((A2.Text == ikonaIgralca) && (B2.Text == ikonaIgralca) && (C2.Text == ""))
                return C2;
            if ((B2.Text == ikonaIgralca) && (C2.Text == ikonaIgralca) && (A2.Text == ""))
                return A2;
            if ((A2.Text == ikonaIgralca) && (C2.Text == ikonaIgralca) && (B2.Text == ""))
                return B2;

            // Tretji stolpec
            if ((A3.Text == ikonaIgralca) && (B3.Text == ikonaIgralca) && (C3.Text == ""))
                return C3;
            if ((B3.Text == ikonaIgralca) && (C3.Text == ikonaIgralca) && (A3.Text == ""))
                return A3;
            if ((A3.Text == ikonaIgralca) && (C3.Text == ikonaIgralca) && (B3.Text == ""))
                return B3;

            // Preverimo po obeh diagonalah
            // Diagonala A1-C3
            if ((A1.Text == ikonaIgralca) && (B2.Text == ikonaIgralca) && (C3.Text == ""))
                return C3;
            if ((B2.Text == ikonaIgralca) && (C3.Text == ikonaIgralca) && (A1.Text == ""))
                return A1;
            if ((A1.Text == ikonaIgralca) && (C3.Text == ikonaIgralca) && (B2.Text == ""))
                return B2;

            // Diagonala A3-C1
            if ((A3.Text == ikonaIgralca) && (B2.Text == ikonaIgralca) && (C1.Text == ""))
                return C1;
            if ((B2.Text == ikonaIgralca) && (C1.Text == ikonaIgralca) && (A3.Text == ""))
                return A3;
            if ((A3.Text == ikonaIgralca) && (C1.Text == ikonaIgralca) && (B2.Text == ""))
                return B2;

            return null;
        }

        /// <summary>
        /// Izvede najboljšo potezo računalnika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poteza_Tick(object sender, EventArgs e)
        {
            // Če začne računalnik, je prva poteza na sredini tj. gumb B2
            if (stevec == 0)
            {
                B2.Text = "X";
                B2.BackColor = Color.FromArgb(137, 225, 3);
                B2.Enabled = false;
            }

            else
            {
                Button premik = PotezaRacunalnik();

                if (naVrsti)
                {
                    premik.Text = "X";
                    premik.BackColor = Color.FromArgb(137, 225, 3);
                    premik.Enabled = false;
                }

                else
                {
                    premik.Text = "O";
                    premik.BackColor = Color.FromArgb(240, 128, 128);
                    premik.Enabled = false;
                }
            }

            stevec++;

            naVrsti = !naVrsti;
            poteza.Stop();
            PreveriZaZmagovalca();
        }
    }
}