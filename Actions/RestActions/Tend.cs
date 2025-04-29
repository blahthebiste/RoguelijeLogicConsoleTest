public class Tend : Action {

    public Tend() {
        this.name = "Tend";
        this.description = "Heal 4 HP.";
        this.actionType = ActionType.REST;
        this.healing = 4;
        this.targetting = TargetCategory.SINGLE_ALLY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Restore HP.
        target!.ReceiveHealing(healing);
        return true;
    }
}