public class Rage : Action {

    public Rage() {
        this.name = "Rage";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 5;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "If HP is half or below, gain "+magicNumber+" Strength.";
    }

    // Can only use if at or below half HP
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        int halfHPThreshold = (int)Math.Ceiling(owner.maxHP/2.0);
        if(owner.currentHP > halfHPThreshold)
        {
            Console.WriteLine("Cannot use Rage! HP must be less than or equal to "+halfHPThreshold);
            return false;
        }
        return base.canUse(target, modifier);
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Strength status effect
        Console.WriteLine("RRRRRRAAAGGGHHH!!!");
        this.owner.AddStatusEffect(new Strength(magicNumber, this.owner));
        return true;
    }
}