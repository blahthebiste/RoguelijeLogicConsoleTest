public class PiousMod : Modifier {
    
    public PiousMod()
    {
        this.name = "Pious";
        this.description = "When played, gain Piety for a turn.";
    }

    public override void useOnce(Action act)
    {
        if (act.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action passed to modifier " + this.name + "!");
            return;
        }
        Console.WriteLine("Gaining Piety due to Pious modifier!");
        act.owner.AddStatusEffect(new Piety(1, act.owner));
    }
}