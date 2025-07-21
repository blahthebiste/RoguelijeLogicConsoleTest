public class RevivePotion : EquipmentItem {

    UseRevivePotion useRevivePotionInstance;

    public RevivePotion()
    {
        this.useRevivePotionInstance = new UseRevivePotion();
        this.useRevivePotionInstance.hasEquipmentSlot = false; // Show that the new action does not come with equipment slots.
        this.name = "Revive Potion";
        this.description = "Gain the '" + useRevivePotionInstance + "' action.";
        this.slot = ActionType.ANY;
        this.price = 130;
        this.tier = 2;
    }

    public override void onEquip() {
        base.onEquip();
        // Add action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        useRevivePotionInstance.owner = this.getOwner();
        this.getOwner()!.ActionList.Add(useRevivePotionInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(useRevivePotionInstance);
    }

}