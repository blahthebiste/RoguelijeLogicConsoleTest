public class Surge : Action {

    public Surge() {
        this.name = "Surge";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Next Spell deals double damage/block/healing.";
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Surging status effect
        this.owner.AddStatusEffect(new Surging(magicNumber, this.owner));
        return true;
    }
}