public class MasterSword : EquipmentItem {

    Cleave cleaveInstance;
    TwinSlash twinslashInstance;
    Counter counterInstance;

    public MasterSword()
    {
        cleaveInstance = new Cleave();
        twinslashInstance = new TwinSlash();
        counterInstance = new Counter();
        cleaveInstance.hasEquipmentSlot = false; // Show that the new actions do not come with equipment slots.
        twinslashInstance.hasEquipmentSlot = false; // Show that the new actions do not come with equipment slots.
        counterInstance.hasEquipmentSlot = false; // Show that the new actions do not come with equipment slots.
        this.name = "Master Sword";
        this.description = "Gain the " + cleaveInstance.name + ", " + twinslashInstance.name + ", and " + counterInstance + " actions.";
        this.slot = ActionType.ATTACK;
        this.price = 260;
        this.tier = 3;
    }

    public override void onEquip() {
        base.onEquip();
        // Add the actions to owners action list
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.ActionList.Add(cleaveInstance);
        this.getOwner()!.ActionList.Add(twinslashInstance);
        this.getOwner()!.ActionList.Add(counterInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove the actions from owners action list
        this.getOwner()!.ActionList.Remove(cleaveInstance);
        this.getOwner()!.ActionList.Remove(twinslashInstance);
        this.getOwner()!.ActionList.Remove(counterInstance);
        this.getOwner()!.assignActionOwnership();
    }

}