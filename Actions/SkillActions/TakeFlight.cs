public class TakeFlight : Action {

    public TakeFlight() {
        this.name = "Take Flight";
        this.description = "Start Flying.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SELF;
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