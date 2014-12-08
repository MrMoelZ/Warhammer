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

namespace QDWH
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Calculate()
        {
            if (WSa.Text != null && WSa.Text != "" && WSb.Text != null && WSb.Text != "")
            {
                bool success = false;
                int valA;
                int valB;
                int toHit;
                success = Int32.TryParse(WSa.Text, out valA) & Int32.TryParse(WSb.Text, out valB);
                if (valA < 1 || valA > 10 || valB < 1 || valB > 10)
                {
                    success = false;
                }
                if (valA <= valB)
                {
                    toHit = 4;
                    if ((valA * 2) <= valB)
                    {
                        toHit = 5;
                    }
                }

                else if (valA > valB)
                {
                    toHit = 3;
                }
                else
                {
                    toHit = 0;
                }

                if (success)
                {
                    ToHit.Text = "Trefferwurf auf die: " + toHit + "+";
                }
                else
                {
                    ToHit.Text = "Eingabe prüfen";
                }
            }

            if (STRa.Text != null && STRa.Text != "" && TOUb.Text != null && TOUb.Text != "")
            {
                bool success = false;
                int valA;
                int valB;
                int toWound;
                success = Int32.TryParse(STRa.Text, out valA) & Int32.TryParse(TOUb.Text, out valB);
                if (valA < 1 || valA > 10 || valB < 1 || valB > 10)
                {
                    success = false;
                }

                if (valA == valB)
                {
                    toWound = 4;
                }
                else if (valA + 1 == valB)
                {
                    toWound = 5;
                }
                else if (valA - 1 == valB)
                {
                    toWound = 3;
                }
                else if (valA + 1 < valB)
                {
                    toWound = 6;
                }
                else if (valA - 1 > valB)
                {
                    toWound = 2;
                }
                else
                {
                    toWound = 0;
                }

                int mod = 3 - valA;
                if (mod < 0)
                {
                    Modificator.Text = "Rüstungsmodifikator: " + mod;
                }
                else
                {
                    Modificator.Text = "";
                }

                if(valA > 4)
                {
                    AdditionalInfo.Text = "Bretonnia: Blessing of the Lady Rettungswurf jetzt 5+!";
                }

                if (success)
                {
                    ToWound.Text = "Verwundung auf die: " + toWound + "+";
                }
                else
                {
                    ToWound.Text = "Eingabe prüfen";
                }
            }
        }



        private void Calc(object sender, KeyEventArgs e)
        {
            Calculate();
        }

        private void Calc(object sender, TextChangedEventArgs e)
        {
            Calculate();
        }
    }
}
