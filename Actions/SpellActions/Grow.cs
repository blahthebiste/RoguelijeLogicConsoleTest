public class Grow : Action {

    public Grow() {
        this.name = "Grow";
        this.description = "Gain 2 Strength.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Strength status effect
        this.owner.AddStatusEffect(new Strength(magicNumber, this.owner));
        return true;
    }
}