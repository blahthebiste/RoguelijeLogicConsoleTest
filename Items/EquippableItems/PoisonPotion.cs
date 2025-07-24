public class PoisonPotion : EquipmentItem {

    UsePoisonPotion usePoisonPotionInstance;

    public PoisonPotion()
    {
        usePoisonPotionInstance = new UsePoisonPotion();
        usePoisonPotionInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Poison Potion";
        this.description = "Gain the '" + usePoisonPotionInstance + "' action.";
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
        this.getOwner()!.ActionList.Add(usePoisonPotionInstance);
        this.getOwner()!.assignActionOwnership();
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(usePoisonPotionInstance);
        this.getOwner()!.assignActionOwnership();
    }

}