using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Deck
{
    public static bool deck_made = false;
    public static List<string> deckList = new List<string>();
    public static void DeckGenerate()
    {
        deckList.Clear();
        for (int i = 2; i < 15; i++)
        {
            deckList.Add(i.ToString() + "_spade");
            deckList.Add(i.ToString() + "_heart");
            deckList.Add(i.ToString() + "_diamond");
            deckList.Add(i.ToString() + "_club");
        }
    }
}
public class Hand
{

    Random rnd = new Random();
    
    public List<string> data_hand = new List<string>();
    private string first_card;
    private string second_card;
    public int balance = 1000;
    public int comb = 1;
    public Dictionary<int, string> combs_dict = new Dictionary<int, string>()
    {
        { 1 , "High card" },
        { 2 , "Pair" },
        { 3 , "Two pairs" },
        { 4 , "Three of a kind" },
        { 5 , "Straight" },
        { 6 , "Flush" },
        { 7 , "Full house" },
        { 8 , "Four of a kind" },
        { 9 , "Straight flush" },
        { 10 , "Royal flush" },
    };
    // 1 : high card
    // 2 : pair
    // 3 : two pairs
    // 4 : three of a kind
    // 5 : straight
    // 6 : flush
    // 7 : full house
    // 8 : four of a kind
    // 9 : straight flush
    // 10 : royal flush
    
    public string FirstCardGetter { get { return first_card; } }
    public string FirstCardSetter { set { first_card = value; } }
    public string SecondCardGetter { get { return second_card; } }
    public string SecondCardSetter { set { second_card = value; } }

    public int score;
    public int bet = 0;
    public int combination_value;
    public int flush_count = 0;

    public Hand()
    {
        if (Deck.deck_made == false)
        {
            Deck.DeckGenerate();
            Deck.deck_made = true;
        }
        int index = rnd.Next(0, Deck.deckList.Count);
        FirstCardSetter = Deck.deckList[index];
        Deck.deckList.Remove(Deck.deckList[index]);
        index = rnd.Next(0, Deck.deckList.Count);
        SecondCardSetter = Deck.deckList[index];
        Deck.deckList.Remove(Deck.deckList[index]);
        
        int f_space_index = first_card.IndexOf('_');
        int s_space_index = second_card.IndexOf('_');

        score += Convert.ToInt32(first_card[0..f_space_index]);
        score += Convert.ToInt32(second_card[0..s_space_index]);

        data_hand.Add($"{first_card[0..f_space_index]}");
        data_hand.Add($"{first_card[f_space_index..first_card.Count()]}");
        data_hand.Add($"{second_card[0..s_space_index]}");
        data_hand.Add($"{second_card[s_space_index..second_card.Count()]}");

        if (first_card[(f_space_index + 1)..(first_card.Length)] == second_card[(s_space_index + 1)..(second_card.Length)])
        {
            score = Convert.ToInt32(Math.Round(score * 1.25));
            flush_count = 2;
        }
        if (first_card[0..f_space_index] == second_card[0..s_space_index])
        {
            score *= 2;
        }

    }

}
public class Table
{
    
    public static List<string> cards = new List<string>();
    public static int first_space;
    public static int second_space;
    public static int third_space;
    public static int fourth_space;
    public static int fifth_space;
    public static List<string> data_table = new List<string>();
    public static void TableGenerator()
    {
        Random rnd = new Random();
        for (int i = 0; i < 5; i++)
	    {
            int index = rnd.Next(0, Deck.deckList.Count);
            cards.Add(Deck.deckList[index]);
            Deck.deckList.Remove(Deck.deckList[index]);
	    }
        first_space = cards[0].IndexOf("_");
        second_space = cards[1].IndexOf("_");
        third_space = cards[2].IndexOf("_");
        fourth_space = cards[3].IndexOf("_");
        fifth_space = cards[4].IndexOf("_");
    }
    
}