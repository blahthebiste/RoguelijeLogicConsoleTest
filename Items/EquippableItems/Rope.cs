public class Rope : EquipmentItem {

    Climb climbInstance;

    public Rope()
    {
        climbInstance = new Climb();
        climbInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Rope";
        this.description = "Gain the '" + climbInstance + "' action.";
        this.slot = ActionType.SKILL;
        this.price = 65;
        this.tier = 1;
    }

    public override void onEquip() {
        base.onEquip();
        // Add Climb action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.ActionList.Add(climbInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove Climb action to owners action list
        this.getOwner()!.ActionList.Remove(climbInstance);
        this.getOwner()!.assignActionOwnership();
    }

}