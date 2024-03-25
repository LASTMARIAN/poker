using System.Linq.Expressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        int index = 0;
        int phase = 0;
        int cash = 0;
        static Hand player_hand = new Hand();
        static Hand bot1_hand = new Hand();
        static Hand bot2_hand = new Hand();
        static Hand bot3_hand = new Hand();
        static Hand bot4_hand = new Hand();
        List<Hand> players = new List<Hand>() { player_hand, bot1_hand, bot2_hand, bot3_hand, bot4_hand };
        int common_bet;

        int duplicates_values(int index, List<int> list)
        {
            int count = 0;
            foreach (var item in list)
            {
                if (item == list[index])
                {
                    count++;
                }
            }
            return count;
        }
        int duplicates_suites(int index, List<string> list)
        {
            int count = 0;
            foreach (var item in list)
            {
                if (item == list[index])
                {
                    count++;
                }
            }
            return count;
        }
        void hand_locator(PictureBox pictureBox1, PictureBox pictureBox2, Hand hand)
        {
            pictureBox1.ImageLocation = $@"C:\Users\chere\source\repos\WinFormsApp2\WinFormsApp2\PNG-cards-1.3\{hand.FirstCardGetter}.png";
            pictureBox2.ImageLocation = $@"C:\Users\chere\source\repos\WinFormsApp2\WinFormsApp2\PNG-cards-1.3\{hand.SecondCardGetter}.png";
        }

        void table_locator(PictureBox pictureBox, int index)
        {
            pictureBox.ImageLocation = $@"C:\Users\chere\source\repos\WinFormsApp2\WinFormsApp2\PNG-cards-1.3\{Table.cards[index]}.png";
        }

        void bot_hand_locator(PictureBox picture1, PictureBox picture2)
        {
            picture1.ImageLocation = $@"C:\Users\chere\source\repos\WinFormsApp2\WinFormsApp2\360_F_42655284_RHCunPUIr61o9GNoJdAwPoYyCmsKCk3T.jpg";
            picture2.ImageLocation = $@"C:\Users\chere\source\repos\WinFormsApp2\WinFormsApp2\360_F_42655284_RHCunPUIr61o9GNoJdAwPoYyCmsKCk3T.jpg";
        }

        
        void straight_checker(int phase)
        {

            for (int i = 0; i < players.Count; i++)
            {
                List<int> numbers = new List<int>();
                for (int j = 0; j < 4; j += 2)
                {
                    numbers.Add(Convert.ToInt32(players[i].data_hand[j]));
                }
                for (int k = 0; k < 4 + phase * 2; k += 2)
                {
                    numbers.Add(Convert.ToInt32(Table.data_table[k]));
                }
                numbers.Sort();
                int row = 0;
                for (int m = 0; m < numbers.Count - 1; m++)
                {
                    if (numbers[m + 1] - numbers[m] == 1)
                    {
                        row += 1;
                        if (row == 4)
                        {
                            players[i].combination_value = numbers[m + 1];
                            break;
                        }
                    }
                    else
                    {
                        row = 0;
                    }
                }
                switch (row)
                {
                    case 4:
                        if (players[i].comb < 5)
                        {
                            players[i].comb = 5;
                        }
                        

                        break;
                    case 3:
                        players[i].score = Convert.ToInt32(players[i].score * 1.2);
                        break;
                }
            }
        }

        void flush_checker(int phase)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (Table.data_table.Contains(players[i].data_hand[1]))
                {
                    for (int k = 1; k < 4; k += 2)
                    {
                        if (players[i].data_hand[k] == players[i].data_hand[1])
                        {
                            players[i].flush_count++;
                        }
                    }

                    for (int j = 1; j < 4 + phase * 2; j += 2)
                    {
                        if (Table.data_table[j] == players[i].data_hand[1])
                        {
                            players[i].flush_count++;
                        }
                    }
                    switch (players[i].flush_count)
                    {
                        case 5:
                            if (players[i].comb == 5)
                            {
                                players[i].comb = 9;
                            }
                            else
                            {
                                if (players[i].comb < 6)
                                {
                                    players[i].comb = 6;
                                }
                            }
                            

                            break;
                        case 4:
                            players[i].score = Convert.ToInt32(Math.Round(Convert.ToDouble(players[i].score) * 1.3));
                            break;
                        case 3:
                            players[i].score = Convert.ToInt32(Math.Round(Convert.ToDouble(players[i].score) * 1.1));
                            break;
                    }
                    players[i].flush_count = 0;
                }
                if (Table.data_table.Contains(players[i].data_hand[3]))
                {
                    for (int k = 1; k < 4; k += 2)
                    {
                        if (players[i].data_hand[k] == players[i].data_hand[3])
                        {
                            players[i].flush_count++;
                        }
                    }

                    for (int j = 1; j < 4 + phase * 2; j += 2)
                    {
                        if (Table.data_table[j] == players[i].data_hand[3])
                        {
                            players[i].flush_count++;
                        }
                    }
                    switch (players[i].flush_count)
                    {
                        case 5:
                            if (players[i].comb == 5)
                            {
                                players[i].comb = 9;
                            }
                            else
                            {
                                if (players[i].comb < 6)
                                {
                                    players[i].comb = 6;
                                }
                            }
                            break;
                        case 4:
                            players[i].score = Convert.ToInt32(Math.Round(Convert.ToDouble(players[i].score) * 1.5));
                            break;
                        case 3:
                            players[i].score = Convert.ToInt32(Math.Round(Convert.ToDouble(players[i].score) * 1.1));
                            break;
                    }
                    players[i].flush_count = 0;
                }
            }
        }

        void same_cards_combs_checker(int phase)
        {
            

            for (int i = 0; i < players.Count; i++)
            {
                
                List<int> values = new List<int>();
                for (int j = 0; j < 4; j += 2)
                {
                    values.Add(Convert.ToInt32(players[i].data_hand[j]));
                }
                for (int k = 0; k < 4 + 2 * phase; k += 2)
                {
                    values.Add(Convert.ToInt32(Table.data_table[k]));
                }
                values.Sort();
                int pairs_quantity = 0;
                for (int h = 0; h < values.Count; h++)
                {

                    if (duplicates_values(h, values) == 4 && players[i].comb < 8)
                    {
                        players[i].comb = 8;
                        players[i].combination_value = values[h];
                        break;

                    }
                    if (duplicates_values(h, values) == 3 && players[i].comb < 4)
                    {
                        if (players[i].comb == 2)
                        {
                            players[i].comb = 7;
                        }
                        else
                        {
                            players[i].comb = 4;
                        }
                         
                        players[i].combination_value = values[h];
                        if (h < values.Count - 3)
                        {
                            h += 3;
                        }
                        else
                        {
                            break;
                        }

                    }
                    if (duplicates_values(h, values) == 2)
                    {
                        
                        if (players[i].comb == 4)
                        {
                            players[i].comb = 7;
                        }
                        if (players[i].comb == 2 && pairs_quantity == 1)
                        {
                            
                            players[i].comb = 3;
                            if (values[h] > players[i].combination_value)
                            {
                                players[i].combination_value = values[h];
                            }
                        }
                        if (players[i].comb < 2)
                        { 
                            players[i].comb = 2;
                            players[i].combination_value = values[h];
                        }
                        pairs_quantity += 1;
                        
                        if (h < values.Count - 2)
                        {
                            h += 2;
                        }
                        else
                        {
                            break;
                        }

                    }
                }

            }

        }

        void royal_flush_checker(int phase)
        {
            for (int i = 0; i < players.Count; i++)
            {
                List<int> values = new List<int>();
                List<string> suites = new List<string>();
                for (int k = 0; k < 4; k += 2)
                {
                    values.Add(Convert.ToInt32(players[i].data_hand[k]));
                    suites.Add(players[i].data_hand[k + 1]);
                }
                for (int j = 0; j < 4 + phase * 2; j += 2)
                {
                    values.Add(Convert.ToInt32(Table.data_table[j]));
                    suites.Add(Table.data_table[j + 1]);
                }
                if (values.Contains(14) && values.Contains(13) &&
                    values.Contains(12) && values.Contains(11) && values.Contains(10)
                    && (duplicates_suites(0, suites) == 5 ||
                    duplicates_suites(1, suites) == 5 || duplicates_suites(2, suites) == 5))
                {
                    players[i].comb = 10;

                }
            }
        }

        void combinations_checker(int phase)
        {
            straight_checker(phase);
            flush_checker(phase);
            same_cards_combs_checker(phase);
            royal_flush_checker(phase);
        }

        void finish_game()
        {
            switch (phase)
            {
                case 0:
                    table_locator(pictureBox7, 0);
                    table_locator(pictureBox8, 1);
                    table_locator(pictureBox13, 2);
                    Table.data_table.Add($"{Table.cards[0][0..Table.first_space]}");
                    Table.data_table.Add($"{Table.cards[0][(Table.first_space + 1)..Table.cards[0].Length]}");
                    Table.data_table.Add($"{Table.cards[1][0..Table.second_space]}");
                    Table.data_table.Add($"{Table.cards[1][(Table.second_space + 1)..Table.cards[1].Length]}");
                    Table.data_table.Add($"{Table.cards[2][0..Table.third_space]}");
                    Table.data_table.Add($"{Table.cards[2][(Table.third_space + 1)..Table.cards[2].Length]}");
                    table_locator(pictureBox14, 3);
                    Table.data_table.Add($"{Table.cards[3][0..Table.fourth_space]}");
                    Table.data_table.Add($"{Table.cards[3][(Table.fourth_space + 1)..Table.cards[3].Length]}");
                    table_locator(pictureBox15, 4);
                    Table.data_table.Add($"{Table.cards[4][0..Table.fifth_space]}");
                    Table.data_table.Add($"{Table.cards[4][(Table.fifth_space + 1)..Table.cards[4].Length]}");
                    combinations_checker(3);
                    break;
                case 1:
                    table_locator(pictureBox14, 3);
                    Table.data_table.Add($"{Table.cards[3][0..Table.fourth_space]}");
                    Table.data_table.Add($"{Table.cards[3][(Table.fourth_space + 1)..Table.cards[3].Length]}"); 
                    table_locator(pictureBox15, 4);
                    Table.data_table.Add($"{Table.cards[4][0..Table.fifth_space]}");
                    Table.data_table.Add($"{Table.cards[4][(Table.fifth_space + 1)..Table.cards[4].Length]}");
                    combinations_checker(3);
                    break;
                case 2:
                    table_locator(pictureBox15, 4);
                    Table.data_table.Add($"{Table.cards[4][0..Table.fifth_space]}");
                    Table.data_table.Add($"{Table.cards[4][(Table.fifth_space + 1)..Table.cards[4].Length]}");
                    combinations_checker(3);
                    break;
            }
            List<int> scores = new List<int>();
            List<int> values_of_comb = new List<int>();
            List<Hand> potential_winners = new List<Hand>();
            
            Dictionary<Hand, Label> dic = new Dictionary<Hand, Label>()
            {
                { player_hand, label8 },
                { bot1_hand, label6 },
                { bot2_hand, label7 },
                { bot3_hand, label9 },
                { bot4_hand, label10 },
            };
            foreach (Hand item in players)
            {
                scores.Add(item.comb);
                
            }
            foreach (Hand item in players)
            {
                if (item.comb == scores.Max())
                {    
                    potential_winners.Add(item);
                    
                    
                }
            }
            foreach (Hand item in potential_winners)
            {
                values_of_comb.Add(item.combination_value);
            }
            if (potential_winners.Count() > 1)
            {
                foreach (int item in values_of_comb)
                {
                    if (item == values_of_comb.Max())
                    {
                        dic[potential_winners[values_of_comb.IndexOf(item)]].Text = $"WINNER! : {players[scores.IndexOf(scores.Max())].combs_dict[players[scores.IndexOf(scores.Max())].comb]}";

                    }
                }
                
            }
            else if (potential_winners.Count() == 1)
            {
                dic[potential_winners[0]].Text = $"WINNER! : {players[scores.IndexOf(scores.Max())].combs_dict[players[scores.IndexOf(scores.Max())].comb]}";
            }
            button6.Enabled = false;
            players[scores.IndexOf(scores.Max())].balance += cash;
            
            
            hand_locator(pictureBox5, pictureBox6, bot1_hand);
            hand_locator(pictureBox4, pictureBox3, bot2_hand);
            hand_locator(pictureBox11, pictureBox12, bot3_hand);
            hand_locator(pictureBox9, pictureBox10, bot4_hand);
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "Make a raise here!";
            button3.Enabled = true;
            
            
            button6.Enabled = true;
            hand_locator(pictureBox1, pictureBox2, player_hand);


            bot_hand_locator(pictureBox5, pictureBox6);


            bot_hand_locator(pictureBox4, pictureBox3);


            bot_hand_locator(pictureBox11, pictureBox12);


            bot_hand_locator(pictureBox9, pictureBox10);

            Label[] dealers_labels = new Label[5] { label6, label7, label8, label9, label10 };
            Hand[] dealers = new Hand[5] { bot1_hand, bot2_hand, player_hand, bot3_hand, bot4_hand };
            if (index == 0)
            {
                dealers_labels[4].Text = "";
            }
            else
            {
                dealers_labels[index - 1].Text = "";
            }
            dealers_labels[index].Text = "DEALER / CALL";
            dealers[index].balance -= 2;
            dealers[index].bet += 2;
            if (index == 4)
            {
                bot1_hand.balance -= 2;
                bot1_hand.bet += 2;
                label6.Text = "SMALL BLIND / CALL";
                bot2_hand.balance -= 2;
                bot2_hand.bet += 2;
                label7.Text = "BIG BLIND";
            }
            else if (index == 3)
            {
                bot1_hand.balance -= 2;
                bot1_hand.bet += 2;
                label6.Text = "BIG BLIND";
            }
            else
            {
                dealers[index + 1].balance -= 2;
                dealers[index + 1].bet += 2;
                dealers_labels[index + 1].Text = "SMALL BLIND / CALL";
                dealers[index + 2].balance -= 2;
                dealers[index + 2].bet += 2;
                dealers_labels[index + 2].Text = "BIG BLIND";
            }
            for (int i = 0; i < 5; i++)
            {
                if (dealers[i].balance == 1000 && i != index)
                {
                    dealers[i].balance -= 2;
                    dealers[i].bet += 2;
                    dealers_labels[i].Text = "CALL";
                }
            }
            label13.Text = $"{player_hand.balance}€";
            label11.Text = $"{bot1_hand.balance}€";
            label12.Text = $"{bot2_hand.balance}€";
            label14.Text = $"{bot3_hand.balance}€";
            label15.Text = $"{bot4_hand.balance}€";

            if (index == 4)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            button3.Enabled = true;
            
            
            button6.Enabled = true;
            cash = 10;
            label19.Text = $"{player_hand.bet}€";
            label20.Text = $"{bot3_hand.bet}€";
            label21.Text = $"{bot4_hand.bet}€";
            label18.Text = $"{bot2_hand.bet}€";
            label16.Text = $"{bot1_hand.bet}€";
            label17.Text = $"{player_hand.bet + bot3_hand.bet + bot4_hand.bet + bot2_hand.bet + bot1_hand.bet}€";
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label19.Text = $"{player_hand.bet}€";
            label20.Text = $"{bot3_hand.bet}€";
            label21.Text = $"{bot4_hand.bet}€";
            label18.Text = $"{bot2_hand.bet}€";
            label16.Text = $"{bot1_hand.bet}€";
            Random rnd = new Random();
            int average_sc = Convert.ToInt32(Math.Round(Convert.ToDouble((bot1_hand.score + bot2_hand.score + bot3_hand.score + bot4_hand.score) / 4)));


            if (Table.cards.Count != 5)
            {
                Table.TableGenerator();
            }

            switch (phase)
            {
                case 0:
                    table_locator(pictureBox7, 0);
                    table_locator(pictureBox8, 1);
                    table_locator(pictureBox13, 2);
                    Table.data_table.Add($"{Table.cards[0][0..Table.first_space]}");
                    Table.data_table.Add($"{Table.cards[0][(Table.first_space + 1)..Table.cards[0].Length]}");
                    Table.data_table.Add($"{Table.cards[1][0..Table.second_space]}");
                    Table.data_table.Add($"{Table.cards[1][(Table.second_space + 1)..Table.cards[1].Length]}");
                    Table.data_table.Add($"{Table.cards[2][0..Table.third_space]}");
                    Table.data_table.Add($"{Table.cards[2][(Table.third_space + 1)..Table.cards[2].Length]}");
                    int point = rnd.Next(0, 100);
                    combinations_checker(1);
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button3.Enabled = true;
                    phase++;
                    button2.Enabled = false;
                    break;
                case 1:
                    table_locator(pictureBox14, 3);
                    Table.data_table.Add($"{Table.cards[3][0..Table.fourth_space]}");
                    Table.data_table.Add($"{Table.cards[3][(Table.fourth_space + 1)..Table.cards[3].Length]}");
                    combinations_checker(2);
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button3.Enabled = true;
                    phase++;
                    button2.Enabled = false;
                    break;
                case 2:
                    table_locator(pictureBox15, 4);
                    Table.data_table.Add($"{Table.cards[4][0..Table.fifth_space]}");
                    Table.data_table.Add($"{Table.cards[4][(Table.fifth_space + 1)..Table.cards[4].Length]}");
                    combinations_checker(3);
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button3.Enabled = true;
                    phase++;
                    button2.Enabled = false;
                    break;
                case 3:
                    finish_game();
                    break;

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            label8.Text = "CHECK";
            Random rnd = new Random();
            Dictionary<Hand, Label> dic = new Dictionary<Hand, Label>()
            {
                { player_hand, label8 },
                { bot1_hand, label6 },
                { bot2_hand, label7 },
                { bot3_hand, label9 },
                { bot4_hand, label10 },
            };
            int average_sc = 0;
            for (int i = 0; i < players.Count; i++)
            {
                average_sc += players[i].score;
            }
            average_sc /= players.Count;
            
            int point = rnd.Next(0, 65 * phase);
            if (point >= average_sc)
            {
                common_bet = 0;
                foreach (Hand item in players)
                {
                    dic[item].Text = "CHECK";
                }
                
                button2.Enabled = true;
                button5.Enabled = false;
            }
            else
            {
                List<Label> status = new List<Label>() { label6, label7, label9, label10 };
                common_bet = rnd.Next(average_sc / 4, average_sc / 2);
                int index = rnd.Next(1, 4);
                status[index].Text = "RAISE";
                
                foreach (Hand item in players)
                {
                    if (item != player_hand && dic[item] != status[index])
                    {
                        dic[item].Text = "CALL";
                    }
                    
                }
                
                button4.Enabled = true;
                button5.Enabled = true;
            }
            bot1_hand.bet += common_bet;
            bot2_hand.bet += common_bet;
            bot3_hand.bet += common_bet;
            bot4_hand.bet += common_bet;
            cash += common_bet * 4;
            bot1_hand.balance -= common_bet;
            bot2_hand.balance -= common_bet;
            bot3_hand.balance -= common_bet;
            bot4_hand.balance -= common_bet;
            label19.Text = $"{player_hand.bet}€";
            label20.Text = $"{bot3_hand.bet}€";
            label21.Text = $"{bot4_hand.bet}€";
            label18.Text = $"{bot2_hand.bet}€";
            label16.Text = $"{bot1_hand.bet}€";
            label17.Text = $"{cash}€";
            label13.Text = $"{player_hand.balance}€";
            label11.Text = $"{bot1_hand.balance}€";
            label12.Text = $"{bot2_hand.balance}€";
            label14.Text = $"{bot3_hand.balance}€";
            label15.Text = $"{bot4_hand.balance}€";
            button6.Enabled = false;
            button3.Enabled = false;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<Hand, Label> dic = new Dictionary<Hand, Label>()
                {
                    { player_hand, label8 },
                    { bot1_hand, label6 },
                    { bot2_hand, label7 },
                    { bot3_hand, label9 },
                    { bot4_hand, label10 },
                };
                List<Hand> players_to_delete = new List<Hand>();
                
                if (Convert.ToInt32(textBox1.Text) <= player_hand.balance)
                {
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button2.Enabled = true;
                    player_hand.bet += Convert.ToInt32(textBox1.Text);
                    player_hand.balance -= Convert.ToInt32(textBox1.Text);
                    cash += Convert.ToInt32(textBox1.Text);
                    button3.Enabled = false;
                    label19.Text = $"{player_hand.bet}€";
                    Random rnd = new Random();
                    for (int i = 0; i < 5; i++)
                    {
                        if (players[i].score >= Convert.ToInt32(textBox1.Text) && players[i] != player_hand)
                        {
                            players[i].bet += Convert.ToInt32(textBox1.Text);
                            players[i].balance -= Convert.ToInt32(textBox1.Text);
                            cash += Convert.ToInt32(textBox1.Text);
                            dic[players[i]].Text = "CALL";
                        }
                        else
                        {
                            if (players[i] != player_hand)
                            {
                                dic[players[i]].Text = "FOLD";
                                dic[players[i]].Enabled = false;
                                players_to_delete.Add(players[i]);
                            }
                            
                        }
                    }
                    foreach (Hand item in players_to_delete)
                    {
                        players.Remove(item);
                    }
                    label8.Text = "RAISE";
                    label19.Text = $"{player_hand.bet}€";
                    label20.Text = $"{bot3_hand.bet}€";
                    label21.Text = $"{bot4_hand.bet}€";
                    label18.Text = $"{bot2_hand.bet}€";
                    label16.Text = $"{bot1_hand.bet}€";
                    label17.Text = $"{cash}€";
                    label13.Text = $"{player_hand.balance}€";
                    label11.Text = $"{bot1_hand.balance}€";
                    label12.Text = $"{bot2_hand.balance}€";
                    label14.Text = $"{bot3_hand.balance}€";
                    label15.Text = $"{bot4_hand.balance}€";
                }
                else
                {
                    textBox1.Text = "This number is more than your balance!";
                }
            }
            catch
            {
                textBox1.Text = "Type a number!";

            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            player_hand.bet += common_bet;
            player_hand.balance -= common_bet;
            cash += common_bet;
            common_bet = 0;
            button2.Enabled = true;
            label19.Text = $"{player_hand.bet}€";
            label13.Text = $"{player_hand.balance}€";
            label17.Text = $"{cash}€";
            label8.Text = "CALL";
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            players.Remove(player_hand);
            label8.Text = "FOLD";
            label8.Enabled = false;
            finish_game();
        }

        
    }
}