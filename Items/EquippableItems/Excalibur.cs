public class Excalibur : EquipmentItem {

    RightfulHeir useRightfulHeirInstance;

    public Excalibur()
    {
        useRightfulHeirInstance = new RightfulHeir();
        useRightfulHeirInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Excalibur";
        this.description = "Gain the '" + useRightfulHeirInstance + "' action.";
        this.slot = ActionType.ATTACK;
        this.price = 9999;
        this.tier = 4; // For now, "special" tiems are considered tier 4
    }

    public override void onEquip() {
        base.onEquip();
        // Add action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.ActionList.Add(useRightfulHeirInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(useRightfulHeirInstance);
        this.getOwner()!.assignActionOwnership();
    }

}