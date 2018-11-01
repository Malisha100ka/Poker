using PokerOddsCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerCalculator
{
    public partial class Form1 : Form
    {
        private int Stage = 0;
        private List<Card> Player1Hand = new List<Card>();
        private List<Card> Player2Hand = new List<Card>();
        private List<Card> Player3Hand = new List<Card>();
        private Boolean Player1 = true;
        private Boolean Player2 = false;
        private Boolean Player3 = false;
        private OddsCalculator calculator = new OddsCalculator();
        private float[] OddsPlayer1 = new float[10];
        private float[] OddsPlayer2 = new float[10];
        private float[] OddsPlayer3 = new float[10];
        private int[] Cards = new int[11] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        private const int SIMS = 1000;
        private const int DEC_PLACES = 2;

        public Form1()
        {
            InitializeComponent();
            SetLabelsNewGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (Stage)
            {
                case 0:
                    Stage0();
                    break;
                case 1:
                    Stage1();
                    break;
                case 2:
                    Stage2();
                    break;
                case 3:
                    Stage3();
                    break;
            }
            Stage++;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SetLabelsNewGame();
            ClearPlayers();
            ClearLists();
            ClearOdds();
            ClearCards();
            Stage = 0;
            button1.Enabled = true;
        }

        private void Stage0()
        {
            GenerateCards();
            CountPlayers();
            SetPlayersCard();
            CalculateOdds();
            SetOdds();
            button1.Text = "Flop";
        }

        private void Stage1()
        {
            SetFlopCards();
            CalculateOdds();
            SetOdds();
            button1.Text = "Turn";
        }

        private void Stage2()
        {
            SetTurnCards();
            CalculateOdds();
            SetOdds();
            button1.Text = "River";
        }

        private void Stage3()
        {
            SetRiverCards();
            CalculateOdds();
            SetOdds();
            button1.Text = "--END--";
            button1.Enabled = false;
        }

        private void GenerateCards()
        {
            var rand = new Random();
            var number = 0;
            for (int i = 0; i < Cards.Length; i++)
            {
                do
                {
                    number = rand.Next(0, 52);
                } while (Cards.Contains(number));
                Cards[i] = number;
            }
        }

        private void CountPlayers()
        {
            if (radioButton4.Checked)
            {
                Player1 = true;
                Player2 = false;
                Player3 = false;
            }
            else if (radioButton5.Checked)
            {
                Player1 = true;
                Player2 = true;
                Player3 = false;
            }
            else
            {
                Player1 = true;
                Player2 = true;
                Player3 = true;
            }
        }


        private void SetPlayersCard()
        {
            if (Player1)
            {
                Player1Hand.Add(new Card(Cards[0]));
                Player1Hand.Add(new Card(Cards[1]));
                label28.Text = Player1Hand[0].ToString();
                label29.Text = Player1Hand[1].ToString();
            }
            if (Player2)
            {
                Player2Hand.Add(new Card(Cards[2]));
                Player2Hand.Add(new Card(Cards[3]));
                label30.Text = Player2Hand[0].ToString();
                label31.Text = Player2Hand[1].ToString();
            }
            if (Player3)
            {
                Player3Hand.Add(new Card(Cards[4]));
                Player3Hand.Add(new Card(Cards[5]));
                label26.Text = Player3Hand[0].ToString();
                label27.Text = Player3Hand[1].ToString();
            }
        }

        private void SetFlopCards()
        {
            if (Player1)
            {
                Player1Hand.Add(new Card(Cards[6]));
                Player1Hand.Add(new Card(Cards[7]));
                Player1Hand.Add(new Card(Cards[8]));
            }
            if (Player2)
            {
                Player2Hand.Add(new Card(Cards[6]));
                Player2Hand.Add(new Card(Cards[7]));
                Player2Hand.Add(new Card(Cards[8]));
            }
            if (Player3)
            {
                Player3Hand.Add(new Card(Cards[6]));
                Player3Hand.Add(new Card(Cards[7]));
                Player3Hand.Add(new Card(Cards[8]));
            }
            label21.Text = Player1Hand[2].ToString();
            label22.Text = Player1Hand[3].ToString();
            label23.Text = Player1Hand[4].ToString();
        }


        private void SetTurnCards()
        {
            if (Player1)
            {
                Player1Hand.Add(new Card(Cards[9]));
            }
            if (Player2)
            {
                Player2Hand.Add(new Card(Cards[9]));
            }
            if (Player3)
            {
                Player3Hand.Add(new Card(Cards[9]));
            }
            label24.Text = Player1Hand[5].ToString();
        }


        private void SetRiverCards()
        {
            if (Player1)
            {
                Player1Hand.Add(new Card(Cards[10]));
            }
            if (Player2)
            {
                Player2Hand.Add(new Card(Cards[10]));
            }
            if (Player3)
            {
                Player3Hand.Add(new Card(Cards[10]));
            }
            label25.Text = Player1Hand[6].ToString();
        }


        private void CalculateOdds()
        {
            if (Player1)
            {
                OddsPlayer1 = calculator.RunSimulations(Player1Hand, SIMS);
            }
            if (Player2)
            {
                OddsPlayer2 = calculator.RunSimulations(Player2Hand, SIMS);
            }
            if (Player3)
            {
                OddsPlayer3 = calculator.RunSimulations(Player3Hand, SIMS);
            }
        }


        private void ClearCards()
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                Cards[i] = 0;
            }
        }

        private void ClearOdds()
        {
            for (int i = 0; i < OddsPlayer1.Length; i++)
            {
                OddsPlayer1[i] = 0;
                OddsPlayer2[i] = 0;
                OddsPlayer3[i] = 0;
            }
        }

        private void ClearLists()
        {
            Player1Hand.Clear();
            Player2Hand.Clear();
            Player3Hand.Clear();
        }

        private void ClearPlayers()
        {
            Player1 = true;
            Player2 = false;
            Player3 = false;
        }

        private void SetOdds()
        {
            if (radioButton1.Checked)
            {
                label11.Text = Math.Round(OddsPlayer3[0], DEC_PLACES) + "%";
                label12.Text = Math.Round(OddsPlayer3[1], DEC_PLACES) + "%";
                label13.Text = Math.Round(OddsPlayer3[2], DEC_PLACES) + "%";
                label14.Text = Math.Round(OddsPlayer3[3], DEC_PLACES) + "%";
                label15.Text = Math.Round(OddsPlayer3[4], DEC_PLACES) + "%";
                label16.Text = Math.Round(OddsPlayer3[5], DEC_PLACES) + "%";
                label17.Text = Math.Round(OddsPlayer3[6], DEC_PLACES) + "%";
                label18.Text = Math.Round(OddsPlayer3[7], DEC_PLACES) + "%";
                label19.Text = Math.Round(OddsPlayer3[8], DEC_PLACES) + "%";
                label20.Text = Math.Round(OddsPlayer3[9], DEC_PLACES) + "%";
            }
            else if (radioButton2.Checked)
            {
                label11.Text = Math.Round(OddsPlayer1[0], DEC_PLACES) + "%";
                label12.Text = Math.Round(OddsPlayer1[1], DEC_PLACES) + "%";
                label13.Text = Math.Round(OddsPlayer1[2], DEC_PLACES) + "%";
                label14.Text = Math.Round(OddsPlayer1[3], DEC_PLACES) + "%";
                label15.Text = Math.Round(OddsPlayer1[4], DEC_PLACES) + "%";
                label16.Text = Math.Round(OddsPlayer1[5], DEC_PLACES) + "%";
                label17.Text = Math.Round(OddsPlayer1[6], DEC_PLACES) + "%";
                label18.Text = Math.Round(OddsPlayer1[7], DEC_PLACES) + "%";
                label19.Text = Math.Round(OddsPlayer1[8], DEC_PLACES) + "%";
                label20.Text = Math.Round(OddsPlayer1[9], DEC_PLACES) + "%";
            }
            else
            {
                label11.Text = Math.Round(OddsPlayer2[0], DEC_PLACES) + "%";
                label12.Text = Math.Round(OddsPlayer2[1], DEC_PLACES) + "%";
                label13.Text = Math.Round(OddsPlayer2[2], DEC_PLACES) + "%";
                label14.Text = Math.Round(OddsPlayer2[3], DEC_PLACES) + "%";
                label15.Text = Math.Round(OddsPlayer2[4], DEC_PLACES) + "%";
                label16.Text = Math.Round(OddsPlayer2[5], DEC_PLACES) + "%";
                label17.Text = Math.Round(OddsPlayer2[6], DEC_PLACES) + "%";
                label18.Text = Math.Round(OddsPlayer2[7], DEC_PLACES) + "%";
                label19.Text = Math.Round(OddsPlayer2[8], DEC_PLACES) + "%";
                label20.Text = Math.Round(OddsPlayer2[9], DEC_PLACES) + "%";
            }
        }

        private void SetLabelsNewGame()
        {
            label1.Text = "High Card:";
            label2.Text = "Pair:";
            label3.Text = "Two Pair:";
            label4.Text = "Three of a Kind:";
            label5.Text = "Straight:";
            label6.Text = "Flush:";
            label7.Text = "Full House:";
            label8.Text = "Four of a Kind:";
            label9.Text = "Straight Flush:";
            label10.Text = "Royal Flush:";
            label11.Text = "None";
            label12.Text = "None";
            label13.Text = "None";
            label14.Text = "None";
            label15.Text = "None";
            label16.Text = "None";
            label17.Text = "None";
            label18.Text = "None";
            label19.Text = "None";
            label20.Text = "None";
            label21.Text = "None";
            label22.Text = "None";
            label23.Text = "None";
            label24.Text = "None";
            label25.Text = "None";
            label26.Text = "None";
            label27.Text = "None";
            label28.Text = "None";
            label29.Text = "None";
            label30.Text = "None";
            label31.Text = "None";

            groupBox1.Text = "Odds";
            groupBox2.Text = "Igrači";
            groupBox3.Text = "Broj igrača";
            groupBox5.Text = "Board";

            radioButton1.Text = "Igrač 3";
            radioButton2.Text = "Igrač 1";
            radioButton3.Text = "Igrač 2";
            radioButton4.Text = "Jedan igrač";
            radioButton5.Text = "Dva igrača";
            radioButton6.Text = "Tri igrača";

            button1.Text = "Start";
            button2.Text = "RESET -> 0x0000";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetOdds();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetOdds();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetOdds();
        }

    }
}
