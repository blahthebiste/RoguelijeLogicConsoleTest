public class FlankingMod : Modifier {
    
    public FlankingMod()
    {
        this.name = "Flanking";
        this.description = "Applies 1 Vulnerable to the target.";
    }

    public override void useOnTarget(Action act, Entity? target)
    {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target passed to modifier " + this.name + "!");
            return;
        }
        Console.WriteLine("Applying Vulnerable due to Flanking modifier!");
        target.AddStatusEffect(new Vulnerable(1, target));
    }
}