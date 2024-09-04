public class Idle : Action {

    public Idle() {
        this.name = "Idle";
        this.description = "Do nothing.";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        // Do nothing.
        return base.use(target, modifier);
    }
}