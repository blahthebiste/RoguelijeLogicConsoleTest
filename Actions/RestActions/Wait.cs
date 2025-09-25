public class Wait : Action {

    public Wait() {
        this.name = "Wait";
        this.description = "The King waits patiently for his Court to handle this.";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        // Do nothing.
        return true;
    }
}