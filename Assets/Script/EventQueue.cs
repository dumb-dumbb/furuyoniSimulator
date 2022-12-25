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
                case "backward":
                    Backward(time.me, time.game);
                    break;
                case "recover":
                    Recover(time.me, time.game);
                    break;
                case "concentrate":
                    Concentrate(time.me, time.game);
                    break;
                case "breakaway":
                    Breakaway(time.me, time.game);
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

    /// <summary>
    /// 기본동작/전진
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Forward(Player player, SingleGame game)
    {
        int n = CountAvailableSakura(game.distance, 1);

        if(player.GetAura() + n > player.GetMaxAura())
        {
            return;
        }

        int distance = game.distance;
        int aura = player.GetAura();

        moveSakura(ref distance, ref aura, 1);

        game.distance = distance;
        player.SetAura(aura);

        Debug.Log(player.GetAura());
        Debug.Log(game.distance);
    }

    /// <summary>
    /// 기본동작/후퇴
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Backward(Player player, SingleGame game)
    {
        int aura = player.GetAura();
        int distance = game.distance;
        int n = CountAvailableSakura(aura, 1);

        if (aura <= 0 || distance >= 10)
        {
            return;
        }

        moveSakura(ref aura, ref distance, 1);

        game.distance = distance;
        player.SetAura(aura);

        Debug.Log(player.GetAura());
        Debug.Log(game.distance);
    }

    /// <summary>
    /// 기본동작/휘감기
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Recover(Player player, SingleGame game)
    {
        int aura = player.GetAura();
        int dust = game.dust;

        int n = CountAvailableSakura(game.dust, 1);

        if (player.GetAura() >= player.GetMaxAura() || dust <= 0)
        {
            return;
        }

        moveSakura(ref dust, ref aura, 1);

        game.dust = dust;
        player.SetAura(aura);

        Debug.Log(player.GetAura());
        Debug.Log(game.dust);
    }

    /// <summary>
    /// 기본동작/품기
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Concentrate(Player player, SingleGame game)
    {
        int aura = player.GetAura();
        int flare = player.GetFlare();

        int n = CountAvailableSakura(game.dust, 1);

        if (player.GetAura() <= 0)
        {
            return;
        }

        moveSakura(ref aura, ref flare, 1);

        player.SetFlare(flare);
        player.SetAura(aura);

        Debug.Log(player.GetAura());
        Debug.Log(player.GetAura());
    }

    /// <summary>
    /// 기본동작/이탈
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Breakaway(Player player, SingleGame game)
    {
        int dust = game.dust;
        int distance = game.distance;

        if (dust <= 0 || distance >= 10)
        {
            return;
        }

        moveSakura(ref dust, ref distance, 1);

        game.dust = dust;
        game.distance = distance;

        Debug.Log(game.dust);
        Debug.Log(game.distance);
    }

    /// <summary>
    ///  집중력 추가
    /// </summary>
    void AddFocus(Player player, int num)
    {
        // TODO: 위축 상태일 때 num을 1 깎는 거 추가
        if(player.GetFocus() + num > 2)
            player.SetFocus(2);
        else
            player.SetFocus(player.GetFocus() + num);
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