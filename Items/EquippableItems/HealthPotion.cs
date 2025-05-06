public class HealthPotion : EquipmentItem {

    UseHealthPotion useHealthPotionInstance;

    public HealthPotion() {
        this.useHealthPotionInstance = new UseHealthPotion();
        this.name = "Health Potion";
        this.description = "Gain the '"+useHealthPotionInstance+"' action.";
        this.slot = ActionType.ANY;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        // Add action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        useHealthPotionInstance.owner = this.getOwner();
        this.getOwner()!.ActionList.Add(useHealthPotionInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove action to owners action list
        this.getOwner()!.ActionList.Remove(useHealthPotionInstance);
    }

}