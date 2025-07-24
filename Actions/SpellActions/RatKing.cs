public class RatKing : Action {

    public RatKing() {
        this.name = "Rat-King";
        this.description = "Kill self. Give Giant Rat +7 HP and +1 Strength.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 7;
        this.magicNumber2 = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ALLY;
    }


    // Requires an allied Giant Rat.
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this + "'.");
            return false;
        }
        if (target == null)
        {
            Console.WriteLine(this.owner.name + " cannot use Rat-King without a target!");
            return false;
        }
        if (target.name != "Giant Rat")
        {
            Console.WriteLine(this.owner.name + " can only use Rat-King on the Giant Rat!");
            return false;
        }
        return base.canUse(target, modifier);
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this + "'.");
            return false;
        }
        if (target == null)
        {
            Console.WriteLine(this.owner.name + " cannot use Rat-King without a target!");
            return false;
        }
        // Kill self:
        this.owner.die();
        // Apply bonus HP
        target.maxHP += magicNumber;
        target.currentHP += magicNumber;
        // Apply strength
        target.AddStatusEffect(new Strength(magicNumber2, target));
        return true;
    }
}