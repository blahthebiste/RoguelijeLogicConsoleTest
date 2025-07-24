public class Quarterstaff : EquipmentItem {

    Parry parryInstance;

    public Quarterstaff()
    {
        parryInstance = new Parry();
        parryInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Quarterstaff";
        this.description = "Gain the '" + parryInstance + "' action.";
        this.slot = ActionType.ATTACK;
        this.price = 65;
        this.tier = 1;
    }

    public override void onEquip() {
        base.onEquip();
        // Add Strike action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.ActionList.Add(parryInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove Strike action to owners action list
        this.getOwner()!.ActionList.Remove(parryInstance);
        this.getOwner()!.assignActionOwnership();
    }

}