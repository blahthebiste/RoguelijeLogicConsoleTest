public class Idle : Action {

    public Idle() {
        this.name = "Idle";
        this.description = "Do nothing.";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Do nothing.            
            return true;
        }
        return false;
    }
}