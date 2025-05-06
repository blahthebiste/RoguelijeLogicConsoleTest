public class ManaPotion : EquipmentItem {

    UseManaPotion useManaPotionInstance;

    public ManaPotion() {
        this.useManaPotionInstance = new UseManaPotion();
        this.name = "Mana Potion";
        this.description = "Gain the '"+useManaPotionInstance+"' action.";
        this.slot = ActionType.ANY;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        // Add action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        useManaPotionInstance.owner = this.getOwner();
        this.getOwner()!.ActionList.Add(useManaPotionInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(useManaPotionInstance);
    }

}