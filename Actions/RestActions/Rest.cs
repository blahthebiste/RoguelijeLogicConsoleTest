public class Rest : Action {

    public Rest() {
        this.name = "Rest";
        this.description = "Recover 3 HP.";
        this.actionType = ActionType.REST;
        this.healing = 3;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Restore HP.
            owner!.ReceiveHealing(healing);
            return true;
        }
        return false;
    }
}