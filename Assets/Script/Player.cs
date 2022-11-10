using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    List<Card> deck;
    List<Card> specialDeck;
    List<Card> usedCards;
    List<Card> discardCards;
    List<Card> hand;

    public int aura { get; set; }
    public int flare { get; set; }
    public int life { get; set; }

    void Start()
    {
        deck = new List<Card>();
        specialDeck = new List<Card>();
        usedCards = new List<Card>();
        discardCards = new List<Card>();
        hand = new List<Card>();

        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());
        deck.Add(new Card());

        specialDeck.Add(new Card());
        specialDeck.Add(new Card());
        specialDeck.Add(new Card());
        aura = 3;
        flare = 0;
        life = 10;
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
