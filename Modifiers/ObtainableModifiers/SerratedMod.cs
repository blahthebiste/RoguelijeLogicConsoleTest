public class SerratedMod : Modifier {
    
    public SerratedMod()
    {
        this.name = "Serrated";
        this.description = "Applies 1 Bleed to the target.";
    }

    public override void useOnTarget(Action act, Entity? target)
    {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target passed to modifier " + this.name + "!");
            return;
        }
        Console.WriteLine("Applying Bleed due to Serrated modifier!");
        target.AddStatusEffect(new Bleed(1, target));
    }
}