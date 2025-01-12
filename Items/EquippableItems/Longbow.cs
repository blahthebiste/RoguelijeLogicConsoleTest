public class Longbow : EquipmentItem {


    public Longbow() {
        this.name = "Longbow";
        this.description = "Your Attack action ignores Taunt.";
        this.slot = ActionType.ATTACK;
    }

    public override void onEquip() {
        base.onEquip();
        // Make the equipped action ignore taunt
        this.parentAction!.ignoresTaunt = true;
        this.parentAction!.description += " Ignores Taunt.";
    }


    public override void onUnequip() {
        base.onUnequip();
        // Get a fresh copy of the action, to remove our modifications
        this.parentAction = DataRegistry.ActionData.getActionByName(this.parentAction!.name);
    }

}