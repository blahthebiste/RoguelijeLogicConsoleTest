public class TakeFlight : Action {

    public TakeFlight() {
        this.name = "Take Flight";
        this.actionType = ActionType.SPELL;
        this.targetting = TargetCategory.SELF;
        this.description = "Start Flying.";
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply the Flying status effect
        this.owner.AddStatusEffect(new Flying(1, this.owner));
        return true;
    }
}