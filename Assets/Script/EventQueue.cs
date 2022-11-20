using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    // 작업 큐 생성
    List<Timimg> eventList;
    bool flag;

    // Start is called before the first frame update
    void Awake()
    {
        eventList = new List<Timimg>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            RunQueue();
        }
    }

    /// <summary>
    ///  큐에 들어온 것들을 
    /// </summary>
    void RunQueue()
    {
        eventList.ForEach((time) =>
        {
            switch(time.effect)
            {
                case "before":
                    // TODO: 기본행동이나 카드 사용 전에 체크
                    break;
                case "forward":
                    Forward(time.me, time.game);
                    break;
                case "yurinaN5-1":
                    AddFocus(time.me, 1);
                    break;
                default:
                    break;
            }
        });

        flag = false;

        eventList[0].game.UpdateWindow();
        Destroy(gameObject);
    }

    /// <summary>
    ///  기본동작을 이벤트큐에 추가
    /// </summary>
    public void AddBasicActionTiming(Player me, SingleGame game, string type)
    {
        eventList.Add(new Timimg(me, game, "basicAction", "before"));
        eventList.Add(new Timimg(me, game, "basicAction", type));
        flag = true;
    }

    public void AddCardTiming(Player me, Player you, SingleGame game, string type)
    {

    }

    void Forward(Player player, SingleGame game)
    {
        int n = CountAvailableSakura(game.distance, 1);
        if(player.aura + n > player.maxAura)
        {
            return;
        }

        int distance = game.distance;
        int aura = player.aura;

        moveSakura(ref distance, ref aura, 1);

        game.distance = distance;
        player.aura = aura;

        Debug.Log(player.aura);
        Debug.Log(game.distance);
    }


    /// <summary>
    ///  집중력 추가
    /// </summary>
    void AddFocus(Player player, int num)
    {
        // TODO: 위축 상태일 때 num을 1 깎는 거 추가
        if(player.focus + num > 2)
            player.focus = num;
        else
            player.focus += num;
    }

    void DrawCard(Player player, int num)
    {
        for(int i = 0; i < num; i++)
        {
            if(player.deck.Count > 0)
            {
                Card card = player.deck[0];
                player.deck.RemoveAt(0);
                player.hand.Add(card);
            }
        }
    }

    int CountAvailableSakura(int startLocation, int count)
    {
        int n = count;
        if(startLocation <= n)
        {
            n = startLocation;
        }

        return n;
    }

    void moveSakura(ref int startLocation, ref int endLocation, int count)
    {
        if(startLocation <= count)
        {
            count = startLocation;
        }

        startLocation -= count;
        endLocation += count;
    }

}