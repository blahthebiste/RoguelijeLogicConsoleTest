public class Shortsword : EquipmentItem {

    Strike strikeInstance;
    TwinSlash twinslashInstance;

    public Shortsword() {
        this.strikeInstance = new Strike();
        this.twinslashInstance = new TwinSlash();
        this.strikeInstance.equippedItem = this;
        this.twinslashInstance.equippedItem = this;
        this.name = "Shortsword";
        this.description = "Replace your attack action with '"+strikeInstance+"'. If the action was already Strike, it becomes Twin Slash.";
        this.slot = ActionType.ATTACK;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null || this.parentAction == null) {
            return;
        }
        // Add Strike action to owners action list
        if(this.parentAction!.name == "Strike") {
            // Action was already Strike, make it Twin Slash
            twinslashInstance.owner = this.getOwner()!;
            // Replace parent action in action list with twin slash
            this.replaceAction(this.twinslashInstance);
        }
        else {
            strikeInstance.owner = this.getOwner()!;
            // Replace parent action in action list with strike
            this.replaceAction(this.strikeInstance);
        }
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}