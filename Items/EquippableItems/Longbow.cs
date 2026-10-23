public class Longbow : EquipmentItem {

    Shoot shootInstance;
    Snipe snipeInstance;

    public Longbow()
    {
        this.shootInstance = new Shoot();
        this.snipeInstance = new Snipe();
        this.shootInstance.equippedItem = this;
        this.snipeInstance.equippedItem = this;
        this.name = "Longbow";
        this.description = "Replace your skill action with '" + shootInstance + "'. If the action was already Shoot, it becomes Snipe.";
        this.slot = ActionType.SKILL;
        this.price = 65;
        this.tier = 1;
    }


    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null || this.parentAction == null) {
            return;
        }
        // Add Shoot action to owners action list
        if(this.parentAction!.name == "Shoot") {
            // Action was already Shoot, make it Snipe
            snipeInstance.owner = this.getOwner()!;
            // Replace parent action in action list with Snipe
            this.replaceAction(this.snipeInstance);
        }
        else {
            shootInstance.owner = this.getOwner()!;
            // Replace parent action in action list with Shoot
            this.replaceAction(this.shootInstance);
        }
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}