public class MasterSword : EquipmentItem {

    Cleave cleaveInstance;
    TwinSlash twinslashInstance;
    Counter counterInstance;

    public MasterSword() {
        this.cleaveInstance = new Cleave();
        this.twinslashInstance = new TwinSlash();
        this.counterInstance = new Counter();
        this.cleaveInstance.hasEquipmentSlot = false; // Show that the new actions do not come with equipment slots.
        this.twinslashInstance.hasEquipmentSlot = false; // Show that the new actions do not come with equipment slots.
        this.counterInstance.hasEquipmentSlot = false; // Show that the new actions do not come with equipment slots.
        this.name = "Master Sword";
        this.description = "Gain the "+cleaveInstance.name+", "+twinslashInstance.name+", and "+counterInstance+" actions.";
        this.slot = ActionType.ATTACK;
        this.price = 260;
    }

    public override void onEquip() {
        base.onEquip();
        // Add the actions to owners action list
        if(this.getOwner() == null) {
            return;
        }
        cleaveInstance.owner = this.getOwner();
        twinslashInstance.owner = this.getOwner();
        counterInstance.owner = this.getOwner();
        this.getOwner()!.ActionList.Add(cleaveInstance);
        this.getOwner()!.ActionList.Add(twinslashInstance);
        this.getOwner()!.ActionList.Add(counterInstance);
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
    }

}