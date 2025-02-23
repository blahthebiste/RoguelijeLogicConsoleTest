public class Tend : Action {

    public Tend() {
        this.name = "Tend";
        this.description = "Heal 4 HP.";
        this.actionType = ActionType.REST;
        this.healing = 4;
        this.targetting = TargetCategory.SINGLE_ALLY;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Restore HP.
            target!.ReceiveHealing(healing);
            return true;
        }
        return false;
    }
}