using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable

    // Start is called before the first frame update
    public List<Card> deck;
    public List<Card> specialDeck;
    public List<Card> usedCards;
    public List<Card> discardCards;
    public List<Card> hand;
    
    private int _aura;
    private int _flare;
    private int _life;
    private int _maxAura;
    private int _focus;

    public const int MAX_FOCUS = 2;

    #endregion

    #region GetterSetter

    public int GetAura()
    {
        return _aura;
    }

    public void SetAura(int value)
    {
        _aura = value;
    }


    public int GetFlare()
    {
        return _flare;
    }

    public void SetFlare(int value)
    {
        _flare = value;
    }


    public int GetLife()
    {
        return _life;
    }

    public void SetLife(int value)
    {
        _life = value;
    }


    public int GetMaxAura()
    {
        return _maxAura;
    }

    public void SetMaxAura(int value)
    {
        _maxAura = value;
    }


    public int GetFocus()
    {
        return _focus;
    }

    public void SetFocus(int value)
    {
        _focus = value;
    }

    #endregion

    void Start()
    {
        deck = new List<Card>();
        specialDeck = new List<Card>();
        usedCards = new List<Card>();
        discardCards = new List<Card>();
        hand = new List<Card>();

        deck.Add(new Card(Card.ACTION, "na_01_yurina_o_n_5_s5"));
        deck.Add(new Card(Card.ATTACK, "na_01_yurina_o_n_1"));
        //deck.Add(new Card());
        //deck.Add(new Card());
        //deck.Add(new Card());
        //deck.Add(new Card());
        //deck.Add(new Card());

        //specialDeck.Add(new Card());
        //specialDeck.Add(new Card());
        //specialDeck.Add(new Card());
        SetAura(3);
        SetFlare(0);
        SetLife(10);
        SetMaxAura(5);
        SetFocus(1);
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
