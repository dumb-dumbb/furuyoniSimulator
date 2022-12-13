using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleGame : MonoBehaviour
{
    public Player me;
    public Player you;
    public GameObject eventQueue;
    public GameObject card;

    public GameObject distanceText;
    public GameObject dustText;
    public GameObject myAuraText;
    public GameObject myLifeText;
    public GameObject myFlareText;
    public GameObject myFocusText;
    public GameObject hand;

    public int distance { get; set; }
    public int dust { get; set; }

    public const int MAX_DISTANCE = 10;

    // Start is called before the first frame update
    void Start()
    {
        distance = 10;
        dust = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    ///  카드 드로우
    /// </summary>
    public void Draw(Player mePlayer, Player youPlayer, int count)
    {
        GameObject queue = Instantiate(eventQueue);
        queue.GetComponent<EventQueue>().AddCardTiming(mePlayer, youPlayer, this, count);

    }

    /// <summary>
    ///  기본행동을 했을 때 작업큐에 이벤트를 추가함
    /// </summary>
    public void UseBasicAction(string type)
    {
        GameObject queue = Instantiate(eventQueue);
        queue.GetComponent<EventQueue>().AddBasicActionTiming(me, this, "forward");
    }

    public void OnBasicActionButtonClick()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        string type = button.GetComponent<BasicButton>().type;

        //Debug.Log(type);
        UseBasicAction(type);
        //UpdateWindow();
    }

    public void OnTestDrawClick()
    {
        Draw(me, you, 1);
    }

    public void UpdateWindow()
    {
        int aura = me.aura;
        int life = me.life;
        int flare = me.flare;
        int focus = me.focus;

        distanceText.GetComponent<Text>().text = distance.ToString();
        myAuraText.GetComponent<Text>().text = aura.ToString();
        myLifeText.GetComponent<Text>().text = life.ToString();
        myFlareText.GetComponent<Text>().text = flare.ToString();
        myFocusText.GetComponent<Text>().text = focus.ToString();

        Debug.Log(me.hand.Count);

    }

    public void ResetHandPanel(Card c)
    {
        // TODO : 1장씩 삭제하는 것 구현
        if(me.hand.Contains(c))
        {
            var handList = hand.GetComponentsInChildren<Transform>();
            
            me.hand.Remove(c);
        }


        //var childList = hand.GetComponentsInChildren<Transform>();
        //if(childList != null)
        //{
        //    for (int i = 1; i < childList.Length; i++)
        //        if (childList[i] != hand.transform)
        //            Destroy(childList[i]);
        //}
    }

    public void SetHandPanel(Card c)
    {
        GameObject obj;
        int n = 0;
        n = hand.GetComponentsInChildren<Transform>().Length - 1;
        Debug.Log(n);
        obj = Instantiate(card, hand.transform);
        obj.GetComponent<CardScript>().card = c;
        obj.transform.localPosition = new Vector3(-20 + 60 * n, 0, 0);
    }
}
