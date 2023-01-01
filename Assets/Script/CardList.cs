using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList<T> : List<T> where T : Card
{
    public delegate void EditCardList(Card c);
    public EditCardList AddCardInUi;
    public EditCardList RemoveCardInUi;
    public void AddCard(T c)
    {
        base.Add(c);
        AddCardInUi(c);
    }
    public void RemoveCard(T c)
    {
        base.Remove(c);
        RemoveCardInUi(c);
    }
}
