using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Moto_17Ventrucci
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Moto[] catalogo = new Moto[10];
        static int  CountMoto = 0;
        Moto[] moto = new Moto[10];
        TestaMoto m;
        bool ok = false, ok2=false, testaM=false,vis=false;
        
        
        class Moto
        {
            protected string _modello, _motorizzazione;
            protected double _prezzo;
            protected int _cilindrata;
            //(due tempi, quattro tempi, elettrico
            public Moto(string mod, int cil, string mot, double p)
            {
                
            }
            public string mod
            { get { return _modello; } }
            public virtual double Sconto()
            {
                return 0;
            }
            public string mot
            { get { return _motorizzazione; } }
            
            public double prez
            { get { return _prezzo; } }
            public int cil
            { get { return _cilindrata; } }
            
        }
        class Stradali : Moto
        {
            string _carena;//(semicarena, cupolino, integrale, naked
            public Stradali(string mod, int cil, string mot, double p, string car):base(mod, cil,mot,p)
            {
                _modello = mod;
                _cilindrata = cil;
                _motorizzazione = mot;
                _prezzo = p;
                _carena = car;
            }
            public string car
            { get { return _carena; } }
            
            public override double Sconto()
            {
                double s = 0,e;
                //15€ ogni 10cc
                s =  150;
                if(_cilindrata>=250)
                {
                    e = (_cilindrata - 250) / 10;
                    e =e* 15;
                    s = s + e;
                }
                return s;
            }
            public override string ToString()
            {
                
                return "Modello: "+ _modello+ " Motorizzazione: "+ _motorizzazione+ " Cilindrata: "+ _cilindrata+ " Carena: "+ _carena+ " Prezzo: "+ _prezzo+ " Sconto: ";
            }

        }
        class Scooter:Moto
        {
            string _bauletto;//(grande, medio, piccolo, assente). 
            public Scooter(string mod, int cil, string mot, double p, string bau):base(mod, cil,mot,p)
            {
                _modello = mod;
                _cilindrata = cil;
                _motorizzazione = mot;
                _prezzo = p;
                _bauletto = bau;
            }
            public string baul
            { get { return _bauletto; } }
            
            public override double Sconto()
            {

                if (_motorizzazione == "2 Tempi")
                    return 300;
                else if (_motorizzazione == "4 Tempi")
                    return 250;
                else
                    return 200;
            }
            public override string ToString()
            {

                return "Modello: " + _modello + " Motorizzazione: " + _motorizzazione + " Cilindrata: " + _cilindrata + " Bauletto: " + _bauletto + " Prezzo: " + _prezzo + " Sconto: ";
            }
        }
        class TestaMoto
        {
            Image _immagine;
            double _x;
            double _y;
            double _velocità;

            bool stato;
            int i;

            public TestaMoto(Image i, double x, double y, double v)
            {
                _immagine = i;
                _x = x;
                _y = y;
                _velocità = v;
            }

            public void Movimento(string m)
            {

                string moto = m;
                BitmapImage provvisoria = new BitmapImage();
                provvisoria.BeginInit();
                if(moto=="z750")
                provvisoria.UriSource = new Uri("immage/z750va.png", UriKind.Relative);
                else if(moto=="z800")
                    provvisoria.UriSource = new Uri("immage/z800va.png", UriKind.Relative);
                else
                    provvisoria.UriSource = new Uri("immage/ninjava.png", UriKind.Relative);
                if (stato == false)
                {
                    _x += _velocità; // va a destra
                    i++;
                }
                else
                {
                    if (moto == "z750")
                    provvisoria.UriSource = new Uri("immage/z750vaspecchio.png", UriKind.Relative);
                    else if (moto == "z800")
                        provvisoria.UriSource = new Uri("immage/z800vaspecchio.png", UriKind.Relative);
                    else
                        provvisoria.UriSource = new Uri("immage/ninjavaspecchio.png", UriKind.Relative);
                    _x -= _velocità; // va a sinistra
                    i--;
                }

                if (i == 35 || i == 0)
                    stato = !stato;

                /*if (i == 35)
                    stato = true;
                if (i == 0)
                    stato = false;
                 */

                provvisoria.EndInit();
                _immagine.Source = provvisoria;
                _immagine.Margin = new Thickness(_x, _y, 0, 0);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            
            cmb_tipo.Items.Add("Stradale");
            cmb_tipo.Items.Add("Scooter");
            cmb_motorizzazione.Items.Add("2 Tempi");
            cmb_motorizzazione.Items.Add("4 Tempi");
            cmb_motorizzazione.Items.Add("Elettrico");
            cmb_bauletto.Items.Add("Grande");
            cmb_bauletto.Items.Add("Medio");
            cmb_bauletto.Items.Add("Piccolo");
            cmb_bauletto.Items.Add("Assente");
            cmb_carena.Items.Add("Semicarena");
            cmb_carena.Items.Add("Cupolino");
            cmb_carena.Items.Add("Integrale");
            cmb_carena.Items.Add("Naked");
            cmb_m.Items.Add("z750");
            cmb_m.Items.Add("z800");
            cmb_m.Items.Add("ninja");
            img_boom.Visibility = Visibility.Hidden;
            image2.Visibility = Visibility.Hidden;
            btn_agg.IsEnabled = false;
            //img_strada.Visibility = Visibility.Visible;
            //img_strada.Source = new BitmapImage(new Uri(@"immage/strada-a-tre-corsie-.png", UriKind.Relative));
            
            
           
        }
        private void txt_modello_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (cmb_tipo.SelectedItem.ToString() == "Stradale" && ok2==true)
            {
                cmb_bauletto.IsEnabled = false;
                cmb_carena.IsEnabled = true;
            }
            else if (cmb_tipo.SelectedItem.ToString() == "Scooter" && ok2 == true)
            {
                cmb_carena.IsEnabled = false;
                cmb_bauletto.IsEnabled = true;
            }
            else
                MessageBox.Show("selezionare tipo moto");

        }

        private void btn_agg_Click(object sender, RoutedEventArgs e)
        {
            //creao oggetto
            int cil;
            double prez;
            bool ok;
            ok = double.TryParse(txt_prezzo.Text, out prez);
            if (!ok&& prez>500)
            {
                txt_prezzo.Text = "";
                MessageBox.Show("Errore inserire prezzo valido");
            }
            else
            {
                ok = int.TryParse(txt_cilindrata.Text, out cil);
            if (!ok)
            {
                txt_cilindrata.Text = "errore";
                MessageBox.Show("Errore inserire cilindrata valida");
            }
            }
            ok = int.TryParse(txt_cilindrata.Text, out cil);
            if (!ok)
            {
                txt_cilindrata.Text = "errore";
                MessageBox.Show("Errore inserire cilindrata valida");
            }

            if (cmb_tipo.SelectedItem.ToString() == "Stradale" && txt_cilindrata.Text != "errore" && txt_cilindrata.Text != "errore")
            {
                moto[CountMoto] = new Stradali(txt_modello.Text, cil, cmb_motorizzazione.SelectedItem.ToString(), prez, cmb_carena.SelectedItem.ToString());
                cmb_mod.Items.Add(txt_modello.Text);
                CountMoto++;
            }
            else if (cmb_tipo.SelectedItem.ToString() == "Scooter" && txt_cilindrata.Text != "errore" && txt_cilindrata.Text != "errore")
            {
                moto[CountMoto] = new Scooter(txt_modello.Text, cil, cmb_motorizzazione.SelectedItem.ToString(), prez, cmb_bauletto.SelectedItem.ToString());
                cmb_mod.Items.Add(txt_modello.Text);
                CountMoto++;
            }
            else
                MessageBox.Show("Moto non aggiunta al catalogo");
            
            
        }

        private void btn_visualizza_Click(object sender, RoutedEventArgs e)
        {

            if (vis == true)
            {
                double s;
                s = moto[cmb_mod.SelectedIndex].Sconto();
                lbl_caratteristiche.Content = moto[cmb_mod.SelectedIndex].ToString() + s.ToString();
                if (moto[cmb_mod.SelectedIndex].mod == "z750")
                {
                    image2.Visibility = Visibility.Visible;
                    image2.Source = new BitmapImage(new Uri(@"immage/z750.jpg", UriKind.Relative));
                }
                else if (moto[cmb_mod.SelectedIndex].mod == "z800")
                {
                    image2.Visibility = Visibility.Visible;
                    image2.Source = new BitmapImage(new Uri(@"immage/z800.jpg", UriKind.Relative));
                }
                else if (moto[cmb_mod.SelectedIndex].mod == "ninja")
                {
                    image2.Visibility = Visibility.Visible;
                    image2.Source = new BitmapImage(new Uri(@"immage/ninja.jpg", UriKind.Relative));
                }
                else
                {
                    image2.Visibility = Visibility.Visible;
                    image2.Source = new BitmapImage(new Uri(@"immage/logo1.jpg", UriKind.Relative));
                }
            }
            else
                MessageBox.Show("selezionare moto");


        }

        private void btn_creaCaso_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int a =rnd.Next(0, 3);
            if (a == 0)
            {
                moto[CountMoto] = new Stradali("z750", 750, "4 tempi", 10000, "naked");
                cmb_mod.Items.Add(moto[CountMoto].mod);
            }
            else if (a == 1)
            {
                moto[CountMoto] = new Stradali("z800", 800, "4 tempi", 10000, "naked");
                cmb_mod.Items.Add(moto[CountMoto].mod);
            }

            else
            {
                moto[CountMoto] = new Stradali("ninja", 1000, "4 tempi", 10000, "cupolino");
                cmb_mod.Items.Add(moto[CountMoto].mod);
            }
            CountMoto++;
        }

        private void btn_vediCatalogo_Click(object sender, RoutedEventArgs e)
        {
            lst_catalogo.Items.Clear();
            for(int i=0;i<CountMoto;i++)
            {

                lst_catalogo.Items.Add(moto[i].mod + " Motorizzazione:" + moto[i].mot + " Cilindrata:" + moto[i].cil + " Prezzo:"+moto[i].prez.ToString());
            }
            
        }

        private void cmb_bauletto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_agg.IsEnabled = true;
        }

        private void cmb_carena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_agg.IsEnabled = true;
        }

        private void cmb_tipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ok2 = true;
        }
        
        private void btn_Testa_Click(object sender, RoutedEventArgs e)
        {

            if (testaM == true)
            {
                if (cmb_m.SelectedItem.ToString() == "z750")
                    img_moto.Source = new BitmapImage(new Uri("immage/z750va.png", UriKind.Relative));// img_moto.Source = new BitmapImage(new Uri(@"z750va.png", UriKind.Relative));
                else if (cmb_m.SelectedItem.ToString() == "z800")
                    img_moto.Source = new BitmapImage(new Uri("immage/z800va.png", UriKind.Relative));
                else
                    img_moto.Source = new BitmapImage(new Uri("immage/nonjava.png", UriKind.Relative));
                m = new TestaMoto(img_moto, 56, 475, 15);//Margin="56,475,0,0"/>
                m.Movimento(cmb_m.SelectedItem.ToString());
                ok = true;
            }
            else
                MessageBox.Show("Selezionare moto");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(ok==true)
            m.Movimento(cmb_m.SelectedItem.ToString());
        }

        private void btn_allah_Click(object sender, RoutedEventArgs e)
        {
            img_boom.Visibility = Visibility.Visible;
            img_boom.Source = new BitmapImage(new Uri(@"immage/boom.png", UriKind.Relative));
            MessageBox.Show("ALLAH AKBAR!!! te l' avevo detto di non premere");
            System.Threading.Thread.Sleep(300);
            Close();
        }

        private void cmb_m_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            testaM = true;
        }

        private void cmb_mod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vis = true;
        }



    }
}
