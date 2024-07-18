public class Rest : Action {

    public Rest() {
        this.name = "Rest";
        this.description = "Recover 3 HP.";
        this.actionType = ActionType.Rest;
        this.healing = 3;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity source, Entity? target, Modifier? modifier) {
        // Restore HP.
        source.recieveHealing(healing);
    }
}