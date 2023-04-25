using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projekt3_Shulga58686
{
    
    public class osBrylyGeometryczne
    {
        const float osKątProsty = 90.0F;
        public abstract class osBryłaAbstrakcyjna
        {
            /*deklaracja typu wyliczeniowego, którego elementy będą"znacznikami"
            wpisywanymi w egzemplarzu każdej bryły do pola rodzaj bryły*/
            public enum osTypyBrył
            {osBG_Walec, osBG_Stożek, osBG_Kula, osBG_Ostrosłup, osBG_Graniastosłup, osBG_Sześcian, osBG_StożekPochylony, osBG_StożekDwustronny, 
                osBG_OstrosłupDwustronny, osBG_OstrosłupPochylony, osBG_GraniastosłupPochylony, osBG_StożekŚcięty, osBG_StożekWalec, osBG_WalecPochylony};
            protected osTypyBrył osRodzajBryły;
            //deklaracja zmiennych dla współnych atrybutów geometrycznych
            protected int osXsP, osYsP;
            protected int osWysokośćBryły;
            protected float osKątPochylenia;
            //deklaracja zmiennych dla współnych atrybutów graficznych
            protected Color osKolorLinii;
            protected DashStyle osStylLinii;
            protected int osGrubośćLinii;
            protected bool osKierunekObrotu; //false : w prawo, true : w lewo
            protected float osPowierzchniaBryły;
            protected float osObjętośćBryły;
            protected bool osWidoczny;
            protected int osPromieńBryły;

            //deklaracja konstruktora
            public osBryłaAbstrakcyjna(Color osKolorLinii, DashStyle osStylLinii, int osGrubośćLinii)
            {
                this.osKolorLinii = osKolorLinii;
                this.osStylLinii = osStylLinii;
                this.osGrubośćLinii = osGrubośćLinii;
                osKątPochylenia = osKątProsty;
            }
            //deklaracja metod abstrakcyjnych
            public abstract void osWykreśl(Graphics osRysownica);
            public abstract void osWymaż(Control osKontrolka, Graphics osRysownica);
            public abstract void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu);
            public abstract void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY);
            //deklaracja metod publicznych z ich pełną implementacją
            public void osUstalAtrybutyGraficzne(Color osKolorLinii, DashStyle osStylLinii, int osGrubośćLinii)
            {
                this.osKolorLinii = osKolorLinii;
                this.osStylLinii = osStylLinii;
                this.osGrubośćLinii = osGrubośćLinii;
            }
            public void osZmianaKierunkuObrotu(bool osKierunek)
            {
                osKierunekObrotu = osKierunek;
            }
            public int osWysokość()
            {
                return osWysokośćBryły;
            }
            public int osPromień()
            {
                return osPromieńBryły;
            }
            public float osPolePowierzchni()
            {
                return osPowierzchniaBryły;
            }
            public float osObjętość()
            {
                return osObjętośćBryły;
            }

        }
        //deklaracja klasy BrułyOsbrotowe
        public class osBryłyObrotowe : osBryłaAbstrakcyjna
        {
            //protected int osPromieńBryły;
            //deklaracja konstruktora
            public osBryłyObrotowe(int osR, Color osKolorLinii, DashStyle osStylLinii, int osGrubośćLinii) : base(osKolorLinii, osStylLinii, osGrubośćLinii)
            {
                //zapisanie (przechowanie) promienia
                osPromieńBryły = osR;
            }
            //nadpisanie wszystkich metod abstrakcyjnych
            public override void osWykreśl(Graphics osRysownica)
            {
                
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                
            }
            
        }//od osBryłyObrotowe
        //deklaracja klasy potomnej osWalec
        public class osWalec : osBryłyObrotowe
        {
            //deklaracje uzupełniające
            protected Point[] osWielokątPodłogi; //podstawy walca
            protected Point[] osWielokątSufitu; //sufitu walca
            protected int osXsS, osYsS;
            //stopień wielokąt podstawy i sufitu walca
            protected int osStopieńWielokątaPodstawy;
            protected float osOśDuża, osOśMała;
            //kąt środkowy między wierzchołkami wielokąta podstawy
            protected float osKątMiędzyWierzchołkami;
            //kąt położenia pierwszego wierzchołka podstawy
            protected float osKątPołożenia;
            
            
            //deklaracja konstruktora
            public osWalec(int osR, int osWysokośćWalca, int osStopieńWielokąta, int osX, int osY, /*float osKątPochyl,*/ Color osKolorLinii, DashStyle osStylLinii, int osGrubośćLinii) : base(osR, osKolorLinii, osStylLinii, osGrubośćLinii)
            {
                //ustawienie rodzaju bryły
                osRodzajBryły =  osTypyBrył.osBG_Walec;
                osWysokośćBryły = osWysokośćWalca;
                osStopieńWielokątaPodstawy = osStopieńWielokąta;
                //wyznaczenie osi elipsy
                osOśDuża = 2 * osPromieńBryły;
                osOśMała = osPromieńBryły / 2;
                osKierunekObrotu = false;
                osXsP = osX;
                osYsP = osY;
                osXsS = osX;
                osYsS = osY - osWysokośćWalca;
                //wyznaczenie kątów  położenia
                osKątMiędzyWierzchołkami = 360 / osStopieńWielokątaPodstawy;
                osKątPołożenia = 0F;
                //wyznaczenie współrzędnych w "podłodze" i "suficie" walca dla wykreślenia prążków na ścianie bocznej walca
                osWielokątPodłogi = new Point[osStopieńWielokątaPodstawy + 1];
                osWielokątSufitu = new Point[osStopieńWielokątaPodstawy + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątSufitu[osi] = new Point();
                    //"podłoga"
                    osWielokątPodłogi[osi].X = (int)(osX + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);
                    osWielokątPodłogi[osi].Y = (int)(osY + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);
                    //"sufit"
                    osWielokątSufitu[osi].X = (int)(osXsS + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);
                    osWielokątSufitu[osi].Y = (int)(osYsS + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);

                }
                //obliczenie powierzchni zewnetrznej walca
                float osPo = (float)Math.PI * osR * osR;
                osPowierzchniaBryły = (float)(2 * osPo + 2 * Math.PI * osR * osWysokośćBryły);                
                //obliczenie objętości walca
                osObjętośćBryły = osPo * osWysokośćWalca;
            }
            //nadpisanie metod abstrakcyjnych z klasy bryłaAbst
            public override void osWykreśl(Graphics osRysownica)
            {
                //utworzenie i sformatowanie pióra
                using(Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    //ustalenie stylu linii
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi walca
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie sufitu walca
                    
                    osRysownica.DrawEllipse(osPióro, osXsS - osOśDuża / 2, osYsS - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie krawedzi bocznych
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS - osOśDuża / 2, osYsS);
                    //wykresleni eprawej kraw bocznej
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS + osOśDuża / 2, osYsS);
                    //wykreslenie prążków
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, 0.5F))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    }
                    osWidoczny = true;
                }//koniec using i zwolnienie pióra
            }//os osWykreśl
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                //utworzenie i sformatowanie pióra
                using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                {
                    //ustalenie stylu linii
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi walca
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie sufitu walca

                    osRysownica.DrawEllipse(osPióro, osXsS - osOśDuża / 2, osYsS - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie krawedzi bocznych
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS - osOśDuża / 2, osYsS);
                    //wykresleni eprawej kraw bocznej
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS + osOśDuża / 2, osYsS);
                    //wykreslenie prążków
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, 0.5F))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    }
                    osWidoczny = false;
                }//koniec using i zwolnienie pióra   
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                //wymazanie bryły walec w aktualnym jej położeniu
                if(osWidoczny)
                  osWymaż(osKontrolka, osRysownica);
                //wyznaczenie nowego położenia pierwszego wierzchołka
                if (osKierunekObrotu)
                    osKątPołożenia -= osKątObrotu;
                else
                    osKątPołożenia += osKątObrotu;
                //wyznaczenie nowych współrzędnych wierzchołków wielokąta podłogi oraz sufitu
                for(int osi = 0; osi<osStopieńWielokątaPodstawy; osi++)
                {
                    //wierzchołki wielokąta podłogi
                    osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));
                    osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));
                    //wierzchołki wielokąta sufitu
                    osWielokątSufitu[osi].X = (int)(osXsS + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));
                    osWielokątSufitu[osi].Y = (int)(osYsS + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));

                }
                //wykreślenie bryła walec po obrocie
                osWykreśl(osRysownica);
            }//os obróć - i - wykredśl
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                //deklaracje pomocnicze
                int osdX, osdY;
                //wymazanie bryły walec
                if (osWidoczny)
                    osWymaż(osKontrolka, osRysownica);
                //wyznaczenie przyrostów zmian współrzędnej X oraz Y
                osdX = osXsP < osX ? osdX = osX - osXsP : osdX = -(osXsP - osX);
                osdY = osYsP < osY? osdY = osY - osYsP : osdY = -(osYsP - osY);
                //ustalenie nowego położenia dla "środków" podłogi i sufitu
                osXsP += osdX;
                osYsP += osdY;
                osXsS += osdX;
                osYsS += osdY;
                //wyznaczenie nowego położenia wierzchołków wielokąta podłogi i sufitu
                for(int osi=0; osi<osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point(osWielokątPodłogi[osi].X + osdX, osWielokątPodłogi[osi].Y + osdY);
                    osWielokątSufitu[osi] = new Point(osWielokątSufitu[osi].X + osdX, osWielokątSufitu[osi].Y + osdY);
                }
                //wykreślenie bryły walec w nowym położeniu
                osWykreśl(osRysownica);
            }

        }//od osWalec
        public class osWalecPochylony : osWalec
        {
            //deklaracja konstruktora
            public osWalecPochylony(int osR, int osWysokość, int osStopieńWielokąta, int osXsP, int osYsP, float osKątPochyleniaWalca,
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osWysokość, osStopieńWielokąta, osXsP, osYsP,
                osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_WalecPochylony;
                osWidoczny = false;
                osKierunekObrotu = false;
                //wyznaczenie wierzchołka stożka
                osXsS = osXsP + (int)(osWysokość / Math.Tan(Math.PI * osKątPochyleniaWalca / 180F));
                osYsS = osYsP - osWysokość;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątMiędzyWierzchołkami = 360 / osStopieńWielokąta;
                osKątPołożenia = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątSufitu[osi] = new Point();
                    //"podłoga"
                    osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);
                    osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);
                    //"sufit"
                    osWielokątSufitu[osi].X = (int)(osXsS + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);
                    osWielokątSufitu[osi].Y = (int)(osYsS + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami)) / 180F);

                }
                //obliczenie powierzchni zewnetrznej walca
                float osPo = (float)Math.PI * osR * osR;
                osPowierzchniaBryły = (float)(2 * osPo + 2 * Math.PI * osR * osWysokośćBryły);
                //obliczenie objętości walca
                osObjętośćBryły = osPo * osWysokośćBryły;

            }
            public override void osWykreśl(Graphics osRysownica)
            {
                //utworzenie i sformatowanie pióra
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    //ustalenie stylu linii
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi walca
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie sufitu walca
                    osRysownica.DrawEllipse(osPióro, osXsS - osOśDuża / 2, osYsS - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie krawedzi bocznych
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS - osOśDuża / 2, osYsS);
                    //wykresleni eprawej kraw bocznej
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS + osOśDuża / 2, osYsS);
                    //wykreslenie prążków
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width/3.0F))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    }
                    osWidoczny = true;
                }//koniec using i zwolnienie pióra
            }//os osWykreśl
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if(osWidoczny)
                using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                {
                    //ustalenie stylu linii
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi walca
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie sufitu walca
                    osRysownica.DrawEllipse(osPióro, osXsS - osOśDuża / 2, osYsS - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie krawedzi bocznych
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS - osOśDuża / 2, osYsS);
                    //wykresleni eprawej kraw bocznej
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS + osOśDuża / 2, osYsS);
                    //wykreslenie prążków
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3.0F))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    }
                    osWidoczny = false;
                }//koniec using i zwolnienie pióra
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                //wymazanie bryły waleca w aktualnym położeniu
                if (osWidoczny)
                    osWymaż(osKontrolka, osRysownica);
                //wyznaczenie nowego położenia pierwszego wierzchołka
                if (osKierunekObrotu)
                    osKątPołożenia -= osKątObrotu;
                else
                    osKątPołożenia += osKątObrotu;
                //wyznaczenie nowych współrzędnych wierzchołków wielokąta podłogi oraz sufitu
                for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                {
                    //wierzchołki wielokąta podłogi
                    osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));
                    osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));
                    //wierzchołki wielokąta sufitu
                    osWielokątSufitu[osi].X = (int)(osXsS + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));
                    osWielokątSufitu[osi].Y = (int)(osYsS + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożenia + osi * osKątMiędzyWierzchołkami) / 180));

                }
                //wykreślenie waleca po obrocie
                osWykreśl(osRysownica);
            }//os obróć - i - wykredśl
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                //deklaracje pomocnicze
                int osdX, osdY;
                //wymazanie bryły walec
                if (osWidoczny)
                    osWymaż(osKontrolka, osRysownica);
                //wyznaczenie przyrostów zmian współrzędnej X oraz Y
                osdX = osXsP < osX ? osdX = osX - osXsP : osdX = -(osXsP - osX);
                osdY = osYsP < osY ? osdY = osY - osYsP : osdY = -(osYsP - osY);
                //ustalenie nowego położenia dla "środków" podłogi i sufitu
                osXsP += osdX;
                osYsP += osdY;
                osXsS += osdX;
                osYsS += osdY;
                //wyznaczenie nowego położenia wierzchołków wielokąta podłogi i sufitu
                for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point(osWielokątPodłogi[osi].X + osdX, osWielokątPodłogi[osi].Y + osdY);
                    osWielokątSufitu[osi] = new Point(osWielokątSufitu[osi].X + osdX, osWielokątSufitu[osi].Y + osdY);
                }
                //wykreślenie bryły walec w nowym położeniu
                osWykreśl(osRysownica);
            }
        }
        public class osStożek : osBryłyObrotowe
        {
            protected int osXsS, osYsS;//wierzchołek stożka
            protected int osStopieńWielokątaPodstawy;
            //osie elipsy
            protected float osOśDuża, osOśMała;
            //kąt rodkowy między wierzchołkami wielokąta podstawy stożka
            protected float osKątMiędzyWierzchołkami;
            protected float osKątPołożeniaPierwszegoWierzchołka;
            //deklaracja zmiennej tablicowej
            protected Point[] osWielokątPodłogi;
            //deklaracja konstruktora
            public osStożek(int osR, int osWysokość, int osStopieńWielokąta, int osXsP, int osYsP, 
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_Stożek;
                //osWidoczny = false;
                osWidoczny = true; ;
                osKierunekObrotu = false;
                osStopieńWielokątaPodstawy = osStopieńWielokąta;
                osWysokośćBryły = osWysokość;
                this.osXsP = osXsP;
                this.osYsP = osYsP;
                osXsS = osXsP;
                osYsS = osYsP - osWysokość;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątMiędzyWierzchołkami = 360 / osStopieńWielokąta;
                
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta+1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                   
                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    //MessageBox.Show()
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                }
                //obliczenie powierzchni stożka
                osPowierzchniaBryły = (float)(Math.PI * osR * (osR + Math.Sqrt(osWysokość * osWysokość + osR * osR)));
                //obliczenie objętości stożka
                osObjętośćBryły = (float)Math.PI * osR * osR * osWysokość/3;
            }
            //nadpisanie metod abstrakcyjnych, które zostały zadeklarowanie w klasie BryłyAbstrakcyjne
            public override void osWykreśl(Graphics osRysownica)
            {
                using(Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podstawy stożka
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie prążków na ścianie bocznej
                    using(Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width/3.0F))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS, osYsS) /*new Point(50, 50)*/);
                    }
                    //wykreślenie krawędzi bocznych stożka
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS, osYsS);
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);

                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if (osWidoczny)
                {
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        osPióro.DashStyle = osStylLinii;
                        //wykreślenie podstawy stożka
                        osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                        //wykreślenie prążków na ścianie bocznej
                        using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3.0F))
                        {
                            for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                                osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS, osYsS) /*new Point(50, 50)*/);
                        }
                        //wykreślenie krawędzi bocznych stożka
                        osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS, osYsS);
                        osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);

                        osWidoczny = false;
                    }
                }
                    
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                //wymazanie bryły walec w aktualnym jej położeniu
                if (osWidoczny)
                    osWymaż(osKontrolka, osRysownica);
                //wyznaczenie nowego kąta położenia dla pierwszego wierzchołka wielokąta
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else
                        osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    //wyznaczenie nowych współrzędnych połorzenia wierzchołków wielokąta podstawy
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                    {
                        osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    //wykreślenie stożka po obrocie
                    osWykreśl(osRysownica);
                
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    int osdX, osdY;
                    osWymaż(osKontrolka, osRysownica);
                    //wyznaczenie przyrostów
                    osdX = osXsP < osX ? osX - osXsP : -(osXsP - osX);
                    osdY = osYsP < osY ? osY - osYsP : -(osYsP - osY);
                    //usalenie nowej lokalizacji
                    osXsP = osXsP + osdX;
                    osYsP = osYsP + osdY;
                    osXsS += osdX;
                    osYsS += osdY;
                    //wyznaczenie nowych współrzędnych wielokąta podstawy
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    //wykreślenie stożka w nowej lokalizacji
                    osWykreśl(osRysownica);
                }
            }
        }
        public class osStożekDwustronny : osStożek
        {
            //deklaracje uzupełniające
            protected int osXsS2; protected int osYsS2;//dla przechowania współrzędnych drugiego wierzchołka
            protected int osWysokośćDolnejCzęści;
            //deklaracja konstruktora
            public osStożekDwustronny(int osR, int osWysokość, int osWysokość2, int osStopieńWielokąta, int osXsP, int osYsP,
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osWysokość, osStopieńWielokąta, osXsP, osYsP,
                osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_StożekDwustronny;
                osWidoczny = false;
                osKierunekObrotu = false;
                osWysokośćDolnejCzęści = osWysokość2;
                //wyznaczenie pierwszego wierzchołka stożka
                osXsS = osXsP;
                osYsS = osYsP - (osWysokość - osWysokość2);
                //wyznaczenie drugiego wierzchołka stożka
                osXsS2 = osXsP;
                osYsS2 = osYsP + osWysokość2;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątMiędzyWierzchołkami = 360 / osStopieńWielokąta;

                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                }
                //obliczenie powierzchni stożka dwustronnego
                osPowierzchniaBryły = (float)(Math.PI * osR * Math.Sqrt((osWysokość - osWysokość2) * (osWysokość - osWysokość2) + osR * osR) +
                    Math.PI * osR * Math.Sqrt(osWysokość2 * osWysokość2 + osR * osR));
                //obliczenie objętości stożka dwustronnego
                osObjętośćBryły = (float)(Math.PI * osR * osR * (osWysokość - osWysokość2) + Math.PI * osR * osR * osWysokość2) / 3.0F;
                

            }
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podstawy stożka
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie prążków na ścianie bocznej
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3.0F))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        {
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS2, osYsS2));
                        }
                    }
                    //wykreślenie krawędzi bocznych stożka dwustronnego
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS, osYsS);
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS2, osYsS2);
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS2, osYsS2);

                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if (osWidoczny)
                {
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        osPióro.DashStyle = osStylLinii;
                        //wykreślenie podstawy stożka
                        osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                        //wykreślenie prążków na ścianie bocznej
                        using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3.0F))
                        {
                            for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            {
                                osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                                osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS2, osYsS2));
                            }
                        }
                        //wykreślenie krawędzi bocznych stożka
                        osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS, osYsS);
                        osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);
                        osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS2, osYsS2);
                        osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS2, osYsS2);
                        osWidoczny = false;
                    }
                }

            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                //wymazanie bryły walec w aktualnym jej położeniu
                if (osWidoczny)
                    osWymaż(osKontrolka, osRysownica);
                //wyznaczenie nowego kąta położenia dla pierwszego wierzchołka wielokąta
                if (osKierunekObrotu)
                    osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                else
                    osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                //wyznaczenie nowych współrzędnych położenia wierzchołków wielokąta podstawy
                for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                }
                //wykreślenie stożka po obrocie
                osWykreśl(osRysownica);

            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    int osdX, osdY;
                    osWymaż(osKontrolka, osRysownica);
                    //wyznaczenie przyrostów
                    osdX = osXsP < osX ? osX - osXsP : -(osXsP - osX);
                    osdY = osYsP < osY ? osY - osYsP : -(osYsP - osY);
                    //ustalenie nowej lokalizacji
                    osXsP = osXsP + osdX;
                    osYsP = osYsP + osdY;
                    osXsS += osdX;
                    osYsS += osdY;
                    osXsS2 += osdX;
                    osYsS2 += osdY;
                    //wyznaczenie nowych współrzędnych wielokąta podstawy
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    //wykreślenie stożka w nowej lokalizacji
                    osWykreśl(osRysownica);
                }
            }

        }
        public class osStożekPochylony : osStożek
        {
            //deklaracja konstruktora
            public osStożekPochylony(int osR, int osWysokość, int osStopieńWielokąta, int osXsP, int osYsP, float osKątPochyleniaStożka,
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osWysokość, osStopieńWielokąta, osXsP, osYsP,
                osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_StożekPochylony;
                //osWidoczny = false;
                osWidoczny = false;
                osKierunekObrotu = false;
                //wyznaczenie wierzchołka stożka
                osXsS = osXsP + (int)(osWysokość / Math.Tan(Math.PI * osKątPochyleniaStożka / 180F));
                osYsS = osYsP - osWysokość;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątMiędzyWierzchołkami = 360 / osStopieńWielokąta;

                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();

                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                }
                //obliczenie powierzchni stożka
                osPowierzchniaBryły = (float)(Math.PI * osR * (osR + Math.Sqrt(osWysokość * osWysokość + osR * osR)));
                //obliczenie objętości stożka
                osObjętośćBryły = (float)Math.PI * osR * osR * osWysokość / 3;

            }
            //nadpisanie metod abstrakcyjnych, które zostały zadeklarowanie w klasie BryłyAbstrakcyjne
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podstawy stożka
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie prążków na ścianie bocznej
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3.0F))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS, osYsS) /*new Point(50, 50)*/);
                    }
                    //wykreślenie krawędzi bocznych stożka
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS, osYsS);
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);

                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if (osWidoczny)
                {
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        osPióro.DashStyle = osStylLinii;
                        //wykreślenie podstawy stożka
                        osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                        //wykreślenie prążków na ścianie bocznej
                        using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3.0F))
                        {
                            for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                                osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], new Point(osXsS, osYsS) /*new Point(50, 50)*/);
                        }
                        //wykreślenie krawędzi bocznych stożka
                        osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS, osYsS);
                        osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);

                        osWidoczny = false;
                    }
                }

            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                //wymazanie bryły walec w aktualnym jej położeniu
                if (osWidoczny)
                    osWymaż(osKontrolka, osRysownica);
                //wyznaczenie nowego kąta położenia dla pierwszego wierzchołka wielokąta
                if (osKierunekObrotu)
                    osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                else
                    osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                //wyznaczenie nowych współrzędnych połorzenia wierzchołków wielokąta podstawy
                for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                }
                //wykreślenie stożka po obrocie
                osWykreśl(osRysownica);

            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    int osdX, osdY;
                    osWymaż(osKontrolka, osRysownica);
                    //wyznaczenie przyrostów
                    osdX = osXsP < osX ? osX - osXsP : -(osXsP - osX);
                    osdY = osYsP < osY ? osY - osYsP : -(osYsP - osY);
                    //usalenie nowej lokalizacji
                    osXsP = osXsP + osdX;
                    osYsP = osYsP + osdY;
                    osXsS += osdX;
                    osYsS += osdY;
                    //wyznaczenie nowych współrzędnych wielokąta podstawy
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    //wykreślenie stożka w nowej lokalizacji
                    osWykreśl(osRysownica);
                }
            }
        }
        public class osStożekŚcięty : osStożek
        {
            float osOśDuża2, osOśMała2;
            Point[] osWielokątSufitu;
            //deklaracja konstruktora
            public osStożekŚcięty(int osR, int osWysokość, int osWysokośćŚcięta, int osStopieńWielokąta, int osXsP, int osYsP,
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osWysokość, osStopieńWielokąta, osXsP, osYsP,
                osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_StożekŚcięty;
                //osWidoczny = false;
                osWidoczny = false;
                osKierunekObrotu = false;
                //wyznaczenie wierzchołka stożka
                osXsS = osXsP;
                osYsS = osYsP - osWysokośćŚcięta;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie osi dużej i osi małej
                osOśDuża2 = osOśDuża * (osWysokośćBryły - osWysokośćŚcięta) / osWysokośćBryły;
                osOśMała2 = osOśMała * (osWysokośćBryły - osWysokośćŚcięta) / osWysokośćBryły;
                float osR2 = osOśMała2 * 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątMiędzyWierzchołkami = 360 / osStopieńWielokąta;
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta + 1];
                osWielokątSufitu = new Point[osStopieńWielokąta + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    //podłoga
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    //sufit
                    osWielokątSufitu[osi] = new Point();
                    osWielokątSufitu[osi].X = (int)(this.osXsS + osOśDuża2 / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątSufitu[osi].Y = (int)(this.osYsS + osOśMała2 / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));

                }
                //obliczenie powierzchni stożka
                double osPo = Math.PI * (osR * osR + osR2 * osR2);
                double osPb1 = Math.PI * osR * Math.Sqrt(osR * osR + osWysokość * osWysokość);
                double osPb2 = Math.PI * osR2 * Math.Sqrt(osR2 * osR2 + (osWysokość - osWysokośćŚcięta) * (osWysokość - osWysokośćŚcięta));
                osPowierzchniaBryły = (float)(osPo + osPb1 - osPb2);
                //obliczenie objętości stożka
                osObjętośćBryły = (float)(Math.PI / 3.0F * (osR * osR * osWysokość - osR2 * osR2 * (osWysokość - osWysokośćŚcięta)));
            }
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podstawy stożka
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    osRysownica.DrawEllipse(osPióro, osXsS - osOśDuża2 / 2, osYsS - osOśMała2 / 2, osOśDuża2, osOśMała2);
                    //wykreślenie prążków na ścianie bocznej
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width/3))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    }
                    //wykreślenie krawędzi bocznych stożka
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS - osOśDuża2/2, osYsS );
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS + osOśDuża2/2, osYsS);

                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if(osWidoczny)
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podstawy stożka
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    osRysownica.DrawEllipse(osPióro, osXsS - osOśDuża2 / 2, osYsS - osOśMała2 / 2, osOśDuża2, osOśMała2);
                    //wykreślenie prążków na ścianie bocznej
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    }
                    //wykreślenie krawędzi bocznych stożka
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS - osOśDuża2 / 2, osYsS);
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS + osOśDuża2 / 2, osYsS);

                    osWidoczny = false;
                }
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else
                        osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        //sufit
                        osWielokątSufitu[osi].X = (int)(this.osXsS + osOśDuża2 / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątSufitu[osi].Y = (int)(this.osYsS + osOśMała2 / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    osWykreśl(osRysownica);
                }   
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    int osdX, osdY;
                    osWymaż(osKontrolka, osRysownica);
                    //wyznaczenie przyrostów
                    osdX = osXsP < osX ? osX - osXsP : -(osXsP - osX);
                    osdY = osYsP < osY ? osY - osYsP : -(osYsP - osY);
                    //usalenie nowej lokalizacji
                    osXsP = osXsP + osdX;
                    osYsP = osYsP + osdY;
                    osXsS += osdX;
                    osYsS += osdY;
                    //wyznaczenie nowych współrzędnych wielokąta podstawy
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        //sufit
                        osWielokątSufitu[osi].X = (int)(this.osXsS + osOśDuża2 / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątSufitu[osi].Y = (int)(this.osYsS + osOśMała2 / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    //wykreślenie stożka w nowej lokalizacji
                    osWykreśl(osRysownica);
                }
            }
        }
        
        public class osKula : osBryłyObrotowe
        {
            float osOśDuża, osOśMała;
            float osPrzesunięcieObręczy;
            float osKątPołożeniaObręczy;
            //deklaracja konstruktora
            public osKula(int osR,int osXsP, int osYsP, Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_Kula;
                osWidoczny = false;
                osKierunekObrotu = false;
                osWysokośćBryły = 2 * osR;
                this.osXsP = osXsP; this.osYsP = osYsP;
                osOśDuża = osR * 2;
                osOśMała = osR / 2;
                osKątPołożeniaObręczy = osPrzesunięcieObręczy = 0;
                //pole powierzchni
                osPowierzchniaBryły = 4.0F * (float)Math.PI * osR * osR;
            }
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie okręgu
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśDuża / 2, osOśDuża, osOśDuża);
                    //wykreślenie przekroju
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);

                    //wykreślenie obręczy
                    using (Pen osPióroObręczy = new Pen(osPióro.Color, osPióro.Width/3))
                        osRysownica.DrawEllipse(osPióroObręczy, osPrzesunięcieObręczy / 2 + osXsP - osOśDuża / 2, osYsP - osOśMała * 2, osOśDuża - osPrzesunięcieObręczy, osOśDuża);
                    osWidoczny = true;
                }    
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if (osWidoczny)
                {
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        osPióro.DashStyle = osStylLinii;
                        //wykreślenie okręgu
                        osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśDuża / 2, osOśDuża, osOśDuża);
                        //wykreślenie przekroju
                        osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                        //wykreślenie obręczy
                        using (Pen osPióroObręczy = new Pen(osPióro.Color, osPióro.Width/3))
                            osRysownica.DrawEllipse(osPióroObręczy, osPrzesunięcieObręczy / 2 + osXsP - osOśDuża / 2, osYsP - osOśMała * 2, osOśDuża - osPrzesunięcieObręczy, osOśDuża);
                    }
                        osWidoczny = false;
                }
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                if (osWidoczny)
                {
                    this.osWymaż(osKontrolka, osRysownica);
                    osKątPołożeniaObręczy = (osKątPołożeniaObręczy + osKątObrotu) % 360;
                    osPrzesunięcieObręczy = (int)(osKątPołożeniaObręczy % osOśDuża) * 2;
                    this.osWykreśl(osRysownica);
                }
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    this.osWymaż(osKontrolka, osRysownica);
                    osXsP = osX; osYsP = osY;
                    this.osWykreśl(osRysownica);
                }
            }
        }
        public class osStożekWalec : osStożekDwustronny
        {
            Point[] osWielokątSufitu;
            int osWysokośćWalca;
            public osStożekWalec(int osR, int osWysokość, int osWysokość2, int osStopieńWielokąta, int osXsP, int osYsP,
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osWysokość, osWysokość2, osStopieńWielokąta, osXsP, osYsP,
                osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_StożekWalec;
                osWidoczny = false;
                osKierunekObrotu = false;
                osWysokośćWalca = osWysokość2;
                //wyznaczenie pierwszego wierzchołka
                osXsS = osXsP;
                osYsS = osYsP - (int)(osWysokość - osWysokość2);
                //wyznaczenie drugiego wierzchołka
                osXsS2 = osXsP;
                osYsS2 = osYsP + osWysokość2;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątMiędzyWierzchołkami = 360 / osStopieńWielokąta;
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta + 1];
                osWielokątSufitu = new Point[osStopieńWielokąta + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątSufitu[osi] = new Point();
                    osWielokątSufitu[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątSufitu[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątPodłogi[osi].X = (int)(this.osXsS2 + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsS2 + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));

                }
                //obliczenie pola powierzchni 
                float osPo = (float)Math.PI * osR * osR;
                osPowierzchniaBryły = (float)(Math.PI * osR * Math.Sqrt((osWysokość - osWysokość2) * (osWysokość - osWysokość2) + osR * osR) + 2 * osPo + 2 * Math.PI * osR * osWysokość2);
                //obliczenie objętości stożka dwustronnego
                osObjętośćBryły = (float)Math.PI * osR * osR * (osWysokość - osWysokość2) / 3.0F + osPo * osWysokość2;
                

            }
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podstawy stożka
                    osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                    osRysownica.DrawEllipse(osPióro, osXsS2 - osOśDuża / 2, osYsS2 - osOśMała / 2, osOśDuża, osOśMała);
                    //wykreślenie prążków na ścianie bocznej
                    using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3))
                    {
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        {
                            osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                            osRysownica.DrawLine(osPióroPrążków, osWielokątSufitu[osi], new Point(osXsS, osYsS));
                        }
                    }
                    //wykreślenie krawędzi bocznych 
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża /2, osYsP, osXsS, osYsS);
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);
                    osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS2 + osOśDuża/2, osYsS2);
                    osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS2 - osOśDuża / 2, osYsS2);
                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if(osWidoczny)
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        osPióro.DashStyle = osStylLinii;
                        //wykreślenie podstawy stożka
                        osRysownica.DrawEllipse(osPióro, osXsP - osOśDuża / 2, osYsP - osOśMała / 2, osOśDuża, osOśMała);
                        osRysownica.DrawEllipse(osPióro, osXsS2 - osOśDuża / 2, osYsS2 - osOśMała / 2, osOśDuża, osOśMała);
                        //wykreślenie prążków na ścianie bocznej
                        using (Pen osPióroPrążków = new Pen(osPióro.Color, osPióro.Width / 3))
                        {
                            for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            {
                                osRysownica.DrawLine(osPióroPrążków, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                                osRysownica.DrawLine(osPióroPrążków, osWielokątSufitu[osi], new Point(osXsS, osYsS));
                            }
                        }
                        //wykreślenie krawędzi bocznych 
                        osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS, osYsS);
                        osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS, osYsS);
                        osRysownica.DrawLine(osPióro, osXsP + osOśDuża / 2, osYsP, osXsS2 + osOśDuża / 2, osYsS2);
                        osRysownica.DrawLine(osPióro, osXsP - osOśDuża / 2, osYsP, osXsS2 - osOśDuża / 2, osYsS2);
                        osWidoczny = false;
                    }
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                //wymazanie bryły walec w aktualnym jej położeniu
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    //wyznaczenie nowego położenia pierwszego wierzchołka
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else
                        osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    //wyznaczenie nowych współrzędnych wierzchołków wielokąta podłogi oraz sufitu
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                    {
                        //wierzchołki wielokąta podłogi
                        osWielokątSufitu[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątSufitu[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        //sufitu
                        osWielokątPodłogi[osi].X = (int)(this.osXsS2 + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsS2 + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    //wykreślenie po obrocie
                    osWykreśl(osRysownica);
                }
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    osXsP = osX; osYsP = osY;
                    osXsS = osXsS2 = osX;
                    osYsS = osY - (osWysokośćBryły - osWysokośćWalca);
                    osYsS2 = osY + osWysokośćWalca;
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                    {
                        //wierzchołki wielokąta podłogi
                        osWielokątSufitu[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątSufitu[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        //sufit
                        osWielokątPodłogi[osi].X = (int)(this.osXsS2 + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(this.osYsS2 + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątMiędzyWierzchołkami) / 180F));
                    }
                    osWykreśl(osRysownica);
                }
            }
        }
       
        public class osWielościany : osBryłaAbstrakcyjna
        {
            //deklaracje zmiennych
            protected Point[] osWielokątPodłogi;
            protected Point[] osWielokątSufitu;
            protected int osStopieńWielokątaPodstawy;
            protected int osXsS, osYsS;
            //protected int osPromieńBryły;
            //deklaracja konstruktora
            public osWielościany(int osR, int osStopieńWielokątaPodstawy, Color osKolorLinii, DashStyle osStylLinii, int osGrubośćLinii) : 
                base(osKolorLinii, osStylLinii, osGrubośćLinii)
            {
                this.osPromieńBryły = osR;
                this.osStopieńWielokątaPodstawy = osStopieńWielokątaPodstawy;
            }
            public override void osWykreśl(Graphics osRysownica)
            {
                throw new NotImplementedException();
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                throw new NotImplementedException();
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                throw new NotImplementedException();
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                throw new NotImplementedException();
            }
        }
        public class osGraniastosłup : osWielościany
        {
            //deklaracje uzupełniające
            protected float osOśDuża, osOśMała;
            protected float osKątŚrodkowyMiędzyWierzchołkami;
            protected float osKątPołożeniaPierwszegoWierzchołka;
            //deklaracja konstruktora
            public osGraniastosłup(int osR, int osWysokośćGraniastosłupa, int osStopieńWielokątaPodstawy, int osXsP, int osYsP, Color osKolorLinii, DashStyle osStylLinii, int osGrubośćLinii) :
                base(osR, osStopieńWielokątaPodstawy, osKolorLinii, osStylLinii, osGrubośćLinii)
            {
                osRodzajBryły = osTypyBrył.osBG_Graniastosłup;
                osWidoczny = false;
                osKierunekObrotu = false;
                osWysokośćBryły = osWysokośćGraniastosłupa;
                this.osStopieńWielokątaPodstawy = osStopieńWielokątaPodstawy;
                //przechowanie współrzędnych środka podstawy
                this.osXsP = osXsP;
                this.osYsP = osYsP;
                //wyznaczenie współrzędnych środka sufitu
                this.osXsS = osXsP;
                this.osYsS = osYsP - osWysokośćBryły;
                //wyznaczenie osi elipsy
                osOśDuża = osR * 2;
                osOśMała = osR / 2;
                //wyznaczenie kątów
                osKątŚrodkowyMiędzyWierzchołkami = 360 / osStopieńWielokątaPodstawy;
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //wyznaczenie współrzędnych punktów
                osWielokątPodłogi = new Point[osStopieńWielokątaPodstawy + 1];
                osWielokątSufitu = new Point[osStopieńWielokątaPodstawy + 1];
                //utworzenie punktów i wpisanie wartości
                for(int osi=0; osi<=osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątSufitu[osi] = new Point();
                    //podłoga
                    osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                    //sufit
                    osWielokątSufitu[osi].X = osWielokątPodłogi[osi].X;
                    osWielokątSufitu[osi].Y = osWielokątPodłogi[osi].Y - osWysokośćGraniastosłupa;
                }
                //pole powierzchni
                double osA = 2.0F * osR * Math.Sin(Math.PI / osStopieńWielokątaPodstawy);
                double osPo = (osStopieńWielokątaPodstawy * osA * osA) / (4.0 * Math.Tan(Math.PI / osStopieńWielokątaPodstawy));
                osPowierzchniaBryły = (float)(osPo * 2.0F + osStopieńWielokątaPodstawy / 2.0 * osA * osWysokośćGraniastosłupa);
                //objętość bryły
                osObjętośćBryły = (float)(osPo * osWysokośćGraniastosłupa);
            }
            //nadpisanie metod abstrakcyjnych
            public override void osWykreśl(Graphics osRysownica)
            {
                using(Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątPodłogi[osi + 1]);
                    //wykreślenie sufitu
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątSufitu[osi], osWielokątSufitu[osi + 1]);
                    //wykreślenie krawędzi bocznych
                    for (int osi = 0; osi<osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if(osWidoczny)
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        osPióro.DashStyle = osStylLinii;
                        //wykreślenie podłogi
                        osRysownica.DrawLines(osPióro, osWielokątPodłogi);
                        //wykreślenie sufitu
                        osRysownica.DrawLines(osPióro, osWielokątSufitu);
                        //wykreślenie krawędzi bocznych
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                        osWidoczny = false;
                    }

            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                if (osWidoczny)
                {
                    this.osWymaż(osKontrolka, osRysownica);
                    //ustalenie nowego kąta położenia pierwszego wierzchołka
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else
                        osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    //wyznaczenie nowego położenia wszystkich wierzchołków
                    for(int osi = 0; osi<= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        //sufit
                        /*osWielokątSufitu[osi].X = (int)(osXsS + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątSufitu[osi].Y = (int)(osYsS + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));*/
                        osWielokątSufitu[osi].X = osWielokątPodłogi[osi].X;
                        osWielokątSufitu[osi].Y = osWielokątPodłogi[osi].Y - osWysokośćBryły;
                    }
                    //wykreślenie graniastosłupa
                    this.osWykreśl(osRysownica);
                }
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    osXsP = osX; osYsP = osY;
                    osXsS = osX; osYsS = osY - osWysokośćBryły;
                    //wyznaczenie wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        //sufit
                        osWielokątSufitu[osi].X = osWielokątPodłogi[osi].X;
                        osWielokątSufitu[osi].Y = osWielokątPodłogi[osi].Y - osWysokośćBryły;
                    }
                    //wykreślenie graniastosłupa
                    this.osWykreśl(osRysownica);
                }
            }
        }
        public class osGraniastosłupPochylony : osGraniastosłup
        {
            //deklaracja konstruktora
            public osGraniastosłupPochylony(int osR, int osWysokość, int osStopieńWielokąta, int osXsP, int osYsP, float osKątPochylenia, 
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osWysokość, osStopieńWielokąta, osXsP, osYsP, osKolor_Linii, osStyl_Linii, osGrubość_Linii) 
            {
                osRodzajBryły = osTypyBrył.osBG_GraniastosłupPochylony;
                //osWidoczny = false;
                osWidoczny = false;
                osKierunekObrotu = false;
                //wyznaczenie wierzchołka stożka
                osXsS = osXsP + (int)(osWysokość / Math.Tan(Math.PI * osKątPochylenia / 180F));
                osYsS = osYsP - osWysokość;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątŚrodkowyMiędzyWierzchołkami = 360 / osStopieńWielokąta;
                this.osKątPochylenia = osKątPochylenia;
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątŚrodkowyMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątŚrodkowyMiędzyWierzchołkami) / 180F));
                    //sufit
                    osWielokątSufitu[osi] = new Point();
                    osWielokątSufitu[osi].X = (int)(this.osXsS + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątŚrodkowyMiędzyWierzchołkami) / 180F));
                    osWielokątSufitu[osi].Y = osWielokątPodłogi[osi].Y - osWysokość;
                }
                //pole powierzchni
                double osA = 2.0F * osR * Math.Sin(Math.PI / osStopieńWielokątaPodstawy);
                double osPo = (osStopieńWielokątaPodstawy * osA * osA) / (4.0 * Math.Tan(Math.PI / osStopieńWielokątaPodstawy));
                osPowierzchniaBryły = (float)(osPo * 2.0F + osStopieńWielokątaPodstawy / 2.0 * osA * osWysokość);
                //objętość bryły
                osObjętośćBryły = (float)(osPo * osWysokość);

            }
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątPodłogi[osi + 1]);
                    //wykreślenie sufitu
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątSufitu[osi], osWielokątSufitu[osi + 1]);
                    //wykreślenie krawędzi bocznych
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if (osWidoczny)
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        //osPióro.DashStyle = osStylLinii;
                        //wykreślenie podłogi
                        osRysownica.DrawLines(osPióro, osWielokątPodłogi);
                        //wykreślenie sufitu
                        osRysownica.DrawLines(osPióro, osWielokątSufitu);
                        //wykreślenie krawędzi bocznych
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątSufitu[osi]);
                        osWidoczny = false;
                    }
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                if (osWidoczny)
                {
                    this.osWymaż(osKontrolka, osRysownica);
                    //ustalenie nowego kąta położenia pierwszego wierzchołka
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else
                        osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    //wyznaczenie nowego położenia wszystkich wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        //sufit
                        osWielokątSufitu[osi].X = (int)(osXsS + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątSufitu[osi].Y = osWielokątPodłogi[osi].Y - osWysokośćBryły;
                    }
                    //wykreślenie graniastosłupa
                    this.osWykreśl(osRysownica);
                }
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    osXsP = osX; osYsP = osY;
                    osXsS = osXsP + (int)(osWysokośćBryły / Math.Tan(Math.PI * osKątPochylenia / 180F));
                    osYsS = osY - osWysokośćBryły;
                    //wyznaczenie wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        //sufit
                        osWielokątSufitu[osi].X = osWielokątPodłogi[osi].X;
                        osWielokątSufitu[osi].Y = osWielokątPodłogi[osi].Y - osWysokośćBryły;
                    }
                    //wykreślenie graniastosłupa
                    this.osWykreśl(osRysownica);
                }
            }
        }
        public class osOstrosłup : osWielościany
        {
            //deklaracje uzupełniające
            protected float osOśDuża, osOśMała;
            protected float osKątŚrodkowyMiędzyWierzchołkami;
            protected float osKątPołożeniaPierwszegoWierzchołka;
            public osOstrosłup(int osR, int osWysokośćOstrosłupa, int osStopieńWielokątaPodstawy, int osXsP, int osYsP, Color osKolorLinii, DashStyle osStylLinii, int osGrubośćLinii) :
                base(osR, osStopieńWielokątaPodstawy, osKolorLinii, osStylLinii, osGrubośćLinii)
            {
                osRodzajBryły = osTypyBrył.osBG_Ostrosłup;
                osWidoczny = false;
                osKierunekObrotu = false;
                osWysokośćBryły = osWysokośćOstrosłupa;
                this.osStopieńWielokątaPodstawy = osStopieńWielokątaPodstawy;
                //przechowanie współrzędnych środka podstawy
                this.osXsP = osXsP;
                this.osYsP = osYsP;
                //wyznaczenie współrzędnych środka sufitu
                this.osXsS = osXsP;
                this.osYsS = osYsP - osWysokośćBryły;
                //wyznaczenie osi elipsy
                osOśDuża = osR * 2;
                osOśMała = osR / 2;
                //wyznaczenie kątów
                osKątŚrodkowyMiędzyWierzchołkami = 360 / osStopieńWielokątaPodstawy;
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //wyznaczenie współrzędnych punktów
                osWielokątPodłogi = new Point[osStopieńWielokątaPodstawy + 1];

                //utworzenie punktów i wpisanie wartości
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    //podłoga
                    osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                }
                //pole powierzchni
                double osA = 2.0F * osR * Math.Sin(Math.PI / osStopieńWielokątaPodstawy);
                double osPo = (osStopieńWielokątaPodstawy * osA * osA) / (4.0 * Math.Tan(Math.PI / osStopieńWielokątaPodstawy));
                osPowierzchniaBryły = (float)(osPo + osStopieńWielokątaPodstawy / 2.0 * osA * Math.Sqrt(osR * osR + osWysokośćOstrosłupa * osWysokośćOstrosłupa));
                //objętość bryły
                osObjętośćBryły = (float)(osPo / 3.0f * osWysokośćOstrosłupa);
            }
                //nadpisanie metod abstrakcyjnych
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi
                    //osRysownica.DrawLines(osPióro, osWielokątPodłogi);
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątPodłogi[osi + 1]);
                    //wykreślenie krawędzi bocznych
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if (osWidoczny)
                    using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                    {
                        osPióro.DashStyle = osStylLinii;
                        //wykreślenie podłogi
                        osRysownica.DrawLines(osPióro, osWielokątPodłogi);
                        //wykreślenie krawędzi bocznych
                        for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                            osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                        osWidoczny = false;
                    }

            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                if (osWidoczny)
                {
                    this.osWymaż(osKontrolka, osRysownica);
                    //ustalenie nowego kąta położenia pierwszego wierzchołka
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else
                        osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    //wyznaczenie nowego położenia wszystkich wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        
                    }
                    //wykreślenie graniastosłupa
                    this.osWykreśl(osRysownica);
                }
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    osXsP = osX; osYsP = osY;
                    osXsS = osX; osYsS = osY - osWysokośćBryły;
                    //wyznaczenie wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        
                    }
                    //wykreślenie graniastosłupa
                    this.osWykreśl(osRysownica);
                }
            }
        }
        public class osOstrosłupPochylony : osOstrosłup
        {
            //deklaracja konstruktora
            public osOstrosłupPochylony(int osR, int osWysokość, int osStopieńWielokąta, int osXsP, int osYsP, float osKątPochyleniaStożka,
                Color osKolor_Linii, DashStyle osStyl_Linii, int osGrubość_Linii) : base(osR, osWysokość, osStopieńWielokąta, osXsP, osYsP,
                osKolor_Linii, osStyl_Linii, osGrubość_Linii)
            {
                osRodzajBryły = osTypyBrył.osBG_OstrosłupPochylony;
                osWidoczny = false;
                osKierunekObrotu = false;
                //wyznaczenie wierzchołka
                osXsS = osXsP + (int)(osWysokość / Math.Tan(Math.PI * osKątPochyleniaStożka / 180F));
                osYsS = osYsP - osWysokość;
                //wyznaczenie osi dużej i osi małej
                osOśDuża = 2 * osR;
                osOśMała = osR / 2;
                //wyznaczenie kąta między wierzchołkami wielokąta
                osKątŚrodkowyMiędzyWierzchołkami = 360 / osStopieńWielokąta;
                osKątPochylenia = osKątPochyleniaStożka;
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //utworzenie egzemplarza tablicy wierzchołków wielokąta podstawy
                osWielokątPodłogi = new Point[osStopieńWielokąta + 1];
                //utworzenie egzemplarzy punktów w podłodze i suficie oraz wpisanie do nich wyznaczonych współrzędnych na obwodzie elipsy
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    osWielokątPodłogi[osi].X = (int)(this.osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątŚrodkowyMiędzyWierzchołkami) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(this.osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osi * osKątŚrodkowyMiędzyWierzchołkami) / 180F));
                }
                //pole powierzchni
                double osA = 2.0F * osR * Math.Sin(Math.PI / osStopieńWielokątaPodstawy);
                double osPo = (osStopieńWielokątaPodstawy * osA * osA) / (4.0 * Math.Tan(Math.PI / osStopieńWielokątaPodstawy));
                osPowierzchniaBryły = (float)(osPo + osStopieńWielokątaPodstawy / 2.0 * osA * Math.Sqrt(osR * osR + osWysokość * osWysokość));
                //objętość bryły
                osObjętośćBryły = (float)(osPo / 3.0f * osWysokość);

            }
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątPodłogi[osi + 1]);
                    //wykreślenie krawędzi bocznych
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                {
                    osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi
                    //osRysownica.DrawLines(osPióro, osWielokątPodłogi);
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątPodłogi[osi + 1]);
                    //wykreślenie krawędzi bocznych
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                    osWidoczny = false;
                }
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                if (osWidoczny)
                {
                    this.osWymaż(osKontrolka, osRysownica);
                    //ustalenie nowego kąta położenia pierwszego wierzchołka
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else
                        osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    //wyznaczenie nowego położenia wszystkich wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                    }
                    //wykreślenie
                    this.osWykreśl(osRysownica);
                }
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    osXsP = osX; osYsP = osY;
                    osXsS = osXsP + (int)(osWysokośćBryły / Math.Tan(Math.PI * osKątPochylenia / 180F)); 
                    osYsS = osY - osWysokośćBryły;
                    //wyznaczenie wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                    }
                    this.osWykreśl(osRysownica);
                }
            }
        }
        public class osOstrosłupDwustronny : osOstrosłup
        {
            //deklaracje uzupełniające
            protected int osXsS2, osYsS2;
            protected int osWysokośćDolnejCzęści;
            public osOstrosłupDwustronny(int osR, int osWysokośćOstrosłupa, int osWysokość2, int osStopieńWielokątaPodstawy, int osXsP, int osYsP, Color osKolorLinii, DashStyle osStylLinii, 
                int osGrubośćLinii) : base(osR, osWysokośćOstrosłupa, osStopieńWielokątaPodstawy, osXsP, osYsP, osKolorLinii, osStylLinii, osGrubośćLinii)
            {
                osRodzajBryły = osTypyBrył.osBG_OstrosłupDwustronny;
                osWidoczny = false;
                osKierunekObrotu = false;
                osWysokośćBryły = osWysokośćOstrosłupa;
                osWysokośćDolnejCzęści = osWysokość2;
                this.osStopieńWielokątaPodstawy = osStopieńWielokątaPodstawy;
                //przechowanie współrzędnych środka podstawy
                this.osXsP = osXsP;
                this.osYsP = osYsP;
                //wyznaczenie współrzędnych środka sufitu
                this.osXsS = osXsP;
                this.osYsS = osYsP - osWysokośćBryły + osWysokośćDolnejCzęści;
                //wyznaczenie współrzędnych środka 
                this.osXsS2 = osXsP;
                this.osYsS2 = osYsP + osWysokośćDolnejCzęści;
                //wyznaczenie osi elipsy
                osOśDuża = osR * 2;
                osOśMała = osR / 2;
                //wyznaczenie kątów
                osKątŚrodkowyMiędzyWierzchołkami = 360 / osStopieńWielokątaPodstawy;
                osKątPołożeniaPierwszegoWierzchołka = 0F;
                //wyznaczenie współrzędnych punktów
                osWielokątPodłogi = new Point[osStopieńWielokątaPodstawy + 1];

                //utworzenie punktów i wpisanie wartości
                for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                {
                    osWielokątPodłogi[osi] = new Point();
                    //podłoga
                    osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                    osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                }
                //pole powierzchni
                double osA = 2.0F * osR * Math.Sin(Math.PI / osStopieńWielokątaPodstawy);
                double osPo = (osStopieńWielokątaPodstawy * osA * osA) / (4.0 * Math.Tan(Math.PI / osStopieńWielokątaPodstawy));
                osPowierzchniaBryły = (float)(osStopieńWielokątaPodstawy / 2.0 * osA * Math.Sqrt(osR * osR + (osWysokośćOstrosłupa - osWysokośćDolnejCzęści) * 
                    (osWysokośćOstrosłupa - osWysokośćDolnejCzęści)) + osStopieńWielokątaPodstawy / 2.0 * osA * Math.Sqrt(osR * osR + osWysokośćDolnejCzęści * osWysokośćDolnejCzęści));
                //objętość bryły
                osObjętośćBryły = (float)(osPo / 3.0f * osWysokośćOstrosłupa);
            }
            public override void osWykreśl(Graphics osRysownica)
            {
                using (Pen osPióro = new Pen(osKolorLinii, osGrubośćLinii))
                {osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątPodłogi[osi + 1]);
                    //wykreślenie krawędzi bocznych
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                    {
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS2, osYsS2));
                    }
                    osWidoczny = true;
                }
            }
            public override void osWymaż(Control osKontrolka, Graphics osRysownica)
            {
                if(osWidoczny)
                using (Pen osPióro = new Pen(osKontrolka.BackColor, osGrubośćLinii))
                {osPióro.DashStyle = osStylLinii;
                    //wykreślenie podłogi
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], osWielokątPodłogi[osi + 1]);
                    //wykreślenie krawędzi bocznych
                    for (int osi = 0; osi < osStopieńWielokątaPodstawy; osi++)
                    {
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS, osYsS));
                        osRysownica.DrawLine(osPióro, osWielokątPodłogi[osi], new Point(osXsS2, osYsS2));
                    }
                    osWidoczny = false;
                }
            }
            public override void osObróć_i_Wykreśl(Control osKontrolka, Graphics osRysownica, float osKątObrotu)
            {
                if (osWidoczny)
                {osWymaż(osKontrolka, osRysownica);
                    //ustalenie nowego kąta położenia pierwszego wierzchołka
                    if (osKierunekObrotu)
                        osKątPołożeniaPierwszegoWierzchołka -= osKątObrotu;
                    else osKątPołożeniaPierwszegoWierzchołka += osKątObrotu;
                    //wyznaczenie nowego położenia wszystkich wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));

                    }
                    //wykreślenie graniastosłupa dwustronnego
                    this.osWykreśl(osRysownica);
                }
            }
            public override void osPrzesuńDoNowegoXY(Control osKontrolka, Graphics osRysownica, int osX, int osY)
            {
                if (osWidoczny)
                {
                    osWymaż(osKontrolka, osRysownica);
                    osXsP = osX; osYsP = osY;  
                    osXsS = osX; osYsS = osY - osWysokośćBryły/2;
                    osXsS2 = osX; osYsS2 = osY + osWysokośćBryły / 2;
                    //wyznaczenie wierzchołków
                    for (int osi = 0; osi <= osStopieńWielokątaPodstawy; osi++)
                    {
                        //podłoga
                        osWielokątPodłogi[osi].X = (int)(osXsP + osOśDuża / 2 * Math.Cos(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));
                        osWielokątPodłogi[osi].Y = (int)(osYsP + osOśMała / 2 * Math.Sin(Math.PI * (osKątPołożeniaPierwszegoWierzchołka + osKątŚrodkowyMiędzyWierzchołkami * osi) / 180F));

                    }
                    //wykreślenie graniastosłupa
                    this.osWykreśl(osRysownica);
                }
            }
        }
    }
}
    
    

