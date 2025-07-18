public class Idle : Action {

    public Idle() {
        this.name = "Idle";
        this.description = "Do nothing.";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        // Do nothing.
        return true;
    }
}