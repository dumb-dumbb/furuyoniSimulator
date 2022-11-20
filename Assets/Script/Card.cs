using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum type
    {
        ATTACK,
        ACTION,
        TEST
    }

    public type cardType;
    public string cardName;
    public List<Timimg> effectList;

    public Card(type t, string name)
    {
        cardType = t;
        cardName = name;
        effectList = new List<Timimg>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
