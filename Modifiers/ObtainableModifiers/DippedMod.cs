public class DippedMod : Modifier {
    
    public DippedMod()
    {
        this.name = "Dipped";
        this.description = "Applies 1 Poison to the target.";
    }

    public override void useOnTarget(Action act, Entity? target)
    {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target passed to modifier " + this.name + "!");
            return;
        }
        Console.WriteLine("Applying Poison due to Dipped modifier!");
        target.AddStatusEffect(new Poison(1, target));
    }
}