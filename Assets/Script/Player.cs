using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Card> deck;
    public List<Card> specialDeck;
    public List<Card> usedCards;
    public List<Card> discardCards;
    public List<Card> hand;

    public int aura { get; set; }
    public int flare { get; set; }
    public int life { get; set; }
    public int maxAura { get; set; }
    public int focus { get; set; }

    public const int MAX_FOCUS = 2;

    void Start()
    {
        deck = new List<Card>();
        specialDeck = new List<Card>();
        usedCards = new List<Card>();
        discardCards = new List<Card>();
        hand = new List<Card>();

        deck.Add(new Card(Card.type.ACTION, "±â¹é"));
        //deck.Add(new Card());
        //deck.Add(new Card());
        //deck.Add(new Card());
        //deck.Add(new Card());
        //deck.Add(new Card());
        //deck.Add(new Card());

        //specialDeck.Add(new Card());
        //specialDeck.Add(new Card());
        //specialDeck.Add(new Card());
        aura = 3;
        flare = 0;
        life = 10;
        maxAura = 5;
        focus = 1;
    }

    //private void Initialize()
    //{
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {

    }
}
