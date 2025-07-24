public class ManaPotion : EquipmentItem {

    UseManaPotion useManaPotionInstance;

    public ManaPotion()
    {
        useManaPotionInstance = new UseManaPotion();
        useManaPotionInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Mana Potion";
        this.description = "Gain the '" + useManaPotionInstance + "' action.";
        this.slot = ActionType.ANY;
        this.price = 65;
        this.tier = 1;
    }

    public override void onEquip() {
        base.onEquip();
        // Add action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.ActionList.Add(useManaPotionInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(useManaPotionInstance);
        this.getOwner()!.assignActionOwnership();
    }

}