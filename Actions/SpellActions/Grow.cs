public class Grow : Action {

    public Grow() {
        this.name = "Grow";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Gain "+magicNumber+" Strength.";
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