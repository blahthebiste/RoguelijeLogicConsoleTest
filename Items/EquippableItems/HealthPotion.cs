public class HealthPotion : EquipmentItem {

    UseHealthPotion useHealthPotionInstance;

    public HealthPotion()
    {
        useHealthPotionInstance = new UseHealthPotion();
        useHealthPotionInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Health Potion";
        this.description = "Gain the '" + useHealthPotionInstance + "' action.";
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
        this.getOwner()!.ActionList.Add(useHealthPotionInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(useHealthPotionInstance);
        this.getOwner()!.assignActionOwnership();
    }

}