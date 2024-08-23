public class Rest : Action {

    public Rest() {
        this.name = "Rest";
        this.description = "Recover 3 HP.";
        this.actionType = ActionType.REST;
        this.healing = 3;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        // Restore HP.
        owner.recieveHealing(healing);
        return base.use(target, modifier);
    }
}