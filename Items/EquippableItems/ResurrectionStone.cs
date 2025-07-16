public class ResurrectionStone : EquipmentItem {

    Resurrect resurrectInstance;

    public ResurrectionStone() {
        this.resurrectInstance = new Resurrect();
        this.resurrectInstance.equippedItem = this; // Still show medkit as equipped
        this.name = "Resurrection Stone";
        this.description = "Replace your Rest action with '"+resurrectInstance+"'.";
        this.slot = ActionType.REST;
        this.price = 260;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        resurrectInstance.owner = this.getOwner();
        // Replace parent action in action list with Stab
        this.replaceAction(this.resurrectInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}