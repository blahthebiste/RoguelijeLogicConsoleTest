public class Dagger : EquipmentItem {

    Stab stabInstance;

    public Dagger() {
        this.stabInstance = new Stab();
        this.stabInstance.equippedItem = this; // Still show dagger as equipped
        this.name = "Dagger";
        this.description = "Replace your Defend action with '"+stabInstance+"'.";
        this.slot = ActionType.DEFEND;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        stabInstance.owner = this.getOwner();
        // Replace parent action in action list with Stab
        this.replaceAction(this.stabInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}