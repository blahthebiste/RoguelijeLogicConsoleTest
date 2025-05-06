public class PoisonPotion : EquipmentItem {

    UsePoisonPotion usePoisonPotionInstance;

    public PoisonPotion() {
        this.usePoisonPotionInstance = new UsePoisonPotion();
        this.name = "Poison Potion";
        this.description = "Gain the '"+usePoisonPotionInstance+"' action.";
        this.slot = ActionType.ANY;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        // Add action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        usePoisonPotionInstance.owner = this.getOwner();
        this.getOwner()!.ActionList.Add(usePoisonPotionInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(usePoisonPotionInstance);
    }

}