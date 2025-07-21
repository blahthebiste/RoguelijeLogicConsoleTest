public class IceWand : EquipmentItem {

    IceWall iceWallInstance;

    public IceWand()
    {
        this.iceWallInstance = new IceWall();
        this.iceWallInstance.equippedItem = this;
        this.name = "Ice Wand";
        this.description = "Replace your Spell action with '" + iceWallInstance + "'.";
        this.slot = ActionType.SPELL;
        this.price = 65;
        this.tier = 1;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        iceWallInstance.owner = this.getOwner();
        // Replace parent action in action list with Ice Wall
        this.replaceAction(this.iceWallInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}