using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraWLitery
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stats stats = new Stats();

        public Form1()
        {
            InitializeComponent();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Dodajemy losową literkę do kontrolki ListBox
            listBox1.Items.Add((Keys)random.Next(65, 90));
            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Koniec gry");
                timer1.Stop();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Jeśli gracz nacisnął klawisz literki dostępnej w kontrolce
            // ListBox, to usuwamy ją i zwiększamy tempo gry 
            if (listBox1.Items.Contains(e.KeyCode))
            {
                listBox1.Items.Remove(e.KeyCode);
                listBox1.Refresh();
                if (timer1.Interval > 400)
                    timer1.Interval -= 10;
                if (timer1.Interval > 250)
                    timer1.Interval -= 7;
                if (timer1.Interval > 100)
                    timer1.Interval -= 2;
                difficultyProgressBar.Value = 800 - timer1.Interval;

                // Gracz nacisnął prawidłowy klawisz, aktualizujemy zatem statystki
                // gry, wywołując metode UPdate() i przekazująć do niej wartość true 
                stats.Update(false);

            }

            else
            {
                // Gracz nacisnął niewłaściwy klawisz, zatem aktualizujemy statystyki,
                // przekazując w wywołujemy metody Update() wartość false
                stats.Update(false);
            }

            //Aktualizujemy etykiety na pasku stanu 
            correctLabel.Text = "Prawidłowych: " + stats.Correct;
            missedLabel.Text = "Błędów: " + stats.Missed;
            totalLabel.Text = "Wszystkich: " + stats.Total;
            accuracyLabel.Text = "Dokładność: " + stats.Accuracy + "%";
        }
    }
}
