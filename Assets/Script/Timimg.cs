class Timimg
{
    public Player me{ get; set; }
    public Player you { get; set; }
    public string tag { get; set; } // tag 배열로 선언?
    public string effect { get; set; }

    public Timimg(Player me, Player you, string tag, string effect)
    {
        this.me = me;
        this.you = you;
        this.tag = tag;
        this.effect = effect;
    }


}