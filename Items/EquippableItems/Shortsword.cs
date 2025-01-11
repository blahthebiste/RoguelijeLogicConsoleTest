public class Shortsword : EquipmentItem {

    Strike strikeInstance;

    public Shortsword() {
        this.strikeInstance = new Strike();
        this.name = "Shortsword";
        this.description = "You have Strike [Deal 6 damage] as an additional Attack action option.";
        this.slot = ActionType.ATTACK;
    }

    public override void onEquip() {
        base.onEquip();
        // Add Strike action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        strikeInstance.owner = this.getOwner();
        this.getOwner()!.ActionList.Add(strikeInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove Strike action to owners action list
        this.getOwner()!.ActionList.Remove(strikeInstance);
    }

}