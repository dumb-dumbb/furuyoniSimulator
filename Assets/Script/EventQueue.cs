using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    // �۾� ť ����
    List<Timimg> eventList;
    bool flag;

    // Start is called before the first frame update
    void Awake()
    {
        eventList = new List<Timimg>();
    }

    private void StartQueue()
    {
        flag = true;
        StartCoroutine(RunQueue());
    }


    /// <summary>
    ///  ť�� ���� �͵��� 
    /// </summary>
    IEnumerator RunQueue()
    {
        for(int i = 0; i < eventList.Count; i++)
        {
            yield return new WaitUntil(() => flag);
            Timimg time = eventList[i];
            Debug.Log(time.effect);
            Debug.Log(i + " " + eventList.Count);
            switch (time.effect)
            {
                case "before":
                    // TODO: �⺻�ൿ�̳� ī�� ��� ���� üũ
                    //if(time.tag.Equals("drawCard")) // ť�� �߰��ؾ� �� ��� �̷��� ���� ����
                    //{
                    //    int j = eventList.IndexOf(time);
                    //    eventList.Insert(j+1, new Timimg(time.me, time.you, time.game, "drawCard", "after"));
                    //}
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
        }

        Destroy(gameObject);
    }


    /// <summary>
    ///  �⺻������ �̺�Ʈť�� �߰�
    /// </summary>
    public void AddBasicActionTiming(Player me, SingleGame game, string type)
    {
        eventList.Add(new Timimg(me, game, "basicAction", "before"));
        eventList.Add(new Timimg(me, game, "basicAction", type));
        eventList.Add(new Timimg(me, game, "basicAction", "after"));
        StartQueue();
    }

    public void AddDrawTiming(Player me, Player you, SingleGame game, int count)
    {
        eventList.Add(new Timimg(me, you, game, "drawCard", "before"));
        for(int i = 0; i < count; i++)
        {
            eventList.Add(new Timimg(me, you, game, "drawCard", "draw"));
        }
        eventList.Add(new Timimg(me, you, game, "drawCard", "after"));
        StartQueue();
    }

    public void AddCardTiming(Player me, Player you, SingleGame game, Card card)
    {
        eventList.Add(new Timimg(me, you, game, card.cardType, "before"));
        for(int i = 0; i < card.effectList.Count; i++)
        {
            eventList.Add(new Timimg(me, you, game, card.effectList[i].tag, card.effectList[i].effect));
        }
        eventList.Add(new Timimg(me, you, game, card.cardType, "after"));
        StartQueue();
    }

    /// <summary>
    /// �⺻����/����
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Forward(Player player, SingleGame game)
    {
        //int n = CountAvailableSakura(game.GetDistance(), 1);

        if(player.GetAura() + 1 > player.GetMaxAura())
        {
            return;
        }

        int distance = game.GetDistance();
        int aura = player.GetAura();

        moveSakura(ref distance, ref aura, 1);

        game.SetDistance(distance);
        player.SetAura(aura);

        Debug.Log(player.GetAura());
        Debug.Log(game.GetDistance());
    }

    /// <summary>
    /// �⺻����/����
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Backward(Player player, SingleGame game)
    {
        int aura = player.GetAura();
        int distance = game.GetDistance();
        //int n = CountAvailableSakura(aura, 1);

        if (aura <= 0 || distance >= 10)
        {
            return;
        }

        moveSakura(ref aura, ref distance, 1);

        game.SetDistance(distance);
        player.SetAura(aura);

        Debug.Log(player.GetAura());
        Debug.Log(game.GetDistance());
    }

    /// <summary>
    /// �⺻����/�ְ���
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Recover(Player player, SingleGame game)
    {
        int aura = player.GetAura();
        int dust = game.GetDust();

        //int n = CountAvailableSakura(game.GetDust(), 1);

        if (player.GetAura() >= player.GetMaxAura() || dust <= 0)
        {
            return;
        }

        moveSakura(ref dust, ref aura, 1);

        game.SetDust(dust);
        player.SetAura(aura);

        Debug.Log(player.GetAura());
        Debug.Log(game.GetDust());
    }

    /// <summary>
    /// �⺻����/ǰ��
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Concentrate(Player player, SingleGame game)
    {
        int aura = player.GetAura();
        int flare = player.GetFlare();

        //int n = CountAvailableSakura(game.dust, 1);

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
    /// �⺻����/��Ż
    /// </summary>
    /// <param name="player"></param>
    /// <param name="game"></param>
    private void Breakaway(Player player, SingleGame game)
    {
        int dust = game.GetDust();
        int distance = game.GetDistance();

        if (dust <= 0 || distance >= 10)
        {
            return;
        }

        moveSakura(ref dust, ref distance, 1);

        game.SetDust(dust);
        game.SetDistance(distance);

        Debug.Log(game.GetDust());
        Debug.Log(game.GetDistance());
    }

    /// <summary>
    ///  ���߷� �߰�
    /// </summary>
    void AddFocus(Player player, int num)
    {
        // TODO: ���� ������ �� num�� 1 ��� �� �߰�
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
            // TODO: ������, GetDamage ���?
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
        //TODO : ���� ����
        
    }

    void GetDamage(Player me, int auraDamage, int lifeDamage)
    {

    }

}