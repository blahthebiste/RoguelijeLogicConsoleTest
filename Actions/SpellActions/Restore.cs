public class Restore : Action {

    public Restore() {
        this.name = "Restore";
        this.actionType = ActionType.SPELL;
        this.healing = 6;
        this.hasLimitedUses = true;
        this.uses = 6;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ANY;
        this.description = "Restore "+healing+" HP.";
    }

    public override bool useOnTarget(Entity? target) {
        // Apply healing
        target!.ReceiveHealing(properties.healing);
        return true;
    }
}