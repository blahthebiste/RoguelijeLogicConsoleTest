public class Campfire : EquipmentItem {


    public Campfire() {
        this.name = "Campfire";
        this.description = "Your Rest action also affects all allies.";
        this.slot = ActionType.REST;
        this.price = 65;
    }

    // Triggers whenever the action that the item is equipped to is used
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        actionBeingUsed.targetting = TargetCategory.ALL_ALLIES;
        actionBeingUsed.setTargets(TargetCategory.ALL_ALLIES);
        return actionBeingUsed;
    }
}