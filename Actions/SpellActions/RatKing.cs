public class RatKing : Action {

    public RatKing() {
        this.name = "Rat-King";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 7;
        this.magicNumber2 = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ALLY;
        this.description = "Kill self. Give Giant Rat +"+magicNumber+" HP and +"+magicNumber2+" Strength.";
    }


    // Requires an allied Giant Rat.
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
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
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
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