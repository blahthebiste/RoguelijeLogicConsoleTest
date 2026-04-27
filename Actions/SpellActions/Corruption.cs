public class Corruption : Action {

    public Corruption() {
        this.name = "Corruption";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 3;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ALLY;
        this.description = "Apply "+magicNumber+" Vulnerability to my master.";
    }


    // Requires an ally, must target master
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        if (!this.owner.hostile)
        {
            Console.WriteLine("ERROR: owner must be enemy for action '" + this.name + "'.");
            return false;
        }
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        if (this.owner.master == null)
        {
            Console.WriteLine(this.name + " requires a master!");
            return false;
        }
        if (target.name != this.owner.master)
        {
            Console.WriteLine(this.name + " must target master!");
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
        // Apply Vulnerability
        target.AddStatusEffect(new Vulnerability(magicNumber, target));
        return true;
    }
}