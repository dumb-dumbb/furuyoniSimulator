using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{

    public const string ATTACK = "attack";
    public const string ACTION = "action";
    public const string TEST = "test";

    public string cardType;
    public string cardName;
    public List<Timimg> effectList;
    public int Id;

    public Card(string t, string name)
    {
        cardType = t;
        cardName = name;
        effectList = new List<Timimg>();

        Id = (int)(Random.value * 100);

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
