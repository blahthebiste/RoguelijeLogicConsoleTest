public class EyeOfTheStorm : Action
{

    public EyeOfTheStorm()
    {
        this.name = "Eye of the Storm";
        this.description = "Apply 8 Frost to ALL enemies. Can only be used if Chill is out of uses.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 8;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.ALL_ENEMIES;
    }

    // This will apply 8 frost to the target. Will be run on each enemy.
    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this + "'.");
            return false;
        }
        // Not affected by spell power or charged.
        // Apply the frost to the target.
        target.AddStatusEffect(new Frost(magicNumber, target));
        return true;
    }
    
    // Can only be used if Chill is at 0 uses
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this + "'.");
            return false;
        }
        foreach (Action act in this.owner.ActionList)
        {
            if (act.name == "Chill" && act.uses > 0)
            {
                Console.WriteLine(this.owner.name + " cannot use "+this.name+", Chill still has uses left!");
                return false;
            }
        }
        return base.canUse(target, modifier);
    }
}