public class Harden : Action {

    public Harden() {
        this.name = "Harden";
        this.description = "Gain 1 Toughness.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
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
        // Apply toughness status effect
        this.owner.AddStatusEffect(new Toughness(magicNumber, this.owner));
        return true;
    }
}