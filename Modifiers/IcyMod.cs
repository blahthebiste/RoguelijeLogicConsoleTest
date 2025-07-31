public class IcyMod : Modifier {
    
    public IcyMod()
    {
        this.name = "Icy";
        this.description = "Applies 1 Frost to the target.";
    }

    public override void useOnTarget(Action act, Entity? target)
    {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target passed to modifier " + this.name + "!");
            return;
        }
        Console.WriteLine("Applying Frost due to Icy modifier!");
        target.AddStatusEffect(new Frost(1, target));
    }
}