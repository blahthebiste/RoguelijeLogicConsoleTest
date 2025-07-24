public class GrapplingHook : EquipmentItem {

    Hook hookInstance;

    public GrapplingHook()
    {
        hookInstance = new Hook();
        hookInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Grappling Hook";
        this.description = "Gain the '" + hookInstance + "' action.";
        this.slot = ActionType.SKILL;
        this.price = 130;
        this.tier = 2;
    }

    public override void onEquip()
    {
        base.onEquip();
        // Add Hook action to owners action list
        if (this.getOwner() == null)
        {
            return;
        }
        this.getOwner()!.ActionList.Add(hookInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove Hook action to owners action list
        this.getOwner()!.ActionList.Remove(hookInstance);
        this.getOwner()!.assignActionOwnership();
    }

}