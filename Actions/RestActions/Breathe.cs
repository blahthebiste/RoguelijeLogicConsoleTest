public class Breathe : Action {

    public Breathe() {
        this.name = "Breathe";
        this.description = "Recover 2 HP.";
        this.actionType = ActionType.REST;
        this.healing = 2;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Restore HP.
        target!.ReceiveHealing(healing);
        return true;
    }
}