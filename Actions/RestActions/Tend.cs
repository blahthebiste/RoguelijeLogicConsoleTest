public class Tend : Action {

    public Tend() {
        this.name = "Tend";
        this.actionType = ActionType.REST;
        this.healing = 4;
        this.targetting = TargetCategory.SINGLE_ALLY;
        this.description = "Heal "+healing+" HP.";
    }

    public override bool useOnTarget(Entity? target) {
        // Restore HP.
        target!.ReceiveHealing((int)(properties.healing*properties.healingMult));
        return true;
    }
}