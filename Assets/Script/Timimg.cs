public class Timimg
{
    public Player me{ get; set; }
    public Player you { get; set; }
    public string tag { get; set; } // TODO: tag 배열로 선언?
    public string effect { get; set; }
    public SingleGame game { get; set; }

    public Timimg(Player me, Player you, SingleGame game, string tag, string effect)
    {
        this.me = me;
        this.you = you;
        this.game = game;
        this.tag = tag;
        this.effect = effect;
    }
    public Timimg(Player me, SingleGame game, string tag, string effect)
    {
        this.me = me;
        this.game = game;
        this.tag = tag;
        this.effect = effect;
    }


}