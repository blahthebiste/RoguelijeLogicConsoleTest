public class Imbue : Action {

    public Imbue() {
        this.name = "Imbue";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ALLY;
        this.description = "Grant an ally +"+magicNumber+" Strength.";
    }


    // Requires an ally, cannot target self
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        if (target == this.owner)
        {
            Console.WriteLine(this.name + " cannot target self!");
            return false;
        }
        return base.canUse(target, modifier);
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        // Apply strength
        target.AddStatusEffect(new Strength(magicNumber, target));
        return true;
    }
}