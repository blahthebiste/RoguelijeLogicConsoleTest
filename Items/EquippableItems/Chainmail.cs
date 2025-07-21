public class Chainmail : EquipmentItem {


    public Chainmail()
    {
        this.name = "Chainmail";
        this.description = "+3 max HP.";
        this.slot = ActionType.ANY;
        this.price = 65;
        this.tier = 1;
    }

    public override void onEquip() {
        base.onEquip();
        // Add +3 max HP to owner
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.changeMaxHP(3);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove the bonus HP
        this.getOwner()!.changeMaxHP(-3);
    }

}