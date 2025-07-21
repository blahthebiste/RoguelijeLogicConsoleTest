public class PlateArmor : EquipmentItem {


    public PlateArmor()
    {
        this.name = "Plate Armor";
        this.description = "+6 max HP.";
        this.slot = ActionType.ANY;
        this.price = 130;
        this.tier = 2;
    }

    public override void onEquip() {
        base.onEquip();
        // Add +6 max HP to owner
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.changeMaxHP(6);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove the bonus HP
        this.getOwner()!.changeMaxHP(-6);
    }

}