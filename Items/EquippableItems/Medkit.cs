public class Medkit : EquipmentItem {

    Tend tendInstance;

    public Medkit() {
        this.tendInstance = new Tend();
        this.tendInstance.equippedItem = this; // Still show medkit as equipped
        this.name = "Medkit";
        this.description = "Replace your Rest action with '"+tendInstance+"'.";
        this.slot = ActionType.REST;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        tendInstance.owner = this.getOwner();
        // Replace parent action in action list with Stab
        this.replaceAction(this.tendInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}