public class BoldMod : Modifier {
    
    public BoldMod()
    {
        this.name = "Bold";
        this.description = "When played, gain Taunt for a turn.";
    }

    public override void useOnce(Action act)
    {
        if (act.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action passed to modifier " + this.name + "!");
            return;
        }
        Console.WriteLine("Gaining Taunt due to Bold modifier!");
        act.owner.AddStatusEffect(new Taunting(1, act.owner));
    }
}