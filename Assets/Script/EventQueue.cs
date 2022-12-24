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
        // TODO: 모든 버튼 비활성화, 팝업 닫기
        eventList.ForEach((time) =>
        {
            Debug.Log(time.effect);
            switch(time.effect)
            {
                case "before":
                    // TODO: 기본행동이나 카드 사용 전에 체크
                    break;
                case "after":
                    break;
                case "forward":
                    Forward(time.me, time.game);
                    break;
                case "na_01_yurina_o_n_5_s5_1":
                    AddFocus(time.me, 1);
                    break;
                case "na_01_yurina_o_n_5_s5_2":
                    //AddFocus(time.me, 1);
                    break;
                case "na_01_yurina_o_n_1_1":
                    break;
                case "draw":
                    DrawCard(time.me, time.game);
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
        eventList.Add(new Timimg(me, game, "basicAction", "after"));
        flag = true;
    }

    public void AddDrawTiming(Player me, Player you, SingleGame game, int count)
    {
        eventList.Add(new Timimg(me, you, game, "drawCard", "before"));
        for(int i = 0; i < count; i++)
        {
            eventList.Add(new Timimg(me, you, game, "drawCard", "draw"));
        }
        eventList.Add(new Timimg(me, you, game, "drawCard", "before"));
        flag = true;
    }

    public void AddCardTiming(Player me, Player you, SingleGame game, Card card)
    {
        eventList.Add(new Timimg(me, you, game, card.cardType, "before"));
        for(int i = 0; i < card.effectList.Count; i++)
        {
            eventList.Add(new Timimg(me, you, game, card.effectList[i].tag, card.effectList[i].effect));
        }
        eventList.Add(new Timimg(me, you, game, card.cardType, "after"));
        flag = true;

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
            player.focus = 2;
        else
            player.focus += num;
    }

    void DrawCard(Player player, SingleGame game)
    {
        if (player.deck.Count > 0)
        {
            Card card = player.deck[0];
            player.deck.RemoveAt(0);
            player.hand.Add(card);
            game.SetHandPanel(card);
        }
        else
        {
            // TODO: 초조뎀, GetDamage 사용?
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

    void Attack(Player me, Player you, int auraDamage, int lifeDamage)
    {
        //TODO : 공격 구현
        
    }

    void GetDamage(Player me, int auraDamage, int lifeDamage)
    {

    }

}