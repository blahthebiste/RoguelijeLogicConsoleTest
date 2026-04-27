public class Idle : Action {

    public Idle() {
        this.name = "Idle";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
        this.description = "Do nothing.";
    }

    public override bool useOnce(Modifier? modifier) {
        // Do nothing.
        return true;
    }

    public override bool canUse(Entity? target, Modifier? modifier)
    {
        return true; // You can ALWAYS do nothing!
    }
}