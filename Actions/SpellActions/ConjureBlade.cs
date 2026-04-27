public class ConjureBlade : Action {

    public ConjureBlade() {
        this.name = "Conjure Blade";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 3;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Replace Strike with Twin Slash for "+magicNumber+" uses.";
    }

    // Can only use if Strike is in action list.
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        foreach (Action act in this.owner.ActionListMinusPassives)
        {
            if (act.name == "Strike")
            {
                return base.canUse(target, modifier);
            }
        }
        Console.WriteLine(this.owner.name+" cannot use " + this.name + ", no Strike action to replace!");
        return false;
    }

    public override bool useOnce(Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        // Apply ConjuredBlade status effect
        this.owner.AddStatusEffect(new ConjuredBlade(magicNumber, this.owner));
        return true;
    }
}