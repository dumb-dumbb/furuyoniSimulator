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

    public GameObject distanceText;
    public GameObject dustText;
    public GameObject myAuraText;
    public GameObject myLifeText;
    public GameObject myFlareText;
    public GameObject myFocusText;

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
    public void Draw(Player player, int count)
    {

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
        GameObject queue = Instantiate(eventQueue);
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

    }
}
