using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private bool _isSpecial;
    //전력여부
    private bool _isThroughOut;
    private string cardType;
    public string cardName;
    public List<Timimg> effectList;
    private string id;

    public string CardType { get => cardType;}
    public string Id { get => id;}

    public Card(string t, string name)
    {
        cardType = t;
        cardName = name;
        effectList = new List<Timimg>();

        this.id = name + (int)(Random.value * 100);

        switch(name)
        {
            case "na_01_yurina_o_n_5_s5":
                effectList.Add(new Timimg(cardType, "na_01_yurina_o_n_5_s5_1"));
                effectList.Add(new Timimg(cardType, "na_01_yurina_o_n_5_s5_2"));
                break;
            case "na_01_yurina_o_n_1":
                effectList.Add(new Timimg(cardType, "na_01_yurina_o_n_1"));
                break;
        }
    }
}
