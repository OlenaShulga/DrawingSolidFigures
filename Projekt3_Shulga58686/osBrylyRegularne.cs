using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Projekt3_Shulga58686.osBrylyGeometryczne;
using System.Drawing.Drawing2D;

namespace Projekt3_Shulga58686
{
    public partial class osBrylyRegularne : Form
    {
        Graphics osRysownica, osPowierzcniaGraficznaWziernikaLinii;
        Pen osPióro;
        List<osBryłaAbstrakcyjna> osLBG = new List<osBryłaAbstrakcyjna>();
        //deklaracja pomocniczej zmiennej dla przechowania współrzędnych 
        Point osPunktLokalizacjiBryły = new Point(-1, -1);
        int osMarginesFormularza = 10;
        int osIndexListy;
        public osBrylyRegularne()
        {
            InitializeComponent();
            //lokalizacja i zwymiarowanie formularza
            this.Location = new Point(Screen.PrimaryScreen.Bounds.X + osMarginesFormularza,
                Screen.PrimaryScreen.Bounds.Y + 2 * osMarginesFormularza);
            this.Width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.85F);
            this.Height = (int)(Screen.PrimaryScreen.Bounds.Height * 0.85F);
            this.StartPosition = FormStartPosition.Manual;
            //można ustalić wartości innych atrybutów formularza
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //lokalizacja i zwymiarowanie kontrolki pictureBox
            ospbRysownica.Location = new Point(ospbKolorWypełnienia.Right + osMarginesFormularza, ospbKolorWypełnienia.Top);
            ospbRysownica.Width = (int)(this.Width * 0.6F);
            ospbRysownica.Height = (int)(this.Height * 0.6F);
            ospbRysownica.BackColor = Color.White;
            ospbRysownica.BorderStyle = BorderStyle.FixedSingle;
            //lokalizacja innych kontrolek
            osbtnDodajNowąBryłę.Location = new Point(osgbAtrybutyNowejBryły.Left, osgbAtrybutyNowejBryły.Bottom + osMarginesFormularza);
            ospbRysownica.Image = new Bitmap(ospbRysownica.Width, ospbRysownica.Height);
            ospnSlajder.Location = new Point(ospbRysownica.Right + osMarginesFormularza, ospbRysownica.Top);
            osgbKryteriaPokazu.Location = new Point(ospbRysownica.Left, ospbRysownica.Bottom + osMarginesFormularza);
            osbtnPowrótZFormLab.Location = new Point(ospnSlajder.Left, ospbRysownica.Top - osMarginesFormularza - osbtnPowrótZFormLab.Height);
            osbtnResetuj.Location = new Point(ospnSlajder.Left + (int)((ospnSlajder.Width - osbtnResetuj.Width) / 2.0F), ospnSlajder.Bottom + osMarginesFormularza);


            //utworzenie egzemplarza osRysownicy
            osRysownica = Graphics.FromImage(ospbRysownica.Image);
            //sformatowanie pióra
            osPióro = new Pen(Color.Black, 1F);
            osPióro.DashStyle = DashStyle.Solid;
            //sformatowanie wzierników
            ospbKolorWypełnienia.BorderStyle = BorderStyle.Fixed3D;
            ospbKolorWypełnienia.BackColor = ospbRysownica.BackColor;
            ospbWiernikLinii.Image = new Bitmap(ospbWiernikLinii.Width, ospbWiernikLinii.Height);
            osPowierzcniaGraficznaWziernikaLinii = Graphics.FromImage(ospbWiernikLinii.Image);
            //wykreslenie domyślnego wzorca linii
            //wykreślenie wziernika linii
            osWykreslenieWziernikaLinii();
        }

        private void kolorLiniiBryłyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog osPaletaKolorów = new ColorDialog();
            osPaletaKolorów.Color = osPióro.Color;
            if(osPaletaKolorów.ShowDialog() == DialogResult.OK)
            {
                osPióro.Color = osPaletaKolorów.Color;
                //uaktualnienie wziernika linii
                osWykreslenieWziernikaLinii();
                //zwolnienie okna dialogowego
                osPaletaKolorów.Dispose();
            }
        }

        private void kreskowaDashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            osPióro.DashStyle = DashStyle.Dash;
            osWykreslenieWziernikaLinii();
        }

        private void kreskowoKropkowaDashDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            osPióro.DashStyle = DashStyle.DashDot;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            osPióro.Width = 3F;
            osWykreslenieWziernikaLinii();
        }

        private void osbtnDodajNowąBryłę_Click(object sender, EventArgs e)
        {
            //wymazanie punktu lokalizacji
            using (SolidBrush osPędzel = new SolidBrush(ospbKolorWypełnienia.BackColor))
            {
                osRysownica.FillEllipse(osPędzel, osPunktLokalizacjiBryły.X - 3, osPunktLokalizacjiBryły.Y - 3, 6, 6);
                    
            }
            //pobranie atrybutów ustawiąnych dla wybranej bryły
            int osWysokośćBryły = ostrbWysokośćBryły.Value;
            int osPromieńBryły = ostrbPromieńBryły.Value;
            int osStopieńWielokąta = (int)osnudStopieńWielokąta.Value;
            int osXsP = osPunktLokalizacjiBryły.X;
            int osYsP = osPunktLokalizacjiBryły.Y;
            int osKątPochylenia = ostrbKątPochylenia.Value;
            //rozpoznanie wybranej bryły
            switch (oscmbListaBrył.SelectedItem)
            {
                case "Walec":
                    osWalec oswalec = new osWalec(osPromieńBryły, osWysokośćBryły, osStopieńWielokąta, osXsP, osYsP, /*90.0F,*/ osPióro.Color, osPióro.DashStyle, (int)osPióro.Width);
                    //dodanie egzemplarza walca do listy osLBG
                    osLBG.Add(oswalec);
                    
                    break;
                case "Stożek":
                    osStożek osstożek = new osStożek(osPromieńBryły, osWysokośćBryły, osStopieńWielokąta, osXsP, osYsP, osPióro.Color, osPióro.DashStyle, (int)osPióro.Width);
                    //dodanie egzemplarza walca do listy osLBG
                    osLBG.Add(osstożek);
                    //osstożek.osWykreśl(osRysownica);
                    break;
                case "Stożek pochylony":
                    osStożekPochylony osstożekpochylony = new osStożekPochylony(osPromieńBryły, osWysokośćBryły, osStopieńWielokąta, osXsP, osYsP, osKątPochylenia, osPióro.Color, osPióro.DashStyle, (int)osPióro.Width);
                    //dodanie egzemplarza walca do listy osLBG
                    osLBG.Add(osstożekpochylony);
                    break;
                case "Graniastosłup":
                    osGraniastosłup osgraniastosłup = new osGraniastosłup(osPromieńBryły, osWysokośćBryły, osStopieńWielokąta, osXsP, osYsP, osPióro.Color, osPióro.DashStyle, (int)osPióro.Width);
                    //dodanie egzemplarza walca do listy osLBG
                    osLBG.Add(osgraniastosłup);
                    osgraniastosłup.osWykreśl(osRysownica);
                    break;
                case "Ostrosłup":
                    osOstrosłup osostrosłup = new osOstrosłup(osPromieńBryły, osWysokośćBryły, osStopieńWielokąta, osXsP, osYsP, osPióro.Color, osPióro.DashStyle, (int)osPióro.Width);
                    //dodanie egzemplarza walca do listy osLBG
                    osLBG.Add(osostrosłup);
                    osostrosłup.osWykreśl(osRysownica);
                    break;
                case "Kula":
                    osKula oskula = new osKula(osPromieńBryły, osXsP, osYsP, osPióro.Color, osPióro.DashStyle, (int)osPióro.Width);
                    //dodanie egzemplarza walca do listy osLBG
                    osLBG.Add(oskula);
                    oskula.osWykreśl(osRysownica);
                    break;
                default:
                    MessageBox.Show("ERROR : żadna bryła nie została wybrana. \n" +
                        "Proszę wybrać bryłę dla wykreślenia z listy");
                    break;

                    
            }
            osZegarObrotu.Enabled = true;
            if (osLBG.Count == 2)
            {
                osgbKryteriaPokazu.Enabled = true;
                ospnSlajder.Enabled = true;
            }
            osbtnDodajNowąBryłę.Enabled = false;
        }

        private void osZegarObrotu_Tick(object sender, EventArgs e)
        {
            const float osKątObrotu = 5F;
            //obracamy wszystkie bryły
            for(int osi =0; osi < osLBG.Count; osi++)
                osLBG[osi].osObróć_i_Wykreśl(ospbRysownica, osRysownica, osKątObrotu);
            Refresh();
        }

        private void oscmbListaBrył_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Pamiętaj, że po wyborze bryły geometrycznej i ustawieniu jej atrybutów geometrycznych i graficznych" +
                "'musisz' ustalić miejsce (lokalizację) wykreślenia wybranej bryły, klikając lewym przyciskiem myszy odpowiednie miejsce 'Rysownicy', " +
                "a następnie kliknij przycisk : Dodaj nową bryłę");
            if (oscmbListaBrył.SelectedItem.ToString() == "Walec" || oscmbListaBrył.SelectedItem.ToString() == "Stożek" || 
                oscmbListaBrył.SelectedItem.ToString() == "Graniastosłup" || oscmbListaBrył.SelectedItem.ToString() == "Ostrosłup")
            {
                //uaktywnienie kontrolek
                ostrbWysokośćBryły.Enabled = true;
                ostrbPromieńBryły.Enabled = true;
                osnudStopieńWielokąta.Enabled = true;
                ostrbKątPochylenia.Enabled = false;
            }
            else
                if (oscmbListaBrył.SelectedItem.ToString() == "Kula")
            {
                ostrbPromieńBryły.Enabled = true;
                osnudStopieńWielokąta.Enabled = false;
                ostrbWysokośćBryły.Enabled = false;
                ostrbKątPochylenia.Enabled = false;
            }
            else//np dla brył pochyłych powinniśmy uaktywnić kontrolkę dla pochylenia bryły
                if(oscmbListaBrył.SelectedItem.ToString() == "Stożek pochylony")
            {
                ostrbWysokośćBryły.Enabled = true;
                ostrbPromieńBryły.Enabled = true;
                osnudStopieńWielokąta.Enabled = true;
                ostrbKątPochylenia.Enabled = true;
            }
           
                

        }

        private void ospbRysownica_Click(object sender, EventArgs e)
        {

        }

        private void ospbRysownica_MouseClick(object sender, MouseEventArgs e)
        {
            //zaznaczony punkt wykreslamy o możliwie małych rozmiarach
            using (SolidBrush osPędzel = new SolidBrush(Color.Red))
            {
                if (osPunktLokalizacjiBryły.X != -1)
                { //wymazanie ustalonego wcześniej położenia bryły
                    osPędzel.Color = ospbKolorWypełnienia.BackColor;
                    osRysownica.FillEllipse(osPędzel, osPunktLokalizacjiBryły.X - 3, osPunktLokalizacjiBryły.Y - 3, 6, 6);
                    osPędzel.Color = Color.Red;
                }
                    //przechowanie współrzędnch miejsca kliknięcia myszą
                    osPunktLokalizacjiBryły = e.Location;
                    //wykreślenie punktu "kontrolnego"
                    osRysownica.FillEllipse(osPędzel, osPunktLokalizacjiBryły.X - 3, osPunktLokalizacjiBryły.Y - 3, 6, 6);
                    //uaktywnienie przycisku
                    osbtnDodajNowąBryłę.Enabled= true;
                ospbRysownica.Refresh();
                
            }
        }

        private void kreskowoKropkowoKropkowaDashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            osPióro.DashStyle = DashStyle.DashDotDot;
            osWykreslenieWziernikaLinii();
        }

        private void kropkowaDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            osPióro.DashStyle = DashStyle.Dot;
            osWykreslenieWziernikaLinii();
        }

        private void ciągłaSolidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            osPióro.DashStyle = DashStyle.Solid;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            osPióro.Width = 1F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            osPióro.Width = 2F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            osPióro.Width = 4F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            osPióro.Width = 5F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            osPióro.Width = 6F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            osPióro.Width = 7F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            osPióro.Width = 8F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            osPióro.Width = 9F;
            osWykreslenieWziernikaLinii();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            osPióro.Width = 10F;
            osWykreslenieWziernikaLinii();
        }

        private void kolorTłaRysownicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog osPaletaKolorów = new ColorDialog();
            osPaletaKolorów.Color = ospbRysownica.BackColor;
            if (osPaletaKolorów.ShowDialog() == DialogResult.OK)
            {
                ospbRysownica.BackColor = osPaletaKolorów.Color;
                //uaktualnienie 
                ospbRysownica.Refresh();
                //zwolnienie okna dialogowego
                osPaletaKolorów.Dispose();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void osbtnPowrótZFormLab_Click(object sender, EventArgs e)
        {
            //odszukanie formularza głównego w kolekcji OpenForms
            foreach (Form osFormX in Application.OpenForms)
                //sprawdzenie czy "znaleziony" formularz jest form1
                if (osFormX.Name == "FormularzGłówny")
                {
                    //ukryci ebieżącego formularza
                    Hide();
                    //odsłonięcie
                    osFormX.Show();
                    //wyjście z metody zdarzenia click
                    return;
                }
            //formularz główny nie został znaleziony

            FormularzGlowny osPrezenter = new FormularzGlowny();
            Hide();
            osPrezenter.Show();
        }

        private void ospbKolorWypełnienia_Click(object sender, EventArgs e)
        {

        }

        private void oslblAtrybutyGraficzneLinii_Click(object sender, EventArgs e)
        {

        }

        private void ospbWiernikLinii_Click(object sender, EventArgs e)
        {

        }

        private void osgbAtrybutyNowejBryły_Enter(object sender, EventArgs e)
        {

        }

        private void oslblKolorWypełnienia_Click(object sender, EventArgs e)
        {

        }

        private void osBryłyRegularne_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult osWynik = MessageBox.Show("Czy rzeczywiście chcesz zakończyć działanie programu?",
                this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            //sprawdzenie odpowiedzi użytkownika programu
            if (osWynik != DialogResult.Yes)
                //skasowanie zdarzenia
                e.Cancel = true;
            else
                //zdarzenie cancel nie może być skasowane
                e.Cancel = false;
        }

        private void osBryłyRegularne_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void osbtnWyłączPokaz_Click(object sender, EventArgs e)
        {
            osZegarSlajdera.Stop();
            osRysownica.Clear(ospbRysownica.BackColor);
            ospbRysownica.Refresh();
            //osbtnLosujPołożenie_Click(sender, e);
            Random osRnd = new Random();
            for (int osi = 0; osi < osLBG.Count; osi++)
            {
                osLBG[osi].osPrzesuńDoNowegoXY(ospbRysownica, osRysownica, osRnd.Next(osLBG[osi].osPromień(), ospbRysownica.Width - osLBG[osi].osPromień()),
                    osRnd.Next(osLBG[osi].osWysokość(), ospbRysownica.Height - osLBG[osi].osWysokość() / 4));
            }
            osZegarObrotu.Enabled = true;
            osgbTrybPokazu.Enabled = true;
            osbtnWłączPokaz.Enabled = true;
            osbtnWyłączPokaz.Enabled = false;
            osbtnNastępna.Enabled = false;
            osbtnPoprzednia.Enabled = false;
            ostrbCzasEkspozycji.Enabled = false;
            osgbKryteriaPokazu.Enabled = true;
            osgbAtrybutyNowejBryły.Enabled = true;
            oscmbListaBrył.Enabled = true;
            //osbtnDodajNowąBryłę.Enabled = true;
            osbtnResetuj.Enabled = true;
            
        }

        private void osbtnWłączPokaz_Click(object sender, EventArgs e)
        {
            bool osPorządek = false;
            osBryłaAbstrakcyjna osBryła;
            //kryterium wysokości
            if (osrdbWysokość.Checked)
            {
                do
                {
                    osPorządek = false;
                    for (int osi = 0; osi < osLBG.Count - 1; osi++)
                        if (osLBG[osi].osWysokość() > osLBG[osi + 1].osWysokość())
                        {
                            osBryła = osLBG[osi + 1];
                            osLBG[osi + 1] = osLBG[osi];
                            osLBG[osi] = osBryła;
                            osPorządek = true;
                        }
                } while (osPorządek);
            }
            else
            //kryterium promienia
            if (osrdbPromień.Checked)
            {
                do
                {
                    osPorządek = false;
                    for (int osi = 0; osi < osLBG.Count - 1; osi++)
                        if (osLBG[osi].osPromień() > osLBG[osi + 1].osPromień())
                        {
                            osBryła = osLBG[osi + 1];
                            osLBG[osi + 1] = osLBG[osi];
                            osLBG[osi] = osBryła;
                            osPorządek = true;
                        }
                } while (osPorządek);
            }
            else
            //kryterium objętości
            if (osrdbObjętość.Checked)
            {
                do
                {
                    osPorządek = false;
                    for (int osi = 0; osi < osLBG.Count - 1; osi++)
                        if (osLBG[osi].osObjętość() > osLBG[osi + 1].osObjętość())
                        {
                            osBryła = osLBG[osi + 1];
                            osLBG[osi + 1] = osLBG[osi];
                            osLBG[osi] = osBryła;
                            osPorządek = true;
                        }
                } while (osPorządek);
            }
            else
            //kryterium pola powierzchni
            if (osrdbPolePowierzchni.Checked)
            {
                do
                {
                    osPorządek = false;
                    for (int osi = 0; osi < osLBG.Count - 1; osi++)
                        if (osLBG[osi].osPolePowierzchni() > osLBG[osi + 1].osPolePowierzchni())
                        {
                            osBryła = osLBG[osi + 1];
                            osLBG[osi + 1] = osLBG[osi];
                            osLBG[osi] = osBryła;
                            osPorządek = true;
                        }
                } while (osPorządek);
            }

            osZegarObrotu.Enabled = false;

            //automatyczny pokaz
            if (osrdbAutomatyczny.Checked)
            {
                osIndexListy = 0;
                osZegarSlajdera.Start();
                ostrbCzasEkspozycji.Enabled = true;
            }
            else
            {
                osbtnNastępna.Enabled = true;
                osbtnPoprzednia.Enabled = true;
                osRysownica.Clear(ospbRysownica.BackColor);
                ostxtNumerBryły.Text = "1";
                osLBG[0].osPrzesuńDoNowegoXY(ospbRysownica, osRysownica, (int)(ospbRysownica.Width / 2F), (int)(ospbRysownica.Height / 2F));
                osLBG[0].osWykreśl(osRysownica);
                ospbRysownica.Refresh();
                osIndexListy = 0;
                
            }
            osbtnWłączPokaz.Enabled = false;
            osbtnWyłączPokaz.Enabled = true;
            osgbTrybPokazu.Enabled = false;
            osgbKryteriaPokazu.Enabled = false;
            osgbAtrybutyNowejBryły.Enabled = false;
            oscmbListaBrył.Enabled = false;
            osbtnDodajNowąBryłę.Enabled = false;
            osbtnResetuj.Enabled = false;
        }

        private void osZegarSlajdera_Tick(object sender, EventArgs e)
        {
            ostxtNumerBryły.Text = (osIndexListy + 1).ToString();
            osRysownica.Clear(ospbRysownica.BackColor);

            osLBG[osIndexListy].osPrzesuńDoNowegoXY(ospbRysownica, osRysownica, (int)(ospbRysownica.Width / 2F), (int)(ospbRysownica.Height / 2F));
            osLBG[osIndexListy].osWykreśl(osRysownica);
            ospbRysownica.Refresh();
            osIndexListy = (osIndexListy + 1) % osLBG.Count;
        }

        private void osbtnPoprzednia_Click(object sender, EventArgs e)
        {
            if (osIndexListy == 0)
                osIndexListy = osLBG.Count - 1;
            else
                osIndexListy--;
            ostxtNumerBryły.Text = (osIndexListy + 1).ToString();
            osRysownica.Clear(ospbRysownica.BackColor);

            osLBG[osIndexListy].osPrzesuńDoNowegoXY(ospbRysownica, osRysownica, (int)(ospbRysownica.Width / 2F), (int)(ospbRysownica.Height / 2F));
            osLBG[osIndexListy].osWykreśl(osRysownica);
            ospbRysownica.Refresh();
        }

        private void osbtnNastępna_Click(object sender, EventArgs e)
        {
            osIndexListy = (osIndexListy + 1) % osLBG.Count;
            ostxtNumerBryły.Text = (osIndexListy + 1).ToString();
            osRysownica.Clear(ospbRysownica.BackColor);
            osLBG[osIndexListy].osPrzesuńDoNowegoXY(ospbRysownica, osRysownica, (int)(ospbRysownica.Width / 2F), (int)(ospbRysownica.Height / 2F));
            osLBG[osIndexListy].osWykreśl(osRysownica);
            ospbRysownica.Refresh();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult osWynik = MessageBox.Show("Czy rzeczywiście chcesz zakończyć działanie programu?",
                this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            //sprawdzenie odpowiedzi użytkownika programu
            if (osWynik != DialogResult.Yes)
                //skasowanie zdarzenia
                return;
            else
                //zdarzenie cancel nie może być skasowane
                Application.Exit();
        }

        private void osbtnResetuj_Click(object sender, EventArgs e)
        {
            osLBG.Clear();
            ospbRysownica.BackColor = Color.White;
            osRysownica.Clear(ospbRysownica.BackColor);
            ospbRysownica.Refresh();
            ostrbWysokośćBryły.Enabled = false;
            ostrbPromieńBryły.Enabled = false;
            osnudStopieńWielokąta.Enabled = false;
            ostrbKątPochylenia.Enabled = false;
            ospnSlajder.Enabled = false;
            osgbKryteriaPokazu.Enabled = false;
            osZegarObrotu.Enabled = false;
            osrdbAutomatyczny.Checked = true;
            osrdbWysokość.Checked = true;
            osPióro.Color = Color.Black;
            osPióro.Width = 1F;
            osPióro.DashStyle = DashStyle.Solid;
            osWykreslenieWziernikaLinii();
        }

        private void ostrbCzasEkspozycji_Scroll(object sender, EventArgs e)
        {
            osZegarSlajdera.Interval = ostrbCzasEkspozycji.Value;
        }


        //deklaracja metody pomocniczej
        void osWykreslenieWziernikaLinii()
        {
            const int osOdstęp = 5;
            //wyczyszczenie powierzchni
            osPowierzcniaGraficznaWziernikaLinii.Clear(ospbWiernikLinii.BackColor);
            //wykreślenie linii "wzorcowej"
            osPowierzcniaGraficznaWziernikaLinii.DrawLine(osPióro, osOdstęp, ospbWiernikLinii.Height / 2, ospbWiernikLinii.Width - osOdstęp, ospbWiernikLinii.Height / 2);
            //odśiweżenie powierzchni
            ospbWiernikLinii.Refresh();
        }
    }
}
