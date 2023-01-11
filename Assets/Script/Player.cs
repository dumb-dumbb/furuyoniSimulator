using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variable

    // Start is called before the first frame update
    public List<Card> deck;
    public List<Card> specialDeck;
    public List<Card> usedCards;
    public List<Card> discardCards;
    public CardList<Card> hand;
    public GameObject cardPrefab;
    
    private int _aura; // 오라
    private int _flare; // 플레어
    private int _life; // 라이프
    private int _maxAura; // 최대오라
    private int _focus; // 집중력
    private bool _isMe; // 플레이어 본인 여부

    public const int MAX_FOCUS = 2;

    private Text _auraText;
    private Text _flareText;
    private Text _lifeText;
    private Text _focusText;

    private GameObject handView;
    private Dictionary<string, GameObject> handObjList;

    #endregion

    #region GetterSetter

    public int GetAura()
    {
        return _aura;
    }

    public void SetAura(int value)
    {
        _aura = value;
        _auraText.text = _aura.ToString();
    }


    public int GetFlare()
    {
        return _flare;
    }

    public void SetFlare(int value)
    {
        _flare = value;
        _flareText.text = _flare.ToString();
    }


    public int GetLife()
    {
        return _life;
    }

    public void SetLife(int value)
    {
        _life = value;
        _lifeText.text = _life.ToString();
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
        if(_focus < MAX_FOCUS)
        {
            _focus = value;
            _focusText.text = _focus.ToString();
        }
    }

    public void SetAuraText(Text value)
    {
        _auraText = value;
    }

    public void SetFlareText(Text value)
    {
        _flareText = value;
    }

    public void SetLifeText(Text value)
    {
        _lifeText = value;
    }

    public void SetFocusText(Text value)
    {
        _focusText = value;
    }

    public bool GetIsMe()
    {
        return _isMe;
    }

    public void SetIsMe(bool value)
    {
        _isMe = value;
    }

    #endregion

    void Start()
    {
        deck = new List<Card>();
        specialDeck = new List<Card>();
        usedCards = new List<Card>();
        discardCards = new List<Card>();
        hand = new CardList<Card>();
        handObjList = new Dictionary<string, GameObject>();

        if (this.name.Equals("Me"))
            SetIsMe(true);
        else
            SetIsMe(false);

        if(GetIsMe())
        {
            SetAuraText(GameObject.Find("Aura").GetComponent<Text>());
            SetFlareText(GameObject.Find("Flare").GetComponent<Text>());
            SetLifeText(GameObject.Find("Life").GetComponent<Text>());
            SetFocusText(GameObject.Find("Focus").GetComponent<Text>());
            handView = GameObject.Find("MyHand");

            hand.AddCardInUi = (card) =>
            {
                string id = card.Id;
                handObjList[id].SetActive(true);
            };
            hand.RemoveCardInUi = (card) =>
            {
                string id = card.Id;
                handObjList[id].SetActive(false);
            };

            deck.Add(new Card("ACTION", "na_01_yurina_o_n_5_s5"));
            deck.Add(new Card("ATTACK", "na_01_yurina_o_n_1"));

            //deck.Add(new Card());
            //deck.Add(new Card());
            //deck.Add(new Card());
            //deck.Add(new Card());
            //deck.Add(new Card());

            //specialDeck.Add(new Card());
            //specialDeck.Add(new Card());
            //specialDeck.Add(new Card());

            for (int i = 0; i < deck.Count; i++)
            {
                CreateCardObj(deck[i]);
            }

            SetAura(3);
            SetFlare(0);
            SetLife(10);
            SetMaxAura(5);
            SetFocus(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateCardObj(Card c)
    {
        GameObject obj;
        int n = 0;
        Debug.Log(n);
        obj = Instantiate(cardPrefab, handView.transform);
        obj.GetComponent<CardScript>().SetAttribute(GameObject.Find("GameEngine").GetComponent<SingleGame>(), c);
        obj.name = c.Id;
        handObjList.Add(c.Id, obj);
        handObjList[c.Id].SetActive(false);
        Debug.Log(obj);
    }
}
