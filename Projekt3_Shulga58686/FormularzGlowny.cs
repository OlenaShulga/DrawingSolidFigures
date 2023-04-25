using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt3_Shulga58686
{
    public partial class FormularzGlowny : Form
    {
       
        public FormularzGlowny()
        {
            InitializeComponent();
            //lokalizacja i zwymiarowanie formularza
            this.Location = new Point(Screen.PrimaryScreen.Bounds.X + (int)((Screen.PrimaryScreen.WorkingArea.Width - this.Width)/2.0F),
                Screen.PrimaryScreen.Bounds.Y + (int)((Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2.0F));
            this.StartPosition = FormStartPosition.Manual;
            //można ustalić wartości innych atrybutów formularza
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //odszukanie formularza osBryłyRegularne w kolekcji OpenForms
            foreach (Form osFormX in Application.OpenForms)
                //sprawdzenie czy "znaleziony" formularz jest osBryłyRegularne
                if (osFormX.Name == "osBrylyRegularne")
                {
                    //ukryci ebieżącego formularza
                    Hide();
                    //odsłonięcie
                    osFormX.Show();
                    //wyjście z metody zdarzenia click
                    return;
                }
            //formularz osBryłyRegularne nie został znaleziony

            osBrylyRegularne osFormLab = new osBrylyRegularne();
            Hide();
            osFormLab.Show();
        }

        private void osbtnPrzejścieDoProjektu_Click(object sender, EventArgs e)
        {
            //odszukanie formularza osBryłyZłożone_Shulga w kolekcji OpenForms
            foreach (Form osFormX in Application.OpenForms)
                //sprawdzenie czy "znaleziony" formularz jest osBryłyZłożone_Shulga
                if (osFormX.Name == "osBryłyZłożone_Shulga")
                {
                    //ukryci ebieżącego formularza
                    Hide();
                    //odsłonięcie
                    osFormX.Show();
                    //wyjście z metody zdarzenia click
                    return;
                }
            //formularz osBryłyZłożone_Shulga nie został znaleziony

            osBrylyZlozone_Shulga osFormLab = new osBrylyZlozone_Shulga();
            Hide();
            osFormLab.Show();
        }
    }
}
