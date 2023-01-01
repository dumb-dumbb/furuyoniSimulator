using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleGame : MonoBehaviour
{
    #region Variable
    public Player me;
    public Player you;
    public GameObject eventQueue;
    public GameObject card;

    private Text _distanceText;
    private Text _dustText;
    public GameObject hand;

    public GameObject myBreakAwayButton;

    private bool canPanelOpened;

    private int _distance;
    private int _dust;

    public const int MAX_DISTANCE = 10;
    #endregion

    #region GetterSetter

    public int GetDistance()
    {
        return _distance;
    }
    public void SetDistance(int value)
    {
        _distance = value;
        _distanceText.text = $"{_distance}/10";
    }
    public int GetDust()
    {
        return _dust;
    }
    public void SetDust(int value)
    {
        _dust = value;
        _dustText.text = _dust.ToString();
    }
    public void SetDistanceText(Text value)
    {
        _distanceText = value;
    }
    public void SetDustText(Text value)
    {
        _dustText = value;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetDistanceText(GameObject.Find("Distance").GetComponent<Text>());
        SetDustText(GameObject.Find("Dust").GetComponent<Text>());
        SetDistance(10);
        SetDust(10);
        canPanelOpened = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ClearPopUp();
        }
    }

    /// <summary>
    ///  카드 드로우
    /// </summary>
    public void Draw(Player mePlayer, Player youPlayer, int count)
    {
        GameObject queue = Instantiate(eventQueue);
        queue.GetComponent<EventQueue>().AddDrawTiming(mePlayer, youPlayer, this, count);

    }

    /// <summary>
    ///  기본행동을 했을 때 작업큐에 이벤트를 추가함
    /// </summary>
    public void UseBasicAction(string type)
    {
        GameObject queue = Instantiate(eventQueue);
        queue.GetComponent<EventQueue>().AddBasicActionTiming(me, this, type);
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

    //public void SetHandPanel(Card c)
    //{
    //    GameObject obj;
    //    int n = 0;
    //    n = hand.GetComponentsInChildren<Transform>().Length - 1;
    //    Debug.Log(n);
    //    obj = Instantiate(card, hand.transform);
    //    obj.GetComponent<CardScript>().SetAttribute(this, c);
    //    obj.GetComponent<Button>().onClick.AddListener(ClickCard);
    //    obj.transform.localPosition = new Vector3(-20 + 60 * n, 0, 0);
    //}

    public void ClearPopUp()
    {
        GameObject[] panelArr = GameObject.FindGameObjectsWithTag("panel");
        for(int i = 0; i < panelArr.Length; i++)
        {
            panelArr[i].SetActive(false);
        }
    }

    public void UseCard(Card card)
    {
        // TODO : 카드 사용
        GameObject queue = Instantiate(eventQueue);
        queue.GetComponent<EventQueue>().AddCardTiming(me, you, this, card);
    }

    public void ClickCard()
    {
        ClearPopUp();

        if(canPanelOpened)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z));
            GameObject card = EventSystem.current.currentSelectedGameObject;

            card.GetComponent<CardScript>().ShowPanel(point);
        }
    }
}
